using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue
{
    internal class Utilisateur
    {
        private string _pseudo;
        private string _motDePasse;
        private string _nom;
        private string _prenom;
        private enum Role
        { 
            UtilisateurSimple,
            Admin,
            Technicien
        }

    }
}
