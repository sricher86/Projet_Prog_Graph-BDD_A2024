using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Prog_Graph_BDD_A2024
{
    public sealed partial class DialogAdminAjoutAdherent : ContentDialog
    {
        public DialogAdminAjoutAdherent()
        {
            this.InitializeComponent();
        }

        Adherents newAdherent = new Adherents();

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (tbx_nom.Text.Trim() != "")
                newAdherent.Nom = tbx_nom.Text;
            else
                tbx_erreur_nom.Text = "Le nom ne peut pas être vide";

            if (tbx_prenom.Text.Trim() != "")
                newAdherent.Prenom = tbx_prenom.Text;
            else
                tbx_erreur_prenom.Text = "Le prénom ne peut pas être vide";

            if (tbx_adresse.Text.Trim() != "")
                newAdherent.Adresse = tbx_adresse.Text;
            else
                tbx_erreur_adresse.Text = "L'adresse ne peut pas être vide";

            if (tbx_date.Date.ToString().Trim() != "")
                newAdherent.DateDeNaissance = new DateTime(tbx_date.Date.Year, tbx_date.Date.Month, tbx_date.Date.Day);
            else
                tbx_erreur_date.Text = "La date de naissance ne peut pas être vide";
        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (args.Result == ContentDialogResult.Primary)
            {
                if (string.IsNullOrWhiteSpace(tbx_erreur_nom.Text))
                    args.Cancel = true;
                if (string.IsNullOrWhiteSpace(tbx_erreur_prenom.Text))
                    args.Cancel = true;
                if (string.IsNullOrWhiteSpace(tbx_erreur_adresse.Text))
                    args.Cancel = true;
                if (string.IsNullOrWhiteSpace(tbx_erreur_date.Text))
                    args.Cancel = true;
            }
            else
                args.Cancel = false;
        }
    }
}
