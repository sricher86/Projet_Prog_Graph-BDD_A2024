using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Prog_Graph_BDD_A2024
{
    internal class Activite
    {
        int idActivite;
        string nom;
        double coutOrganisation;
        double prixVente;
        int idCategorie;
        string idAdmin;

        public Activite(int idActivite, string nom, double coutOrganisation, double prixVente, int idCategorie, string idAdmin)
        {
            this.idActivite = idActivite;
            this.nom = nom;
            this.coutOrganisation = coutOrganisation;
            this.prixVente = prixVente;
            this.idCategorie = idCategorie;
            this.idAdmin = idAdmin;
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public double CoutOrganisation
        {
            get { return coutOrganisation; }
            set { coutOrganisation = value; }
        }

        public double PrixVente
        {
            get { return prixVente; }
            set { prixVente = value; }
        }

        public int IdCategorie
        {
            get { return idCategorie; }
            set { idCategorie = value; }
        }

        public string IdAdmin
        {
            get { return idAdmin; }
            set { idAdmin = value; }
        }

    }
}
