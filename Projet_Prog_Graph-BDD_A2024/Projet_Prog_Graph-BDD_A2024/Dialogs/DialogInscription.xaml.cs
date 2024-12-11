using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Projet_Prog_Graph_BDD_A2024.Classes;
using Projet_Prog_Graph_BDD_A2024.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Prog_Graph_BDD_A2024.Dialogs
{
    public sealed partial class DialogInscription : ContentDialog
    {
        Adherents adherentConnecte = Singleton_Session.AdherentConnecte;
        string no_identification;
        string prenom;
        string nom;
        string adresse;
        DateTime ddn;
        int age;
        bool fermer = false;

        public DialogInscription()
        {
            this.InitializeComponent();

            no_identification = adherentConnecte.No_identification;
            prenom = adherentConnecte.Prenom;
            nom = adherentConnecte.Nom;
            adresse = adherentConnecte.Adresse;
            ddn = adherentConnecte.DateDeNaissance;
            age = adherentConnecte.Age;

            noIDTxtBox.Text = no_identification;
            prenomTxtBox.Text = prenom;
            nomTxtBox.Text = nom;
            adresseTxtBox.Text = adresse;
            ddnTxtBox.Text = String.Format(DateTime.Now.ToString("dddd, dd MMMM yyyy"),ddn.ToString());
            ageTxtBox.Text = age.ToString();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Singleton_BD.getInstance().inscriptionAdherent();
        }
    
        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (args.Result == ContentDialogResult.Primary)
            {
                
            }
            else
                args.Cancel = false;
        }
    }
}
