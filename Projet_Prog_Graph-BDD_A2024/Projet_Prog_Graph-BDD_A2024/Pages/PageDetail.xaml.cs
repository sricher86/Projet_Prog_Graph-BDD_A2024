using Google.Protobuf.Compiler;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlX.XDevAPI;
using Projet_Prog_Graph_BDD_A2024.Dialogs;
using Projet_Prog_Graph_BDD_A2024.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Prog_Graph_BDD_A2024.Pages
{
    public sealed partial class PageDetail : Page
    {
        Activite activite;
        Adherents adherent;
        ObservableCollection<Seance> seancesActivite;
        ObservableCollection<Seance> seances;
        ObservableCollection<Adherents_Seance> adherents_Seances;
        List<Seance> seancesDisponible;
        int nbrPlaces;

        public int NbrPlaces
        {
            get {return nbrPlaces; }
            set { this.nbrPlaces = value; }
        }

        public PageDetail()
        {
            this.InitializeComponent();
            adherent = Singleton_BD.getInstance().AdherentConnecte;
            Singleton_BD.getInstance().getActivites();
            Singleton_BD.getInstance().getSeances();
            Singleton_BD.getInstance().getAdherentsSeances();
            seances = Singleton_BD.getInstance().getListeSeance();
            adherents_Seances = Singleton_BD.getInstance().getListeAdherentsSeance();
            seancesDisponible = new List<Seance>();
            int ctrS = 1;
            int ctrAS = 1;

            foreach (Seance s in seances)
            {
                foreach (Adherents_Seance ads in adherents_Seances)
                {
                    if (s.IdSeance != ads.IdSeance && ads.No_identification != adherent.No_identification && s.NbrPlaceDisponible > 0)
                    {
                        seancesDisponible.Add(s);
                    }
                    else 
                    {
                        erreur.Text = "ctrS: " + ctrS + "ctrAS: " + ctrAS + "s.Id seance: " + s.IdSeance + "ads.Id seance: " + ads.IdSeance + "ads.no_identification: " + ads.No_identification + "adherent.no_identification: " + adherent.No_identification; 
                    }
                }
                ctrS++;
                ctrAS++;    
            }

            calDates.ItemsSource = seancesDisponible;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                activite = (Activite)e.Parameter;
                //seancesActivite = Singleton_BD.getInstance().getSeanceActivites(activite.IdActivite);
                //ObservableCollection<DateTime> dates = new ObservableCollection<DateTime>();
                //foreach (Seance s in seancesActivite) dates.Add(s.DateOrganisation);
            }
        }

        private async void calDates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seance seance = calDates.SelectedItem as Seance;
            Singleton_BD.getInstance().NouvelleInscription = seance;
            nbrPlaces = seance.NbrPlaceDisponible;

            if (seance != null)
            {
                    DialogInscription dialog = new DialogInscription(nbrPlaces);
                    dialog.XamlRoot = this.XamlRoot;
                    dialog.Title = "Inscription";
                    dialog.PrimaryButtonText = "Envoyer l'inscription";
                    dialog.CloseButtonText = "Annuler";
                    dialog.DefaultButton = ContentDialogButton.Close;
                    ContentDialogResult resultat = await dialog.ShowAsync();
            }
        }
    }
}
