using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_Prog_Graph_BDD_A2024.Classes
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
        ObservableCollection<Seance> listeSeancesDisponible;

        List<String> listeTypeCategories;
        List<String> listeNomsActivites;
        List<DateTime> listeDateSeances;

        static Singleton_BD instance = null;
        Adherents adherentConnecte;
        Adherents adherentInscription;
        Seance seanceInscription;
        Activite activiteSelectionne;
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
            listeSeancesDisponible = new ObservableCollection<Seance>();
            listeTypeCategories = new List<String>();
            listeNomsActivites = new List<String>();
            listeDateSeances = new List<DateTime>();

            AdherentConnecte = new Adherents();

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

        public Adherents AdherentConnecte
        {
            get { return adherentConnecte; }
            set { this.adherentConnecte = value; }
        }

        public Seance SeanceInscription
        {
            get { return seanceInscription; }
            set { this.seanceInscription = value; }
        }

        public ObservableCollection<Seance> getListeSeancesDispo(int idAct)
        {
            try
            {
                listeSeancesDisponible.Clear();
                MySqlCommand commande = new MySqlCommand();
                MySqlDataReader reader;

                con.Open();
                commande.Connection = con;
                commande.CommandText = "Call getSeances('"+adherentConnecte.No_identification+"', "+idAct+");";
                Debug.WriteLine(commande.CommandText);
                reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    int idSeance = reader.GetInt32("idSeance");
                    DateTime dateOrganisation = reader.GetDateTime("dateOrganisation");
                    int heureOrganisation = reader.GetInt32("heureOrganisation");
                    int nbrPlaceDisponible = reader.GetInt32("nbrPlaceDisponible");
                    int noteAppreciation = reader.GetInt32("noteAppreciation");
                    string idAdmin = reader.GetString("idAdmin").ToString();
                    int idActivite = reader.GetInt32("idActivite");

                    Seance nouvelleSeance = new Seance(idSeance, dateOrganisation, heureOrganisation, nbrPlaceDisponible, noteAppreciation, idAdmin, idActivite);

                    listeSeancesDisponible.Add(nouvelleSeance);
                }
                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                con.Close();
            }

            return listeSeancesDisponible;
        }

        public void inscriptionAdherent()
        {
            if (adherentConnecte != null && seanceInscription != null)
            {
                try
                {
                    string no_identification = adherentConnecte.No_identification;
                    int idSeance = seanceInscription.IdSeance;
                    int idActivite = seanceInscription.IdActivite;

                    MySqlCommand commande = new MySqlCommand();
                    commande.Connection = con;
                    commande.CommandText = "Insert into Adherents_Seances values (@no_identification, @idSeance, @idActivite);";
                    commande.Parameters.AddWithValue("@no_identification", no_identification);
                    commande.Parameters.AddWithValue("@idSeance", idSeance);
                    commande.Parameters.AddWithValue("@idActivite", idActivite);

                    con.Open();
                    commande.Prepare();
                    commande.ExecuteNonQuery();
                    con.Close();

                    modifierNbrPlaces(seanceInscription);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    con.Close();
                }
            }
        }

        public void modifierNbrPlaces (Seance seance)
        {
            if (seance != null)
            {
                try
                {
                    int idSeance = seance.IdSeance;

                    MySqlCommand commande = new MySqlCommand();
                    commande.Connection = con;
                    commande.CommandText = "Update seances set nbrPlaceDisponible = nbrPlaceDisponible -1 where idSeance = "+idSeance+";";

                    con.Open();
                    commande.Prepare();
                    commande.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    con.Close();
                }
            }

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

        public List<String> getListeNomsActivites()
        {
            return listeNomsActivites;
        }

        public List<DateTime> getListeDateSeances()
        {
            return listeDateSeances;
        }

        public void getActivites()
        {
            try
            {
                listeActivites.Clear();

                int idActivite = 0;
                string nom = "";
                double coutOrganisation = 0.00;
                double prixVente = 0.00;
                double noteEvaluation = 0;
                int idCategorie = 0;
                string idAdmin = "";
                string url = "";
                string description = "";
                int nbrEvaluations = 0;

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
                    noteEvaluation = reader.GetDouble("noteEvaluation");
                    idCategorie = reader.GetInt32("idCategorie");
                    idAdmin = reader.GetString("idAdmin").ToString();
                    description = reader.GetString("description").ToString();
                    nbrEvaluations = reader.GetInt32("nbrEvaluations");
                    Activite activite = new Activite(idActivite, nom, coutOrganisation, prixVente, noteEvaluation, idCategorie, idAdmin, url, description, nbrEvaluations);
                    listeActivites.Add(activite);

                    listeNomsActivites.Add(nom);
                }

                reader.Close();
                con.Close();

                foreach (Activite a in listeActivites)
                {
                    con.Open();
                    commande.Connection = con;

                    commande.CommandText = "Select getUrl(" + a.IdActivite + ") as url;";

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
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                con.Close();
            }
        }
        public void getAdherents()
        {
            try
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
                    string no_identification = reader.GetString("no_identification").ToString();
                    string nom = reader.GetString("nom").ToString();
                    string prenom = reader.GetString("prenom").ToString();
                    string adresse = reader.GetString("adresse").ToString();
                    DateTime dateDeNaissance = reader.GetDateTime("dateDeNaissance").Date;
                    int age = reader.GetInt32("age");
                    string idAdmin = reader.GetString("idAdmin").ToString();

                    Adherents adherent = new Adherents(no_identification, nom, prenom, adresse, dateDeNaissance, age, idAdmin);

                    listeAdherents.Add(adherent);
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                con.Close();
            }
        }

        public void getAdherentsSeances()
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                con.Close();
            }
        }

        public void getAdmins()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                con.Close();
                {

                }
            }
        }
        public void getCategories()
        {
            try
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
                    String idAdmin = reader.GetString("idAdmin").ToString();
                    String url = reader.GetString("url").ToString();

                    Categorie cat = new Categorie(idCategorie, type, idAdmin, url);
                    listeCategories.Add(cat);

                    listeTypeCategories.Add(type);
                }

                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                con.Close();
            }
        }


        public void addActivite(Activite activite)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();

                commande.Connection = con;
                commande.CommandText = "INSERT INTO activites(idActivite, nom, coutOrganisation, prixVente, idCategorie, idAdmin, description) VALUES(@idActivite, @nom, @coutOrganisation, @prixVente, @idCategorie, @idAdmin, @description)";
                commande.Parameters.AddWithValue("@idActivite", activite.IdActivite);
                commande.Parameters.AddWithValue("@nom", activite.Nom);
                commande.Parameters.AddWithValue("@coutOrganisation", activite.CoutOrganisation);
                commande.Parameters.AddWithValue("@prixVente", activite.PrixVente);
                commande.Parameters.AddWithValue("@idCategorie", activite.IdCategorie);
                commande.Parameters.AddWithValue("@idAdmin", activite.IdAdmin);
                commande.Parameters.AddWithValue("@description", activite.Description);

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();

                getActivites();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        public void addCategorie(Categorie categorie)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();

                commande.Connection = con;
                commande.CommandText = "INSERT INTO categories (idCategorie, type, idAdmin, url) VALUES(@idCategorie, @type, @idAdmin, @url)";
                commande.Parameters.AddWithValue("@idCategorie", categorie.IdCategorie);
                commande.Parameters.AddWithValue("@type", categorie.Type);
                commande.Parameters.AddWithValue("@idAdmin", categorie.IdAdmin);
                commande.Parameters.AddWithValue("@url", categorie.Url);

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();

                getCategories();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }
        public void getSeances()
        {
            try
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
                    listeDateSeances.Add(seance.DateOrganisation);
                }

                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                con.Close();
            }
        }

        public void getStatistiques()
        {
            try
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
                    int idStatistiques = reader.GetInt32("idStatistiques");
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                con.Close();
            }
        }

        public ObservableCollection<Seance> getSeanceActivites(int idActivite)
        {
            ObservableCollection<Seance> seanceActivite = new ObservableCollection<Seance>();
            listeSeances.Where(idActivite => listeSeances.Contains(idActivite)).ToList().ForEach(seanceActivite.Add);

            return seanceActivite;
        }

        public ObservableCollection<Adherents_Seance> getAdherentSeanceParActivite(int idActivite)
        {
            ObservableCollection<Adherents_Seance> adherentSeanceActivite = new ObservableCollection<Adherents_Seance>();
            listeAdherentsSeances.Where(idActivite=> listeAdherentsSeances.Contains(idActivite)).ToList().ForEach(adherentSeanceActivite.Add);

            return adherentSeanceActivite;
        }

        public double getNoteMoyenne (int idAct)
        {
            double moyenne = 0;
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                //commande.CommandText = "select getMoyenne("+idAct+") as moyenne";
                commande.CommandText = "SELECT noteAppreciation FROM afficherNote WHERE idActivite = " + idAct;

                con.Open();

                MySqlDataReader reader = commande.ExecuteReader();

                //read values from SQL command and store in vars, in order to add values to Equipe list
                while (reader.Read())
                {
                    //moyenne = reader.GetDouble("moyenne");
                    moyenne = reader.GetDouble("noteAppreciation");
                }
                reader.Close();
                con.Close();

                return moyenne;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                con.Close();
            }
            return moyenne;
        }

        public void modifierNotes(int idAct, double nouvelleNote)
        {
            double moyenne = 0;
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "call modNotes(@idAct, nouvelleNote);";

                commande.Parameters.AddWithValue("@idAct", idAct);
                commande.Parameters.AddWithValue("@nouvelleNote", nouvelleNote);

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                con.Close();
            }
        }

        public int getNbrAdhAct(int idAct)
        {
            int nbrParticipants = 0;
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT nbrParticipants FROM nbrAdhAct JOIN activites act on nbrAdhAct.activite = act.nom WHERE idActivite = " + idAct;

                con.Open();

                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    nbrParticipants = reader.GetInt32("nbrParticipants");
                }
                reader.Close();
                con.Close();

                return nbrParticipants;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                con.Close();
            }
            return nbrParticipants;
        }

        public string getAdhPlusActif()
        {
            string partPlusActif = "";
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT CONCAT(prenom, ' ', nom, ' (', a.no_identification, ') -> ', nbrSeances, ' séances') AS partPlusActif " +
                    "FROM partPlusActif JOIN adherents a on partPlusActif.no_identification = a.no_identification";

                con.Open();

                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    partPlusActif = reader.GetString("partPlusActif");
                }
                reader.Close();
                con.Close();

                return partPlusActif;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                con.Close();
            }
            return partPlusActif;
        }

        public double getNoteMoyenneActs()
        {
            double moyenne = 0;
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM moyenneNotesAppAct";

                con.Open();

                MySqlDataReader reader = commande.ExecuteReader();

                //read values from SQL command and store in vars, in order to add values to Equipe list
                while (reader.Read())
                {
                    moyenne = reader.GetDouble("moyenneNotesApp");
                }
                reader.Close();
                con.Close();

                return moyenne;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                con.Close();
            }
            return moyenne;
        }

        public string getAdhPlusAge()
        {
            string adherentPlusAge = "";
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT CONCAT(adherentPlusAge(), ' (', no_identification, ') -> ', age, ' ans') AS adherentPlusAge " +
                    "FROM adherents WHERE CONCAT(prenom, ' ', nom) = adherentPlusAge()";

                con.Open();

                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    adherentPlusAge = reader.GetString("adherentPlusAge");
                }
                reader.Close();
                con.Close();

                return adherentPlusAge;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                con.Close();
            }
            return adherentPlusAge;
        }
    }
}
