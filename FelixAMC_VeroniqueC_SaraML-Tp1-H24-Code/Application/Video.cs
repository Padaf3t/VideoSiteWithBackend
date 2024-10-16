﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjetCatalogue
{

    /// <summary>
    /// Classe qui constitue une vidéo
    /// </summary>
    public class Video
    {

        private static string pathExtrait = "ressources/extraits/";
        private static string pathVideoComplet = "ressources/videos/";
        private static string pathImage = "ressources/images/";



        private int _idVideo;
        private string _titre;
        private EnumAnimal _typeVideo;
        private double _coteEvaluation;
        private DateOnly _dateMiseEnLigne;
        private double _dureeVideo;
        private string _auteur;
        private string _acteur;
        private string _extrait;
        private string _videoComplet;
        private string _image;

        public int IdVideo { get => _idVideo; set => _idVideo = value; }
        
        /// <summary>
        /// Le titre doit avoir entre 5 et 50 charactères inclusivement
        /// </summary>
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
        public double CoteEvaluation
        {
            get => Math.Round(_coteEvaluation,1);
            set => this._coteEvaluation = value;
        }
        
        /// <summary>
        /// La DateRealisation doit être dans le passé
        /// </summary>
        public DateOnly DateMiseEnLigne
        {
            get => _dateMiseEnLigne;
            set
            {
                if (value > DateOnly.FromDateTime(System.DateTime.Now))
                {
                    throw new ArgumentException("La date ne peut pas être au-delà de la date du jour");
                }
                else
                {
                    _dateMiseEnLigne = value;
                }
            }
        }

        /// <summary>
        /// la DureeVideo doit de 0 à 30 minutes inclusivement
        /// </summary>
        public double DureeVideo
        {
            get => _dureeVideo;
            set
            {
                if (value < 0 || value > 30)
                {
                    throw new ArgumentException("La durée de la vidéo doit de 0 à 30 minutes");
                }
                else
                {
                    _dureeVideo = value;
                }
            }
        }

        /// <summary>
        /// La longueur de la chaine de charactères doit être 0 à 50 charactères inclusivement
        /// </summary>
        public string Auteur
        {
            get => _auteur;
            set
            {
                if (value.Length > 50)
                {
                    throw new ArgumentException("Le nom de l'auteur ne peut pas dépasser 50 caractères");
                }
                else
                {
                    _auteur = value;
                }
            }

        }

        /// <summary>
        /// Acteur doit avoir maximum 25 charactères
        /// </summary>
        public string Acteur
        {
            get => _acteur;
            set
            {
                if (value.Length > 25)
                {
                    throw new ArgumentException("Le nom de l'acteur ne peut pas dépasser 25 caractères");
                }
                else
                {
                    _acteur = value;
                }
            }
        }

        /// <summary>
        /// Extrait doit être le nom du fichier avec l'extension mp4
        /// </summary>
        public string Extrait
        {
            get => _extrait;
            set
            {

                if (!(Regex.IsMatch(value, "^.\\w*(\\.mp4)$")) && value != "")
                {
                    throw new ArgumentException("Le path de l'extrait n'est pas bon");
    }
                else
                {
                    _extrait = value;
                }
            }
        }

        /// <summary>
        /// VideoComplet doit être le nom du fichier avec l'extension mp4
        /// </summary>
        public string VideoComplet
        {
            get => _videoComplet;
            set
            {

                if (!(Regex.IsMatch(value, "^.\\w*(\\.mp4)$")) && value != "")
                {
                    throw new ArgumentException("Le path de la vidéo complète n'est pas bon");
                }
                else
                {
                    _videoComplet = value;
                }
            }
        }

        /// <summary>
        /// Image doit être le nom du fichier avec l'extension jpeg
        /// </summary>
        public string Image
        {
            get => _image;
            set
            {
                if (!(Regex.IsMatch(value, "^.\\w*(\\.jpeg)$")) && value != "")
                {
                    throw new ArgumentException("Le path de l'image n'est pas bon");
                }
                else
                {
                    _image = value;
                }
            }
        }

        /// <summary>
        /// Constructeur par défaut pour la sérialisation
        /// </summary>
        public Video() 
        {

        }

        /// <summary>
        /// Constructeur avec id en paramètre, appelle le constructeur avec tous param
        /// </summary>
        /// <param name="pIdVideo">id de la videos (doit être unique) </param>
        public Video(int pIdVideo) : this(pIdVideo, "Insérez un titre svp")
        {

        }

        /// <summary>
        /// Constructeur qui reçoit un titre seulement en paramètre. Va appeler le constructeur qui prend tous les param sauf id.
        /// Va mettre par défaut le type indéterminé comme type de vidéo (type d'animal) et une cote de 0. Va créer une liste
        /// vide d'évaluations, va mettre une date de réalisation par défaut null, une durée de vidéo de 0, et va mettre des
        /// chaines vides pour l'auteur, l'acteur, le path de l'extrait, le path de la vidéo complète, et le path de l'image.
        /// </summary>
        /// <param name="pIdVideo">int : Le Id de la video (doit être unique) </param>
        /// <param name="pTitre">string : le titre de la vidéo (entre 5 et 50 char)</param>
        public Video(int pIdVideo, string pTitre) : this(pIdVideo, pTitre, EnumAnimal.Indetermine, -1, DateOnly.FromDateTime(DateTime.Now), 0, "", "", "vide.mp4", "vide.mp4", "vide.jpeg")
        {
                     
        }

        /// <summary>
        /// Constructeur qui reçoit tous les paramètres, sauf id qui est généré automatiquement.
        /// </summary>
        /// <param name="pIdVideo">le Id de la video (doit être unique)</param>
        /// <param name="pTitre">le titre de la vidéo (entre 5 et 50 char)</param>
        /// <param name="pTypeVideo">le type de vidéo qui conrespond au type d'animal qui y figure</param>
        /// <param name="pCoteEvaluation">La cote de la vidéo (moyenne de toutes les Évaluations) - entre 0 et 5 inclusivement</param>
        /// <param name="pDateRealisation">La date de mise en ligne de la vidéo (ne peut pas être au-delà de la date du jour)</param>
        /// <param name="pDureeVideo">La durée de la vidéo</param>
        /// <param name="pAuteur">L'auteur de la vidéo</param>
        /// <param name="pActeur">L'acteur de la vidéo</param>
        /// <param name="pExtrait">Le path de l'extrait de la vidéo</param>
        /// <param name="pVideoComplet">Le path du vidéo complet</param>
        /// <param name="pImage">Le path de l'image qui représente la vidéo</param>
        public Video(int pIdVideo, string pTitre, EnumAnimal pTypeVideo, double pCoteEvaluation, DateOnly pDateRealisation,
            double pDureeVideo, string pAuteur, string pActeur, string pExtrait, string pVideoComplet, string pImage)
        {
            IdVideo = pIdVideo; // unique
            Titre = pTitre;
            TypeVideo = pTypeVideo;
            CoteEvaluation = pCoteEvaluation;
            DateMiseEnLigne = pDateRealisation;
            DureeVideo = pDureeVideo;
            Auteur = pAuteur;
            Acteur = pActeur;
            Extrait = pExtrait;
            VideoComplet = pVideoComplet;
            Image = pImage;
        }

        ///// <summary>
        ///// Permet de calculer la cote d'évaluation moyenne d'une liste d'évaluation afin de la placer dans le champs _coteEvaluation 
        ///// </summary>
        ///// <param name="listeEval">La liste des évaluations</param>
        //public void calculerCoteEvaluation(List<Evaluation> listeEval)
        //{
        //    int count = 0;
        //    IEnumerable<Evaluation> query =
        //    from eval in listeEval
        //    where eval.IdVideo.Equals(this.IdVideo)
        //    select eval;

        //    double cote = 0;

        //    foreach (Evaluation eval in query)
        //    {
        //        cote += (double)eval.CoteDonne;
        //    }
        //    count = query.Count();
        //    if(count == 0)
        //    {
        //        cote = -1;
        //    }
        //    else
        //    {
        //        cote /= query.Count();
        //    }

            

        //    this._coteEvaluation = cote;
        //}

        /// <summary>
        /// Méthode ToString de la classe
        /// </summary>
        /// <returns>string: chaine représentant la vidéo avec tous ses champs</returns>
        public override string ToString()
        {
            string retour = "Vidéo #" + IdVideo + " :\nTitre: " + Titre + "\nType de vidéo: " + TypeVideo + "\nCote d'évaluation: " +
                CoteEvaluation + "\nDate de mise en ligne: " + DateMiseEnLigne + "\nDurée: " + DureeVideo + "\nAuteur: " + Auteur +
                "\nActeur: " + Acteur + "\nPath de l'extrait: " + Extrait + "\nPath de la vidéo complète: " + VideoComplet + "\nPath de l'image: " + Image + "\n";

            return retour;
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
