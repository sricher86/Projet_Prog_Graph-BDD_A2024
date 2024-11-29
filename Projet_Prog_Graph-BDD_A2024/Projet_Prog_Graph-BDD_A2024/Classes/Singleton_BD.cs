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
        public void getActivites()
        {
            listeActivites.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from Activites";

            con.Open();

            MySqlDataReader reader = commande.ExecuteReader();

            //read values from SQL command and store in vars, in order to add values to Equipe list
            while (reader.Read())
            {
                int idActivite= reader.GetInt32("idActivite");
                String nom = reader.GetString("nom").ToString();
                double coutOrganisation = reader.GetDouble("coutOrganisation");
                double prixVente = reader.GetDouble("prixVente");
                int idCategorie = reader.GetInt32("idCategorie");
                string idAdmin= reader.GetString("idAdmin").ToString();


                Activite activite = new Activite(idActivite, nom, coutOrganisation, prixVente, idCategorie, idAdmin);

                listeActivites.Add(activite);
            }

            reader.Close();
            con.Close();
        }
        public void getAdherents()
        {
            listeAdherents.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from Adherents";

            con.Open();

            MySqlDataReader reader = commande.ExecuteReader();

            //read values from SQL command and store in vars, in order to add values to Equipe list
            while (reader.Read())
            {
                string no_identification= reader.GetString("no_identification").ToString();
                string nom = reader.GetString("nom").ToString();
                string prenom = reader.GetString("prenom").ToString();
                string adresse = reader.GetString("adresse").ToString();
                DateTime dateDeNaissance = reader.GetDateTime("dateDeNaissance");
                int age = reader.GetInt32("age");
                string idAdmin = reader.GetString("idAdmin").ToString() ;

                Adherents adherent = new Adherents(no_identification, nom, prenom, adresse, dateDeNaissance, age, idAdmin);

                listeAdherents.Add(adherent);
            }

            reader.Close();
            con.Close();
        }

        public void getAdherentsSeances()
        {
            listeAdherentsSeances.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from Adherents_Seances";

            con.Open();

            MySqlDataReader reader = commande.ExecuteReader();

            //read values from SQL command and store in vars, in order to add values to Equipe list
            while (reader.Read())
            {
                string no_identification = reader.GetString("no_identification").ToString();
                int idSeance = reader.GetInt32("idSeance");
                int idActivite = reader.GetInt32("idActivite");
                int idCategorie = reader.GetInt32("idCategorie");
                string idAdmin = reader.GetString("idAdmin").ToString();


                Activite activite = new Activite(idActivite, nom, coutOrganisation, prixVente, idCategorie, idAdmin);

                listeActivites.Add(activite);
            }

            reader.Close();
            con.Close();
        }

        public void getCategories()
        {
            listeCategories.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from Categories";

            con.Open();

            MySqlDataReader reader = commande.ExecuteReader();

            //read values from SQL command and store in vars, in order to add values to Equipe list
            while (reader.Read())
            {
                int idCategorie = reader.GetInt32("idCategorie");
                String type = reader.GetString("type").ToString();
                String idAdmin= reader.GetString("idAdmin").ToString();
                String url = reader.GetString("url").ToString();

                Categorie cat= new Categorie(idCategorie,type, idAdmin,url);
                listeCategories.Add(cat);
            }

            reader.Close();
            con.Close();
        }
    }
}
