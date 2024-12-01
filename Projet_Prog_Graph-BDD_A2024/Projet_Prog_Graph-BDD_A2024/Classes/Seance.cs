using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Prog_Graph_BDD_A2024
{
    internal class Seance
    {
        int idSeance;
        DateTime dateOrganisation;
        int heureOrganisation;
        int nbrPlaceDisponible;
        double noteAppreciation;
        string idAdmin;
        int idActivite;

        public Seance(int idSeance, DateTime dateOrganisation, int heureOrganisation, int nbrPlaceDisponible, double noteAppreciation, string idAdmin, int idActivite)
        {
            this.idSeance = idSeance;
            this.dateOrganisation = dateOrganisation;
            this.heureOrganisation = heureOrganisation;
            this.nbrPlaceDisponible = nbrPlaceDisponible;
            this.noteAppreciation = noteAppreciation;
            this.idAdmin = idAdmin;
            this.idActivite = idActivite;
        }

        public int IdSeance { 
            get { return idSeance; } 
            set { idSeance = value; }
        }

        public DateTime DateOrganisation { 
            get { return dateOrganisation; } 
            set { dateOrganisation = value; } 
        }

        public int HeureOrganisation { 
            get { return heureOrganisation; }
            set { heureOrganisation = value; }
        }

        public int NbrPlaceDisponible
        {
            get { return nbrPlaceDisponible; }
            set { nbrPlaceDisponible = value; }
        }

        public double NoteAppreciation { 
            get { return noteAppreciation; } 
            set { noteAppreciation = value; } 
        }

        public string IdAdmin { 
            get { return idAdmin; } 
            set { idAdmin = value; } 
        }
    }
}
