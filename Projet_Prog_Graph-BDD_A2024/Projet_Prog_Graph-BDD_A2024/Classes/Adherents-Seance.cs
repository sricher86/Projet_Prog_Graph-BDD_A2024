using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Prog_Graph_BDD_A2024.Classes
{
    internal class Adherents_Seance
    {
        string no_identification;
        int idSeance;
        int idActivite;
        public Adherents_Seance(string no_identification, int idSeance, int idActivite)
        {
            this.no_identification = no_identification;
            this.idSeance = idSeance;
            this.idActivite = idActivite;
        }

        public String No_identification { 
            get { return no_identification; } 
            set { no_identification = value; } 
        } 
        public int IdSeance { 
            get {  return idSeance; } 
            set { idSeance = value; } 
        }
        public int IdActivite
        {
            get { return idActivite; }
            set { idActivite = value; }
        }

    }
}
