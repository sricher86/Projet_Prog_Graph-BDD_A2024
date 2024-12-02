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
        string url;

        public Categorie()
        {
            type = "";
            idAdmin = "1000";
            url = "";
        }

        public Categorie(int idCategorie, string type, string idAdmin, string url)
        {
            this.idCategorie = idCategorie;
            this.type = type;
            IdAdmin = idAdmin;
            this.url = url;
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

        public string Url { 
            get { return url; } 
            set { url = value; } 
        }
    }
}
