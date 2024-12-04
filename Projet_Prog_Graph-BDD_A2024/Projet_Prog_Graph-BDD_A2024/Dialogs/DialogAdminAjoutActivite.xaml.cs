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
    public sealed partial class DialogAdminAjoutActivite : ContentDialog
    {
        Activite newActivite = new Activite();

        Visibility visible = Visibility.Visible;
        Visibility collapse = Visibility.Collapsed;

        public DialogAdminAjoutActivite()
        {
            this.InitializeComponent();
            cbx_listeCategorie.ItemsSource = Singleton_BD.getInstance().getListeTypeCategories();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (tbx_nom.Text.Trim() != "")
                newActivite.Nom = tbx_nom.Text;
            else
                tbx_erreur_nom.Text = "Le nom ne peut pas �tre vide";


            if (Double.TryParse(tbx_cout.Text, out double cout))
            {
                if (tbx_cout.Text.Trim() != "")
                    newActivite.CoutOrganisation = cout;
                else
                    tbx_erreur_cout.Text = "Le co�t ne peut pas �tre vide";
            }
            else
                tbx_erreur_cout.Text = "Le co�t doit �tre en format d�cimal";


            if (Double.TryParse(tbx_prix.Text, out double prix))
            {
                if (tbx_prix.Text.Trim() != "")
                    newActivite.PrixVente = prix;
                else
                    tbx_erreur_prix.Text = "Le prix ne peut pas �tre vide";
            }
            else
                tbx_erreur_prix.Text = "Le prix doit �tre en format d�cimal";


            if (cbx_listeCategorie.Text.Trim() == "")
            {
                if (cbx_listeCategorie.SelectedIndex != -1)
                    newActivite.IdCategorie = (cbx_listeCategorie.SelectedIndex + 1);
                else
                    tbx_erreur_categorie.Text = "Veuillez choisir une cat�gorie";
            }

            if (tbx_url.Visibility == visible)
                if (tbx_url.Text.Trim() == "")
                    tbx_erreur_url.Text = "Le url ne peut pas �tre vide";
        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (args.Result == ContentDialogResult.Primary)
            {
                if (string.IsNullOrWhiteSpace(tbx_erreur_nom.Text))
                    args.Cancel = true;
                if (string.IsNullOrWhiteSpace(tbx_erreur_cout.Text))
                    args.Cancel = true;
                if (string.IsNullOrWhiteSpace(tbx_erreur_prix.Text))
                    args.Cancel = true;
                if (string.IsNullOrWhiteSpace(tbx_erreur_categorie.Text))
                    args.Cancel = true;
                if (string.IsNullOrWhiteSpace(tbx_erreur_url.Text))
                    args.Cancel = true;
            }
            else
                args.Cancel = false;
        }

        private void tbx_listeCategorie_TextSubmitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        {
            tbx_url.Visibility = visible;

            if (Singleton_BD.getInstance().getListeTypeCategories().Contains(cbx_listeCategorie.Text))
            {
                newActivite.IdCategorie = (Singleton_BD.getInstance().getListeTypeCategories().IndexOf(cbx_listeCategorie.Text) + 1);
                tbx_url.Visibility = collapse;
            }
            else if (cbx_listeCategorie.SelectedIndex > 0)
            {
                tbx_url.Visibility = collapse;
            }
            else
            {
                newActivite.IdCategorie = (Singleton_BD.getInstance().getListeCategorie().Count() + 1);

                Categorie newCategorie = new Categorie();

                if (cbx_listeCategorie.Text.Trim() != "")
                    newCategorie.Type = cbx_listeCategorie.Text;

                if (tbx_url.Visibility == visible)
                    if (tbx_url.Text.Trim() != "")
                    {
                        if (Uri.IsWellFormedUriString(tbx_url.Text, UriKind.Absolute))
                            newCategorie.Url = tbx_url.Text;
                        else
                            tbx_erreur_url.Text = "Le prix doit �tre en format url";
                    }
            }
        }
    }
}
