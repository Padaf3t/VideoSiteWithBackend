using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace ProjetCatalogue.Models
{

    /// <summary>
    /// Classe qui constitue un utilisateur
    /// </summary>
    public class Utilisateur
    {
        
        private string _pseudo;
        private string _motDePasse;
        private string _nom;
        private string _prenom;
        private EnumRole _roleUser;

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string Pseudo { 
            get => _pseudo; 
            set {
                    _pseudo = VerifierPseudo(value);
                } 
        }
        
        public string MotDePasse {
            get => _motDePasse;
            set {
                _motDePasse = VerifierMotDePasse(value);
            } 
        }
        public string Nom { 
            get => _nom;
            set
            { 
                if(value.Length >= 50 || Regex.Matches(value, "^[\\w-]*$").Count == 0)
                {
                    throw new ArgumentException("Le nom doit avoir 50 caractère ou moins. Il doit être composer de caractère alphanumérique ou '-'. ");
                }
                _nom = value; 
            }
        }
        public string Prenom
        {
            get => _prenom;
            set
            {
                if (value.Length >= 50 || Regex.Matches(value,"^[\\w-]*$").Count == 0)
                {
                    throw new ArgumentException("Le prénom doit avoir 50 caractère ou moins. Il doit être composer de caractère alphanumérique ou '-'. ");
                }
                _prenom = value;
            }
        }
        public EnumRole RoleUser { get => _roleUser; set => _roleUser = value; }

        [InverseProperty("Utilisateur")]
        public virtual ICollection<Favori> Favoris { get; set; }
        

        /// <summary>
        /// Constructeur par défaut pour la sérialisation
        /// </summary>
        public Utilisateur()
        {
        }


        /// <summary>
        /// Constructeur de la classe, avec 2 paramètres. Le pseudo défini l'utilisateur. Son nom et prénom sont mis automatiquement
        /// en chaines vides et seront définis plus tard, son rôle est mis par défaut à UtilisateurSimple
        /// </summary>
        /// <param name="pPseudo">le pseudo de l'utilisateur</param>
        /// <param name="pMotDePasse">le mot de passe de l'utilisateur</param>
        public Utilisateur(string pPseudo, string pMotDePasse) : this(pPseudo,pMotDePasse, "", "", EnumRole.UtilisateurSimple)
        {
        }

        /// <summary>
        /// Constructeur de la classe complet. Le pseudo défini l'utilisateur.
        /// </summary>
        /// <param name="pPseudo"></param>
        /// <param name="pMotDePasse"></param>
        /// <param name="pNom"></param>
        /// <param name="pPrenom"></param>
        /// <param name="pRoleUser"></param>
        public Utilisateur(string pPseudo, string pMotDePasse, string pNom, string pPrenom, EnumRole pRoleUser)
        {
            Pseudo = pPseudo;
            MotDePasse = pMotDePasse;
            Nom = pNom;
            Prenom = pPrenom;
            RoleUser = pRoleUser;
        }

        /// <summary>
        /// Permet de vérifié si le pseudo contient minimalement 5 charactères à 20 charactères uniquement alphanumérique
        /// (sinon lance une exception)
        /// </summary>
        /// <param name="pseudo">le pseudo à tester</param>
        /// <returns>Le pseudo entré s'il répond à tous les critères</returns>
        /// <exception cref="ArgumentException"></exception>
        private string VerifierPseudo(string pseudo)
        {
            int longeurMin = 5;
            int longeurMax = 20;
            if (pseudo == null || pseudo.Length <= longeurMin || pseudo.Length >= longeurMax || Regex.Matches(pseudo, "^\\w+$").Count == 0)
            {
                throw new ArgumentException("Le pseudo doit être entre " + longeurMin + " et " + longeurMax +" caractères inclusivement et ne doit pas contenir de caractère spécial. ");
            }
            return pseudo;
        }

        /// <summary>
        /// Vérifie si le pseudo répond aux critères (longueur de 5 à 20 char, pas null, match regex) (sinon lance une exception)
        /// </summary>
        /// <param name="pseudo">le pseudo à vérifier</param>
        /// <exception cref="ArgumentException"></exception>
        public static void VerifierUnPseudo(string pseudo)
        {
            int longeurMin = 5;
            int longeurMax = 20;
            if (pseudo == null || pseudo.Length <= longeurMin || pseudo.Length >= longeurMax || Regex.Matches(pseudo, "^\\w+$").Count == 0)
            {
                 throw new ArgumentException("Le pseudo doit être entre " + longeurMin + " et " + longeurMax + " caractères inclusivement et ne doit pas contenir de caractère spécial. ");
            }
        }

        /// <summary>
        /// Permet de vérifier si le mot de passe contient minimum 8 et maximum 60 charactères dont 1 chiffre et 1 charactère spécial
        /// Elle emmet une erreur si un des critère n'est pas respecté
        /// </summary>
        /// <param name="motDePasse">string : mot de passe a tester</param>
        /// <returns>Le mot de passe s'il répond à tous les critères </returns>
        /// <exception cref="ArgumentException">Lance une exception si un critère n'est pas respecté</exception>
        private string VerifierMotDePasse(string motDePasse)
        {

            if (motDePasse == null || motDePasse.Length <= 8 || motDePasse.Length >= 60 || !motDePasse.Any(char.IsDigit) || Regex.Matches(motDePasse, "[@.#$!%*?&$]").Count == 0)
            {
                throw new ArgumentException("Le mot de passe doit avoir une longueur de 8 à 60 caractères inclusivement, contenir un chiffre et un caractère spécial. ");
            }
            return motDePasse;
        }

        /// <summary>
        /// Fait la vérification du mot de passe (ne peut pas être null, doit avoir une longueur entre 8 et 60 char et doit répondre à la regex,
        /// sinon, lance une exception)
        /// </summary>
        /// <param name="motDePasse">le mot de passe à vérifier</param>
        /// <exception cref="ArgumentException"></exception>
        public static void VerifierUnMotDePasse(string motDePasse)
        {
            if (motDePasse == null || motDePasse.Length <= 8 || motDePasse.Length > 60 || !motDePasse.Any(char.IsDigit) || Regex.Matches(motDePasse, "[@.#$!%*?&$]").Count == 0)
            {
                throw new ArgumentException("Le mot de passe doit avoir une longueur de 8 à 60 caractères inclusivement, contenir un chiffre et un caractère spécial. ");
            }
        }

        /// <summary>
        /// Vérifie un nom pour s'assurer qu'il ne dépasse pas 50 char (sinon lance une exception)
        /// </summary>
        /// <param name="nom">le nom à vérifier</param>
        /// <exception cref="ArgumentException"></exception>
        public static void VerifierUnNom(string nom)
        {
            if (nom.Length >= 50 || Regex.Matches(nom, "^[\\w-]*$").Count == 0)
            {
                throw new ArgumentException("Le nom doit avoir 50 caractère ou moins. Il doit être composer de caractère alphanumérique ou '-'. ");
            }
        }

        /// <summary>
        /// Vérifie un nom pour s'assurer qu'il ne dépasse pas 50 char (sinon lance une exception)
        /// </summary>
        /// <param name="prenom">le prénom à vérifier</param>
        /// <exception cref="ArgumentException"></exception>
        public static void VerifierUnPrenom(string prenom)
        {
            if (prenom.Length >= 50 || Regex.Matches(prenom, "^[\\w-]*$").Count == 0)
            {
                throw new ArgumentException("Le prénom doit avoir 50 caractère ou moins. Il doit être composer de caractère alphanumérique ou '-'. ");
            }
        }

        /// <summary>
        /// Methode Equals de la classe, qui valide si l'objet reçu en param est bien un utilisateur et si c'est le même
        /// utilisateur que celui sur lequel la méthode a été appelée (selon les pseudos)
        /// </summary>
        /// <param name="obj">L'objet à comparer à l'utilisateur sur lequel la méthode a été appelée</param>
        /// <returns>bool : true si ce sont le même utilisateur</returns>
        public override bool Equals(object? obj)
        {
            return obj is Utilisateur utilisateur &&
                   _pseudo == utilisateur._pseudo &&
                   _motDePasse == utilisateur._motDePasse;
        }

        /// <summary>
        /// Méthode GetHashCode de la classe, génère hashcode sur la base du pseudo
        /// </summary>
        /// <returns>int : le hashcode généré</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(_pseudo, _motDePasse);
        }
        


    }
}
