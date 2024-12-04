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

        List<String> listeTypeCategories;

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

            listeTypeCategories = new List<String>();

            getActivites();
            getAdherents();
            getAdherentsSeances();
            getAdmins();
            getCategories();
            getSeances();
            getStatistiques();
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

        public List<String> getListeTypeCategories()
        {
            return listeTypeCategories;
        }

        public void getActivites()
        {
            listeActivites.Clear();

            int idActivite = 0;
            string nom = "";
            double coutOrganisation = 0.00;
            double prixVente = 0.00;
            int idCategorie = 0;
            string idAdmin = "";
            string url = "";
            string description = "";

            MySqlCommand commande = new MySqlCommand();
            MySqlDataReader reader;

            con.Open();
            commande.Connection = con;
            commande.CommandText = "Select * from Activites;";
            reader = commande.ExecuteReader();

                    while (reader.Read())
                    {
                        idActivite = reader.GetInt32("idActivite");
                        nom = reader.GetString("nom").ToString();
                        coutOrganisation = reader.GetDouble("coutOrganisation");
                        prixVente = reader.GetDouble("prixVente");
                        idCategorie = reader.GetInt32("idCategorie");
                        idAdmin = reader.GetString("idAdmin").ToString();
                        description = reader.GetString("description").ToString();

                        Activite activite = new Activite(idActivite, nom, coutOrganisation, prixVente, idCategorie, idAdmin, url, description);
                        listeActivites.Add(activite);
                    }
                
            reader.Close();
            con.Close();

            foreach (Activite a in listeActivites)
            {
                con.Open();
                commande.Connection = con;

                commande.CommandText = "Select getUrl("+a.IdActivite+") as url;";

                reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    url = reader.GetString("url").ToString();
                    a.Url = url;
                }

                con.Close();
                reader.Close();
            }
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

                Adherents_Seance adherents_Seance = new Adherents_Seance(no_identification, idSeance, idActivite);

                listeAdherentsSeances.Add(adherents_Seance);
            }

            reader.Close();
            con.Close();
        }

        public void getAdmins()
        {
            listeAdministrateurs.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from Administrateurs";

            con.Open();

            MySqlDataReader reader = commande.ExecuteReader();

            //read values from SQL command and store in vars, in order to add values to Equipe list
            while (reader.Read())
            {   
                string idAdmin = reader.GetString("idAdmin").ToString();
                string motDePasse = reader.GetString("motDePasse").ToString();

                Administrateur administrateur = new Administrateur(idAdmin, motDePasse);

                listeAdministrateurs.Add(administrateur);
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

                listeTypeCategories.Add(type);
            }

            reader.Close();
            con.Close();
        }

        public void getSeances()
        {
            listeSeances.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from Seances";

            con.Open();

            MySqlDataReader reader = commande.ExecuteReader();

            //read values from SQL command and store in vars, in order to add values to Equipe list
            while (reader.Read())
            {
                int idSeance = reader.GetInt32("idSeance");
                DateTime dateOrganisation = reader.GetDateTime("dateOrganisation");
                int heureOrganisation = reader.GetInt32("heureOrganisation");
                int nbrPlaceDisponible = reader.GetInt32("nbrPlaceDisponible");
                int noteAppreciation = reader.GetInt32("noteAppreciation");
                string idAdmin = reader.GetString("idAdmin").ToString();
                int idActivite = reader.GetInt32("idActivite");

                Seance seance = new Seance(idSeance, dateOrganisation, heureOrganisation, nbrPlaceDisponible, noteAppreciation, idAdmin, idActivite);    

                listeSeances.Add(seance);
            }

            reader.Close();
            con.Close();
        }

        public void getStatistiques()
        {
            listeStatistiques.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from Statistiques";

            con.Open();

            MySqlDataReader reader = commande.ExecuteReader();

            //read values from SQL command and store in vars, in order to add values to Equipe list
            while (reader.Read())
            {
                int idStatistiques= reader.GetInt32("idStatistiques");
                int nbrTotalParticipants = reader.GetInt32("nbrTotalParticipants");
                string participantPlusActif = reader.GetString("participantPlusActif").ToString();
                double moyenneNoteAppreciation = reader.GetDouble("moyenneNoteApp");
                int idActivite = reader.GetInt32("idActivite");

                Statistiques statistique = new Statistiques(idStatistiques, nbrTotalParticipants, participantPlusActif, moyenneNoteAppreciation, idActivite);

                listeStatistiques.Add(statistique);
            }

            reader.Close();
            con.Close();
        }

        public ObservableCollection<Seance> getSeanceActivites(int idActivite)
        {
            ObservableCollection<Seance> seanceActivite = new ObservableCollection<Seance>();
            foreach (Seance s in listeSeances)
            {
                if (s.IdActivite == idActivite)
                {
                    seanceActivite.Add(s);
                }
            }
            return seanceActivite;
        }
    }
}
