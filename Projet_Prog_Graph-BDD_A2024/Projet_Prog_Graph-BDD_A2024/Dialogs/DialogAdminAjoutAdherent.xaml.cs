using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Projet_Prog_Graph_BDD_A2024.Classes;
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
    public sealed partial class DialogAdminAjoutAdherent : ContentDialog
    {
        bool valide = true;

        string no_identification = "";
        string nom;
        string prenom;
        string adresse;
        int age = 0;
        DateTime dateDeNaissance;
        string idAdmin = "1000";

        public DialogAdminAjoutAdherent()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            resetErreurs();

            if (tbx_nom.Text.Trim() != "")
                nom = tbx_nom.Text;
            else
            {
                tbx_erreur_nom.Text = "Le nom ne peut pas être vide";
                valide = false;
            }

            if (tbx_prenom.Text.Trim() != "")
                prenom = tbx_prenom.Text;
            else
            {
                tbx_erreur_prenom.Text = "Le prénom ne peut pas être vide";
                valide = false;
            }

            if (tbx_adresse.Text.Trim() != "")
                adresse = tbx_adresse.Text;
            else
            {
                tbx_erreur_adresse.Text = "L'adresse ne peut pas être vide";
                valide = false;
            }

            if (tbx_date.SelectedDate.HasValue == true)
                dateDeNaissance = new DateTime(tbx_date.Date.Year, tbx_date.Date.Month, tbx_date.Date.Day);
            else
            {
                tbx_erreur_date.Text = "La date de naissance ne peut pas être vide";
                valide = false;
            }

            if (valide == true)
            {
                Singleton_BD.getInstance().addAdherent(new Adherents(no_identification, nom, prenom, adresse, dateDeNaissance, age, idAdmin));
            }
        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (args.Result == ContentDialogResult.Primary)
            {
                if (valide == false)
                    args.Cancel = true;
            }
            else
                args.Cancel = false;
        }

        private void resetErreurs()
        {
            tbx_erreur_nom.Text = String.Empty;
            tbx_erreur_prenom.Text = String.Empty;
            tbx_erreur_adresse.Text = String.Empty;
            tbx_erreur_date.Text = String.Empty;
        }
    }
}
