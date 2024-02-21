using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue
{

    /// <summary>
    /// Classe qui constitue une vidéo
    /// </summary>
    public class Video
    {

        /// <summary>
        /// Attribut statique qui représente le dernier id qui a été attribué à une vidéo
        /// </summary>
        static int lastId = 0;
        

        private int _idVideo;
        private string _titre;
        private EnumAnimal _typeVideo;
        private double _coteEvaluation;
        private DateOnly? _dateRealisation;
        private double _dureeVideo;
        private string _auteur;
        private string _acteur;
        private string _extrait;
        private string _videoComplet;
        private string _image;

        public int IdVideo { get => _idVideo; set => _idVideo = value; }
        public string Titre
        {
            get => _titre;
            set
            {
                if (value.Length < 5 || value.Length > 50)
                {
                    throw new ArgumentException("Le titre doit être entre 5 et 50 caractères");
                }
                else
                {
                    _titre = value;
                }
            }
        }
        public EnumAnimal TypeVideo { get => _typeVideo; set => _typeVideo = value; }
        public double CoteEvaluation { get => _coteEvaluation; set => _coteEvaluation = value; }
        public DateOnly? DateRealisation { get => _dateRealisation; set => _dateRealisation = value; }
        public double DureeVideo { get => _dureeVideo; set => _dureeVideo = value; }
        public string Auteur { get => _auteur; set => _auteur = value; }
        public string Acteur { get => _acteur; set => _acteur = value; }
        public string Extrait { get => _extrait; set => _extrait = value; }
        public string VideoComplet { get => _videoComplet; set => _videoComplet = value; }
        public string Image { get => _image; set => _image = value; }

        /// <summary>
        /// Constructeur défaut pour la sérialisation
        /// </summary>
        public Video()
        {

        }

        /// <summary>
        /// Constructeur avec id en paramètre, appelle le constructeur avec tous param
        /// </summary>
        /// <param name="pIdVideo"></param>
        public Video(int pIdVideo) : this("Insérez un titre svp", EnumAnimal.Indetermine, 0, new List<Evaluation>(), null, 0, "", "", "", "", "")
        {

        }

        /// <summary>
        /// Constructeur qui reçoit un titre seulement en paramètre. Va appeler le constructeur qui prend tous les param sauf id.
        /// Va mettre par défaut le type indéterminé comme type de vidéo (type d'animal) et une cote de 0. Va créer une liste
        /// vide d'évaluations, va mettre une date de réalisation par défaut null, une durée de vidéo de 0, et va mettre des
        /// chaines vides pour l'auteur, l'acteur, le path de l'extrait, le path de la vidéo complète, et le path de l'image.
        /// </summary>
        /// <param name="pTitre">string : le titre de la vidéo (entre 5 et 50 char)</param>
        public Video(string pTitre) : this(pTitre, EnumAnimal.Indetermine, 0, new List<Evaluation>(), null, 0, "", "", "", "", "")
        {
                     
        }

        /// <summary>
        /// Constructeur qui reçoit tous les paramètres, sauf id qui est généré automatiquement.
        /// </summary>
        /// <param name="pTitre">le titre de la vidéo (entre 5 et 50 char)</param>
        /// <param name="pTypeVideo">le type de vidéo qui conrespond au type d'animal qui y figure</param>
        /// <param name="pCoteEvaluation">La cote de la vidéo (moyenne de toutes les Évaluations)</param>
        /// <param name="pListeEvaluation">La liste des évaluations associées à la vidé</param>
        /// <param name="pDateRealisation">La date de réalisation de la vidéo</param>
        /// <param name="pDureeVideo">La durée de la vidéo</param>
        /// <param name="pAuteur">L'auteur de la vidéo</param>
        /// <param name="pActeur">L'acteur de la vidéo</param>
        /// <param name="pExtrait">Le path de l'extrait de la vidéo</param>
        /// <param name="pVideoComplet">Le path du vidéo complet</param>
        /// <param name="pImage">Le path de l'image qui représente la vidéo</param>
        public Video(string pTitre, EnumAnimal pTypeVideo, double pCoteEvaluation, List<Evaluation> pListeEvaluation, DateOnly? pDateRealisation,
            double pDureeVideo, string pAuteur, string pActeur, string pExtrait, string pVideoComplet, string pImage)
        {
            IdVideo = GenerateId(); // unique
            Titre = pTitre;
            TypeVideo = pTypeVideo;
            CoteEvaluation = pCoteEvaluation;
            DateRealisation = pDateRealisation;
            DureeVideo = pDureeVideo;
            Auteur = pAuteur;
            Acteur = pActeur;
            Extrait = pExtrait;
            VideoComplet = pVideoComplet;
            Image = pImage;
        }

        /// <summary>
        /// Permet de générer un id pour la vidéo, qui sera incrémenté après le dernier id utilisé pour une vidéo
        /// </summary>
        /// <returns>int : le id généré</returns>
        private static int GenerateId()
        {
            return Interlocked.Increment(ref lastId);
        }

        /// <summary>
        /// Permet de set le dernier id utilisé pour une vidéo (lastId) selon une valeur reçue en paramètre, si la valeur du
        /// lastId est de 0
        /// </summary>
        /// <param name="value">int: la valeur à attribuer à lastId</param>
        public void SetLastIdAuDemarageDuProgramme(int value)
        {
            if(lastId == 0)
            {
                lastId = value;
            }
        }

        /// <summary>
        /// Méthode ToString de la classe
        /// </summary>
        /// <returns>string: chaine contenant l'id et le titre de la vidéo</returns>
        public override string ToString()
        {
            return IdVideo + " " + Titre;
        }

        /// <summary>
        /// Méthode Equals de la classe, est appelée sur une instance de Video et valide si elle est pareille à un objet reçu
        /// en paramètre (valide si l'objet en question est bien une Video, puis les compare)
        /// </summary>
        /// <param name="obj">L'objet à comparer à la vidéo sur laquelle la méthode est appelée</param>
        /// <returns>bool : true si c'est la même vidéo</returns>
        public override bool Equals(object? obj)
        {
            return obj is Video video &&
                   IdVideo == video.IdVideo;
        }

        /// <summary>
        /// Méthode GetHashCode de la classe. Produit un hashcode sur la base du id de la vidéo
        /// </summary>
        /// <returns>int: le hashcode produit</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(IdVideo);
        }

        /// <summary>
        /// Methode de surcharge de l'opérateur ==, qui utilise la méthode Equals pour vérifier si 2 vidéos sont la même vidéo
        /// </summary>
        /// <param name="left">Video : une vidéo</param>
        /// <param name="right">Video : une autre vidéo</param>
        /// <returns>true si c'est la même vidéo</returns>
        public static bool operator == (Video left, Video right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Methode de surcharge de l'opérateur !=, qui utilise la méthode de surcharge de l'opérateur == pour vérifier si
        /// 2 vidéos sont différentes
        /// </summary>
        /// <param name="left">Video : une vidéo</param>
        /// <param name="right">Video : une autre vidéo</param>
        /// <returns></returns>
        public static bool operator !=(Video left, Video right)
        {
            return !(left == right);
        }
    }
}
