using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Prog_Graph_BDD_A2024.Classes
{
    internal class Activite : IEquatable<Activite>, IComparable<Activite>
    {
        int idActivite;
        string nom;
        double coutOrganisation;
        double prixVente;
        double noteEvaluation;
        int idCategorie;
        string idAdmin;
        string url;
        string description;
        int nbrNotes;

        public Activite()
        {
            nom = "";
            coutOrganisation = 0;
            prixVente = 0;
            noteEvaluation = 0;
            idAdmin = "1000";
            url = "";
            description = "";
            nbrNotes = 0;
        }

        public Activite(int idActivite, string nom, double coutOrganisation, double prixVente, double noteEvaluation, int idCategorie, string idAdmin, string url, string description, int nbrNotes)
        {
            this.idActivite = idActivite;
            this.nom = nom;
            this.coutOrganisation = coutOrganisation;
            this.prixVente = prixVente;
            this.noteEvaluation = noteEvaluation;
            this.idCategorie = idCategorie;
            this.idAdmin = idAdmin;
            this.url = url;
            this.description = description;
            this.nbrNotes = nbrNotes;
        }

        public int IdActivite
        {
            get { return idActivite; }
            set { idActivite = value; }
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

        public double NoteEvaluation
        {
            get { return noteEvaluation; }
            set { noteEvaluation = value; }
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
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int NbrNotes
        {
            get { return nbrNotes; }
            set { nbrNotes = value; }
        }

        public int CompareTo(Activite other)
        {
            if (this.idActivite > other.idActivite)
                return 1;
            else if (this.idActivite < other.idActivite)
                return -1;
            else
                return 0;
        }

        public bool Equals(Activite other)
        {
            if (this.idActivite.Equals(other.idActivite) && this.nom == other.nom)
                return true;
            else
                return false;
        }

        public string StringCSV
        {
            get
            {
                return $"{IdActivite};{Nom};{CoutOrganisation};{PrixVente};{IdCategorie};{IdAdmin};{Url};{Description}";
            }
        }

        public double noteEvaluationMoyenne
        {
            get 
            {
                return Singleton_BD.getInstance().getNoteMoyenne(idActivite);
            }
        }

        public int nbrAdhAct
        {
            get
            {
                return Singleton_BD.getInstance().getNbrAdhAct(idActivite);
            }
        }
    }
}
