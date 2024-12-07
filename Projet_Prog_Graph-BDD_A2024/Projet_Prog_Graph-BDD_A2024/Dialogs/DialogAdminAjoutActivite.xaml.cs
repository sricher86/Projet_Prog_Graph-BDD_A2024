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

namespace Projet_Prog_Graph_BDD_A2024.Dialogs
{
    public sealed partial class DialogAdminAjoutActivite : ContentDialog
    {
        bool valide = true;

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
            resetErreurs();

            if (tbx_nom.Text.Trim() != "")
                newActivite.Nom = tbx_nom.Text;
            else
            {
                tbx_erreur_nom.Text = "Le nom ne peut pas être vide";
                valide = false;
            }

            if (tbx_cout.Text.Trim() != "")
            {
                if (Double.TryParse(tbx_cout.Text, out double cout))
                    newActivite.CoutOrganisation = cout;
                else
                {
                    tbx_erreur_cout.Text = "Le coût doit être en format décimal";
                    valide = false;
                }
            }
            else
            {
                tbx_erreur_cout.Text = "Le coût ne peut pas être vide";
                valide = false;
            }

            if (tbx_prix.Text.Trim() != "")
            {
                if (Double.TryParse(tbx_prix.Text, out double prix))
                    newActivite.PrixVente = prix;
                else
                {
                    tbx_erreur_prix.Text = "Le prix doit être en format décimal";
                    valide = false;
                }
            }
            else
            {
                tbx_erreur_prix.Text = "Le prix ne peut pas être vide";
                valide = false;
            }

            if (cbx_listeCategorie.Text.Trim() == "")
            {
                if (cbx_listeCategorie.SelectedIndex != -1)
                    newActivite.IdCategorie = (cbx_listeCategorie.SelectedIndex + 1);
                else
                {
                    tbx_erreur_categorie.Text = "Veuillez choisir une catégorie";
                    valide = false;
                }
            }

            if (tbx_url.Visibility == visible)
            {
                if (tbx_url.Text.Trim() == "")
                {
                    tbx_erreur_url.Text = "Le url ne peut pas être vide";
                    valide = false;
                }
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
                {
                    if (tbx_url.Text.Trim() != "")
                    {
                        if (Uri.IsWellFormedUriString(tbx_url.Text, UriKind.Absolute))
                            newCategorie.Url = tbx_url.Text;
                        else
                        {
                            tbx_erreur_url.Text = "Le prix doit être en format url";
                            valide = false;
                        }
                    }
                }
            }
        }

        private void resetErreurs()
        {
            tbx_erreur_nom.Text = String.Empty;
            tbx_erreur_cout.Text = String.Empty;
            tbx_erreur_prix.Text = String.Empty;
            tbx_erreur_categorie.Text = String.Empty;
            tbx_erreur_url.Text = String.Empty;
        }
    }
}
