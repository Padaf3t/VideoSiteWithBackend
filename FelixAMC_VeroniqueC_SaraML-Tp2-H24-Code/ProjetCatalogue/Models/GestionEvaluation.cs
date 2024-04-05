using Newtonsoft.Json;

namespace ProjetCatalogue.Models
{
    /// <summary>
    /// Classe qui permet de gérer une liste d'évaluations
    /// </summary>
    public class GestionEvaluation
    {
        List<Evaluation> _listeEvaluations;

        public List<Evaluation> ListeEvaluations { get => _listeEvaluations; set => _listeEvaluations = value; }

        public GestionEvaluation()
        {
            _listeEvaluations = new List<Evaluation>();
        }

        /// <summary>
        /// Permet de calculer la cote d'évaluation moyenne d'une liste d'évaluation afin de la placer dans le champs _coteEvaluation 
        /// Attribu une cote de -1 s'il n'y a pas d'évaluation pour cette video
        /// </summary>
        /// <param name="video">La video que l'on veut modifier la cote</param>
        public void calculerCoteEvaluation(Video video)
        {
            int count = 0;
            IEnumerable<Evaluation> query =
            from eval in this.ListeEvaluations
            where eval.IdVideo.Equals(video.IdVideo)
            select eval;

            double cote = 0;

            foreach (Evaluation eval in query)
            {
                cote += (double)eval.CoteDonne;
            }
            count = query.Count();
            if (count == 0)
            {
                cote = -1;
            }
            else
            {
                cote /= count;
            }
            video.CoteEvaluation = cote;
        }

        /// <summary>
        /// Permet l'ajout d'une évaluation à la liste des évaluations de l'utilisateur
        /// </summary>
        /// <param name="user">L'utilisateur qui créer l'évaluation</param>
        /// <param name="video">La vidéo évaluée</param>
        /// <param name="cote">La cote attribuée</param>
        /// <param name="texte">Le texte que l'utilisateur a écrit pour son évaluation</param>
        /// <returns>bool : true si l'évaluation a bien été ajoutée à sa liste</returns>
        public bool AjouterEvaluation(Video video, Utilisateur user, EnumCote cote, string texte)
        {
            Evaluation evaluation = new Evaluation(video.IdVideo, user.Pseudo, cote, texte);


            IEnumerable<Evaluation> query =
            from evaluationTemp in this.ListeEvaluations
            where evaluationTemp.Equals(evaluation)
            select evaluationTemp;

            bool erreurNote = false;

            try
            {
                if (query.Count() > 0)
                {
                    throw new ArgumentException("L'utilisateur " + user.Pseudo + " a déjà fait une évaluation pour la vidéo #" + video.IdVideo);
                }

                this.ListeEvaluations.Add(evaluation);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                erreurNote = true;
            }

            return !erreurNote;

        }

        /// <summary>
        /// Permet de prendre une liste d'évaluations et de la sérialiser dans un fichier JSON
        /// </summary>
        /// <param name="fichierJSON">Le fichier JSON à utiliser</param>
        public void SerialisationEvaluation(string fichierJSON)
        {

            string jsonListe = JsonConvert.SerializeObject(this.ListeEvaluations, this.ListeEvaluations.GetType(), Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            File.WriteAllText(@fichierJSON, jsonListe);
        }

        /// <summary>
        /// Méthode qui permet la désérialisation d'un fichier JSON pour en extraire des objets C# Évaluations et les placer
        /// dans une liste d'Évaluation
        /// soulève une exception si le dossier n'est pas trouvé ou que le fichier n'est pas trouver
        /// </summary>
        /// <param name="fichierJSON"><Le fichier JSON utilisé/param>
        public void DeserisalisationJSONEvaluation(string fichierJSON)
        {
            List<Evaluation>? liste = null;
            try
            {

                liste = JsonConvert.DeserializeObject<List<Evaluation>>(File.ReadAllText(@fichierJSON), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Le dossier {0} n'a pas été trouvé", @fichierJSON);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Le fichier {0} n'a pas été trouvé", fichierJSON);
            }
            finally
            {
                if (liste != null)
                {
                    this.ListeEvaluations = liste;
                }
            }
        }
    }
}
