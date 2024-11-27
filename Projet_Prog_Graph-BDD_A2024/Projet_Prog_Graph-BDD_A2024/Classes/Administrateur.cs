using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Prog_Graph_BDD_A2024
{
    internal class Administrateur
    {
        string idAdmin;
        string motDePasse;

        public Administrateur(string idAdmin, string motDePasse)
        {
            this.idAdmin = idAdmin;
            this.motDePasse = motDePasse;
        }

        public string IdAdmin { 
            get { return idAdmin; } 
            set { idAdmin = value; }
        }

        public string MotDePasse { 
            get { return motDePasse; } 
            set { motDePasse = value; } 
        }
    }
}
