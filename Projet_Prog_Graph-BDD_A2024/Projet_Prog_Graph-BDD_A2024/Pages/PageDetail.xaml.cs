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
using System.Diagnostics;

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
        ObservableCollection<Seance> seancesDisponible;

        int nbrPlaces;
        string seanceDisponible;
        public int NbrPlaces
        {
            get { return nbrPlaces; }
            set { this.nbrPlaces = value; }
        }

        public PageDetail()
        {
            this.InitializeComponent();
            adherent = Singleton_BD.getInstance().AdherentConnecte;
            Singleton_BD.getInstance().getActivites();
            Singleton_BD.getInstance().getSeances();
            Singleton_BD.getInstance().getAdherentsSeances();
            seancesDisponible = new ObservableCollection<Seance>();
            seances = Singleton_BD.getInstance().getListeSeance();
            adherents_Seances = Singleton_BD.getInstance().getListeAdherentsSeance();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                activite = (Activite)e.Parameter;
                seancesDisponible = Singleton_BD.getInstance().getListeSeancesDispo(activite.IdActivite);
                ObservableCollection<Adherents_Seance> adherentsSeanceParActivite = Singleton_BD.getInstance().getAdherentSeanceParActivite(activite.IdActivite);
                foreach (Adherents_Seance ads in adherentsSeanceParActivite)
                {
                    if(ads.No_identification == adherent.No_identification)
                    {
                        noteLabel.Visibility = Visibility.Visible;
                        note.Visibility = Visibility.Visible;
                    }
                }
                Debug.WriteLine(seancesDisponible.Count);
                if (seancesDisponible.Count > 0) calDates.ItemsSource = Singleton_BD.getInstance().getListeSeancesDispo(activite.IdActivite);
                else seanceDisponible = "Aucune séances disponible pour l'instant";
            }

            noteMoyenne.Text = Singleton_BD.getInstance().getNoteMoyenne(activite.IdActivite).ToString();
        }

        private async void calDates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seance seance = calDates.SelectedItem as Seance;

            if (seance != null)
            {
                Debug.WriteLine(seance.IdSeance);
                Singleton_BD.getInstance().SeanceInscription = seance;
                DialogInscription dialog = new DialogInscription(nbrPlaces);
                dialog.XamlRoot = this.XamlRoot;
                dialog.Title = "Inscription";
                dialog.PrimaryButtonText = "Envoyer l'inscription";
                dialog.CloseButtonText = "Annuler";
                dialog.DefaultButton = ContentDialogButton.Close;
                ContentDialogResult resultat = await dialog.ShowAsync();
            }

            Frame.Navigate(typeof(PagePublique));
        }

        private void buttonRetourner_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PagePublique));
        }

        private void note_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (note.SelectedItem != null)
            {

            }
        }
    }
}
