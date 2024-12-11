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
    public sealed partial class DialogAdminAjoutSeance : ContentDialog
    {
        bool valide = true;

        int idSeance;
        DateTime dateOrganisation;
        int heureOrganisation;
        int nbrPlaceDisponible;
        double noteAppreciation;
        string idAdmin = "1000";
        int idActivite;

        public DialogAdminAjoutSeance()
        {
            this.InitializeComponent();
            Singleton_BD.getInstance().getActivites();
            cbx_listeActivites.ItemsSource = Singleton_BD.getInstance().getListeNomsActivites();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            resetErreurs();

            idSeance = (Singleton_BD.getInstance().getListeSeance().Count() + 1);

            if (tbx_date.SelectedDate.HasValue == true)
                dateOrganisation = new DateTime(tbx_date.Date.Year, tbx_date.Date.Month, tbx_date.Date.Day);
            else
            {
                tbx_erreur_date.Text = "La date d'organisation ne peut pas être vide";
                valide = false;
            }

            if (tbx_heure.Text.Trim() != "")
            {
                if (Int32.TryParse(tbx_heure.Text, out int heure))
                {
                    if ((heure >= 1) && (heure <= 24))
                        heureOrganisation = heure;
                    else
                    {
                        tbx_erreur_heure.Text = "L'heure d'organisation doit être un nombre entier entre 1 et 24";
                        valide = false;
                    }
                }
                else
                {
                    tbx_erreur_heure.Text = "L'heure d'organisation doit être un nombre entier";
                    valide = false;
                }
            }
            else
            {
                tbx_erreur_heure.Text = "L'heure d'organisation ne peut pas être vide";
                valide = false;
            }

            if (tbx_places.Text.Trim() != "")
            {
                if (Int32.TryParse(tbx_places.Text, out int places))
                    nbrPlaceDisponible = places;
                else
                {
                    tbx_erreur_places.Text = "Le nombre de places disponible doit être un nombre entier";
                    valide = false;
                }
            }
            else
            {
                tbx_erreur_places.Text = "Le nombre de places disponible ne peut pas être vide";
                valide = false;
            }

            //if (tbx_note.Text.Trim() != "")
            //{
            //    if (Double.TryParse(tbx_note.Text, out double note))
            //    {
            //        if ((note >= 1) && (note <= 5))
            //            noteAppreciation = note;
            //        else
            //        {
            //            tbx_erreur_note.Text = "La note d'appréciation doit être un nombre entre 1 et 5";
            //            valide = false;
            //        }
            //    }
            //    else
            //    {
            //        tbx_erreur_note.Text = "La note d'appréciation doit être en format décimal";
            //        valide = false;
            //    }
            //}
            //else
            //{
            //    tbx_erreur_note.Text = "La note d'appréciation ne peut pas être vide";
            //    valide = false;
            //}

            if (cbx_listeActivites.SelectedIndex != -1)
            {
                idActivite = (cbx_listeActivites.SelectedIndex + 1);
                System.Diagnostics.Debug.WriteLine(idActivite);
            }
            else
            {
                tbx_erreur_activite.Text = "Veuillez choisir une activité";
                valide = false;
            }

            if (valide == true)
            {
                Singleton_BD.getInstance().addSeance(new Seance(idSeance, dateOrganisation, heureOrganisation, nbrPlaceDisponible, noteAppreciation, idAdmin, idActivite));
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
            tbx_erreur_date.Text = String.Empty;
            tbx_erreur_heure.Text = String.Empty;
            tbx_erreur_places.Text = String.Empty;
            //tbx_erreur_note.Text = String.Empty;
            tbx_erreur_activite.Text = String.Empty;
        }
    }
}
