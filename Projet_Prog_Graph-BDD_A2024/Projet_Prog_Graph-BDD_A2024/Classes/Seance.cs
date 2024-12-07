using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Prog_Graph_BDD_A2024
{
    internal class Seance: IEquatable<Seance>, IComparable<Seance>
    {
        int idSeance;
        DateTime dateOrganisation;
        int heureOrganisation;
        int nbrPlaceDisponible;
        double noteAppreciation;
        string idAdmin;
        int idActivite;

        public Seance()
        {
            dateOrganisation = new DateTime();
            heureOrganisation = 0;
            nbrPlaceDisponible = 0;
            idAdmin = "1000";
        }

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
        public int IdActivite
        {
            get { return idActivite; }
            set { idActivite = value; }
        }

        public int CompareTo(Seance other)
        {
            if (this.idActivite > other.idActivite)
                return 1;
            else if (this.idActivite < other.idActivite)
                return -1;
            else
                return 0;
        }

        public bool Equals(Seance other)
        {
            if (this.idSeance.Equals(other.idSeance) && this.idActivite == other.idActivite)
                return true;
            else
                return false;
        }


    }
}
