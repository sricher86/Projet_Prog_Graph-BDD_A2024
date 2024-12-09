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
    public sealed partial class DialogAdminAjoutActivite : ContentDialog
    {
        bool valide = true;

        Visibility visible = Visibility.Visible;
        Visibility collapse = Visibility.Collapsed;

        int idActivite;
        string nom;
        double coutOrganisation;
        double prixVente;
        int idCategorie;
        string idAdmin = "1000";
        string url = "";
        string description;
        string type;


        public DialogAdminAjoutActivite()
        {
            this.InitializeComponent();

            cbx_listeCategorie.ItemsSource = Singleton_BD.getInstance().getListeTypeCategories();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            resetErreurs();

            idActivite = (Singleton_BD.getInstance().getListeActivites().Count() + 1);

            if (tbx_nom.Text.Trim() != "")
            {
                nom = tbx_nom.Text;
                valide = true;
            }
            else
            {
                tbx_erreur_nom.Text = "Le nom ne peut pas être vide";
                valide = false;
            }

            if (tbx_cout.Text.Trim() != "")
            {
                if (Double.TryParse(tbx_cout.Text, out double cout))
                {
                    coutOrganisation = cout;
                    valide = true;
                }
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
                {
                    prixVente = prix;
                    valide = true;
                }
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

            if (tbx_description.Text.Trim() != "")
            {
                description = tbx_description.Text;
                valide = true;
            }
            else
            {
                tbx_erreur_description.Text = "La description ne peut pas être vide";
                valide = false;
            }

            if (cbx_listeCategorie.Text.Trim() == "")
            {
                if (cbx_listeCategorie.SelectedIndex != -1)
                {
                    idCategorie = (cbx_listeCategorie.SelectedIndex + 1);
                    valide = true;
                }
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

            if(valide == true)
            {
                Singleton_BD.getInstance().addActivite(
                    new Activite(
                        idActivite, nom, coutOrganisation, prixVente, idCategorie, idAdmin, url, description
                    )
                );
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
                idCategorie = (Singleton_BD.getInstance().getListeTypeCategories().IndexOf(cbx_listeCategorie.Text) + 1);
                tbx_url.Visibility = collapse;
            }
            else if (cbx_listeCategorie.SelectedIndex > 0)
            {
                tbx_url.Visibility = collapse;
            }
            else
            {
                idCategorie = (Singleton_BD.getInstance().getListeCategorie().Count() + 1);

                Categorie newCategorie = new Categorie();

                if (cbx_listeCategorie.Text.Trim() != "")
                {
                    type = cbx_listeCategorie.Text;
                    valide = true;
                }

                if (tbx_url.Visibility == visible)
                {
                    if (tbx_url.Text.Trim() != "")
                    {
                        if (Uri.IsWellFormedUriString(tbx_url.Text, UriKind.Absolute))
                        {
                            url = tbx_url.Text;
                            valide = true;
                        }
                        else
                        {
                            tbx_erreur_url.Text = "Le prix doit être en format url";
                            valide = false;
                        }
                    }
                }

                if(valide == true)
                {
                    Singleton_BD.getInstance().addCategorie(
                        new Categorie(
                            idCategorie, type, idAdmin, url
                        )
                    );
                }
            }
        }

        private void resetErreurs()
        {
            tbx_erreur_nom.Text = String.Empty;
            tbx_erreur_cout.Text = String.Empty;
            tbx_erreur_prix.Text = String.Empty;
            tbx_erreur_description.Text = String.Empty;
            tbx_erreur_categorie.Text = String.Empty;
            tbx_erreur_url.Text = String.Empty;
        }
    }
}
