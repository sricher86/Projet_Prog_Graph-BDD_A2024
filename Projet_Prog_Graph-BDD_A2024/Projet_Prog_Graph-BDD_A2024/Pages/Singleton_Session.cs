using Projet_Prog_Graph_BDD_A2024.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Prog_Graph_BDD_A2024.Pages
{
    internal class Singleton_Session : INotifyPropertyChanged
    {
        static Singleton_Session instance = null;
        public static bool userConnected;
        public static bool admin;
        public static string pageCourante;
        public static Activite activiteCourante;
        public static Adherents adherentConnecte;
        public static Administrateur adminConnecte;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Singleton_Session()
        {
            adherentConnecte = new Adherents();
            adherentConnecte = null;
            userConnected = false;
            pageCourante = "";
        }

        public static Singleton_Session getInstance()
        {
            if (instance == null)
                instance = new Singleton_Session();

            return instance;
        }


        public void adherentConn(Adherents adh)
        {
            adherentConnecte = adh;
            UserConnected = true;
        }

        public void adherentDeconn()
        {
            adherentConnecte = null;
            UserConnected = false;
        }

        public void adminConn(Administrateur adm)
        {
            adminConnecte = adm;
            admin = true;
        }

        public void adminDeconn()
        {
            adminConnecte = null;
            admin = false;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public static string PageCourante
        {
            get;
            set;
        }

        public static Activite ActiviteCourante
        {
            get;
            set;
        }

        public static bool UserConnected
        {
            get { return userConnected; }
            set
            {
                userConnected = value;
                //userConnected.OnPropertyChanged(nameof(UserConnected));
            }
        }

        public static Adherents AdherentConnecte
        {
            get { return adherentConnecte; }
            set { adherentConnecte = value; }
        }
    }
}
