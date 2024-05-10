using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using NUnit.Framework;
using ProjetCatalogue.Models;

namespace TestProjetCatalogue
{
    public class TestGestionFavori
    {
        private Utilisateur utilisateur;
        private Video video;
        private GestionFavori gestion;
        private GestionUtilisateur gestionUtilisateur;
        private Catalogue catalogue;
        private GestionContext gestionContext;

        [OneTimeSetUp]
        public void BaseSetUp()
        {
            gestionContext = new GestionContext();
            this.utilisateur = new Utilisateur("pseudoTest", "Mot_de_passe1!");
            gestionUtilisateur = new GestionUtilisateur();
            gestionUtilisateur.AjouterUtilisateur(utilisateur, out String? message);
            this.catalogue = new Catalogue();
            this.video = new Video("Alloa",EnumAnimal.Autre,-1,DateTime.Now,1,"auteur","acteur", "","","");
            catalogue.DbSetVideos.Add(this.video);
            catalogue.GestionContext.SaveChanges();
            gestionContext.SaveChanges();

            this.gestion = new GestionFavori();
        }

        [OneTimeTearDown]
        public void BaseTearDown()
        {
            gestion.SupprimerFavorisUtilisateur(utilisateur);
            gestionUtilisateur.SupprimerUtilisateur(utilisateur);
           
            catalogue.DbSetVideos.Remove(this.video);
            catalogue.GestionContext.SaveChanges();
 
            gestionContext.Dispose();
        }


        [Test]
        public void etantFavoriCorrectEtGestionFavoriCorrectEtQueFavoriPasPresent_quandModifierFavori_alorsContainsRetourneTrueEtFavoriAjoute()
        {
            Favori favori = new Favori(video.IdVideo, utilisateur.Pseudo);
            this.gestion.ModifierFavori(utilisateur, video);


            Assert.That(this.gestion.DbSetFavoris.ToList().Contains(favori), Is.True);
        }

        [Test]
        public void etantFavoriAjouteAyantLeMemePseudoUserEtLeMemeIdVideoQueFavoriDejaPresent_quandModifierFavori_alorsContainsRetourneFalseEtFavoriSupprime()
        {
            Favori favori = new Favori(video.IdVideo, utilisateur.Pseudo);
            gestionContext.Add(favori);
            gestionContext.SaveChanges();


            this.gestion.ModifierFavori(utilisateur, video);

            Assert.That(this.gestion.DbSetFavoris.ToList().Contains(favori), Is.False);

        }



    }
}
