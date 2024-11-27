using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_Prog_Graph_BDD_A2024
{
    internal class Singleton_BD
    {
        ObservableCollection<Activite> listeActivites;
        ObservableCollection<Adherents> listeAdherents;
        ObservableCollection<Adherents_Seance> listeAdherentsSeances;
        ObservableCollection<Administrateur> listeAdministrateurs;
        ObservableCollection<Categorie> listeCategories;
        ObservableCollection<Seance> listeSeances;
        ObservableCollection<Statistiques> listeStatistiques;

        static Singleton_BD instance = null;
        MySqlConnection con;

        public Singleton_BD()
        {
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420335ri_eq12;Uid=1238963;Pwd=1238963;");
            listeActivites = new ObservableCollection<Activite>();
            listeAdherents = new ObservableCollection<Adherents>();
            listeAdherentsSeances = new ObservableCollection<Adherents_Seance>();
            listeAdministrateurs = new ObservableCollection<Administrateur>();
            listeCategories = new ObservableCollection<Categorie>();
            listeSeances = new ObservableCollection<Seance>();
            listeStatistiques = new ObservableCollection<Statistiques>();
        }

        public static Singleton_BD getInstance()
        {
            if (instance == null)
                instance = new Singleton_BD();

            return instance;
        }

        public ObservableCollection<Activite> getListeActivites()
        {
            return listeActivites;
        }

        public ObservableCollection<Adherents> getListeAdherents()
        {
            return listeAdherents;
        }

        public ObservableCollection<Adherents_Seance> getListeAdherentsSeance()
        {
            return listeAdherentsSeances;
        }

        public ObservableCollection<Administrateur> getListeAdministrateur()
        {
            return listeAdministrateurs;
        }

        public ObservableCollection<Categorie> getListeCategorie()
        {
            return listeCategories;
        }

        public ObservableCollection<Seance> getListeSeance()
        {
            return listeSeances;
        }

        public ObservableCollection<Statistiques> getListeStatistiques()
        {
            return listeStatistiques;
        }
    }
}
