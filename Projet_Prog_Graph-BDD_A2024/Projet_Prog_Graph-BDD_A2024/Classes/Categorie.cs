using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Prog_Graph_BDD_A2024
{
    internal class Categorie
    {
        int idCategorie;
        string type;
        string idAdmin;

        public Categorie(int idCategorie, string type, string idAdmin)
        {
            this.idCategorie = idCategorie;
            this.type = type;
            IdAdmin = idAdmin;
        }

        public int IdCategorie
        {
            get { return idCategorie; }
            set { idCategorie = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string IdAdmin
        {
            get { return idAdmin; }
            set { idAdmin = value; }
        }
    }
}
