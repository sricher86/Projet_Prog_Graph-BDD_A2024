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
using System.ComponentModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Prog_Graph_BDD_A2024.Pages
{
    public sealed partial class PageDetail : Page, INotifyPropertyChanged
    {
        Activite activite;
        Adherents adherent;
        ObservableCollection<Seance> seancesActivite;
        ObservableCollection<Seance> seances;
        ObservableCollection<Adherents_Seance> adherents_Seances;
        ObservableCollection<Seance> seancesDisponible;

        int nbrPlaces;
        string seanceDisponible;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int NbrPlaces
        {
            get { return nbrPlaces; }
            set { this.nbrPlaces = value; }
        }

        public PageDetail()
        {
            this.InitializeComponent();
            adherent = Singleton_Session.AdherentConnecte;
            Singleton_BD.getInstance().getActivites();
            Singleton_BD.getInstance().getSeances();
            Singleton_BD.getInstance().getAdherentsSeances();
            if (Singleton_Session.UserConnected)
            {
                seancesDisponible = new ObservableCollection<Seance>();

            }
            seances = Singleton_BD.getInstance().getListeSeance();
            adherents_Seances = Singleton_BD.getInstance().getListeAdherentsSeance();

            //if (Singleton_BD.getInstance().UserConnected) noteEval.IsEnabled = true;
            //else noteEval.IsEnabled = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                activite = (Activite)e.Parameter;
                seancesDisponible = Singleton_BD.getInstance().getListeSeancesDispo(activite.IdActivite);
                ObservableCollection<Adherents_Seance> adherentsSeanceParActivite = Singleton_BD.getInstance().getAdherentSeanceParActivite(activite.IdActivite);

                if (seancesDisponible.Count > 0) calDates.ItemsSource = Singleton_BD.getInstance().getListeSeancesDispo(activite.IdActivite);
                else seanceDisponible = "Aucune s�ances disponible pour l'instant";
            }

            noteEval.Value = Singleton_BD.getInstance().getNoteMoyenne(activite.IdActivite);
        }

        private async void calDates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calDates.SelectedItem != null)
            {
                if (Singleton_Session.UserConnected)
                {
                    Seance seance = calDates.SelectedItem as Seance;
                    Singleton_BD.getInstance().SeanceInscription = seance;
                    DialogInscription dialog = new DialogInscription();
                    dialog.XamlRoot = this.XamlRoot;
                    dialog.Title = "Inscription";
                    dialog.PrimaryButtonText = "Envoyer l'inscription";
                    dialog.CloseButtonText = "Annuler";
                    dialog.DefaultButton = ContentDialogButton.Close;
                    ContentDialogResult resultat = await dialog.ShowAsync();
                    Frame.Navigate(typeof(PagePublique));
                }
                else
                {
                    DialogErreur erreur = new DialogErreur();
                    erreur.XamlRoot = this.XamlRoot;
                    erreur.Title = "Mode visiteur";
                    erreur.CloseButtonText = "Ok";
                    erreur.DefaultButton = ContentDialogButton.Close;
                    ContentDialogResult resultat = await erreur.ShowAsync();
                    calDates.SelectedIndex = -1;
                    //erreur.Content = "mon contenu";
                }
            }
        }

        private void buttonRetourner_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PagePublique));
        }

        private void noteEval_ValueChanged(RatingControl sender, object args)
        {
            Singleton_BD.getInstance().modifierNote(Singleton_Session.ActiviteCourante.IdActivite, noteEval.Value);
            noteEval.Value = Singleton_BD.getInstance().getNoteMoyenne(Singleton_Session.ActiviteCourante.IdActivite);
        }

        public bool UserConnected
        {
            get { return Singleton_Session.UserConnected; }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
