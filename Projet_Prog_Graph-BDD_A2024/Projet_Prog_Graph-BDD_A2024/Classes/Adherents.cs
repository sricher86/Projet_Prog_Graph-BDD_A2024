using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Prog_Graph_BDD_A2024
{
    internal class Adherents
    {
        string no_identification;
        string nom;
        string prenom;
        string adresse;
        DateTime dateDeNaissance;
        int age;
        string idAdmin;

        public Adherents()
        {
            nom = "";
            prenom = "";
            adresse = "";
            dateDeNaissance = new DateTime();
            IdAdmin = "1000";
        }

        public Adherents(string no_identification, string nom, string prenom, string adresse, DateTime dateDeNaissance, int age, string idAdmin)
        {
            this.no_identification = no_identification;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.dateDeNaissance = dateDeNaissance;
            this.age = age;
            IdAdmin = idAdmin;
        }

        public string No_identification
        {
            get { return no_identification; }
            set { no_identification = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }

        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }

        public DateTime DateDeNaissance
        {
            get { return dateDeNaissance; }
            set { dateDeNaissance = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public string IdAdmin
        {
            get { return idAdmin; }
            set { idAdmin = value; }
        }
    }
}
