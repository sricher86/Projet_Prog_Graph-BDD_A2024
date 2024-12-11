using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Projet_Prog_Graph_BDD_A2024.Pages;
using Projet_Prog_Graph_BDD_A2024.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Prog_Graph_BDD_A2024.Dialogs
{
    public enum SignInResult
    {
        SignInAdherent,
        SignInAdmin
    }

    public partial class DialogAuthentification : ContentDialog
    {
        public SignInResult Result { get; private set; }

        static ObservableCollection<Administrateur> adminList;
        static ObservableCollection<Adherents> adherentList;
        bool valide = true;

        public DialogAuthentification()
        {
            this.InitializeComponent();
            Singleton_BD.getInstance().getAdmins();
            Singleton_BD.getInstance().getAdherents();
            adminList = new ObservableCollection<Administrateur>();
            adherentList = new ObservableCollection<Adherents>();
            adminList = Singleton_BD.getInstance().getListeAdministrateur();
            adherentList = Singleton_BD.getInstance().getListeAdherents();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            resetErreurs();

            if (stkpnl_adherent.Visibility == Visibility.Visible)
            {
                Boolean user = false;

                foreach (Adherents adh in adherentList)
                {
                    if (tbx_id_adherent.Text.Equals(adh.No_identification))
                    {
                        user = true;
                        Singleton_BD.getInstance().adherentConn(adh);
                        Singleton_BD.getInstance().UserConnected = true;
                    }
                }

                if (tbx_id_adherent.Text.Trim() == "")
                {
                    tbx_erreur_id_adherent.Text = "Le no. d'identification ne peut pas être vide";
                    valide = false;
                }
                else
                {
                    if (!user)
                    {
                        tbx_erreur_id_adherent.Text = "Erreur de no. d'identification";
                        valide = false;
                    }
                }

                if (user)
                {
                    valide = true;
                    this.Result = SignInResult.SignInAdherent;
                }
            }

            if (stkpnl_admin.Visibility == Visibility.Visible)
            {
                Boolean user = false;
                Boolean pass = false;

                string passwordHashed = Hash(pbx_motDePasse.Password).ToLower();

                foreach (Administrateur a in adminList)
                {
                    if (tbx_id_admin.Text.Equals(a.IdAdmin))
                    {
                        user = true;

                        if (passwordHashed.Equals(a.MotDePasse)) pass = true;
                    }
                }

                if (tbx_id_admin.Text.Trim() == "")
                {
                    tbx_erreur_id_admin.Text = "Le no. d'identification ne peut pas être vide";
                    valide = false;
                }
                else
                {
                    if (!user)
                    {
                        tbx_erreur_id_admin.Text = "Erreur de no. d'identification";
                        valide = false;
                    }
                }

                if (pbx_motDePasse.Password.Trim() == "")
                {
                    tbx_erreur_motDePasse.Text = "Le mot de passe ne peut pas être vide";
                    valide = false;
                }
                else
                {
                    if (!pass)
                    {
                        tbx_erreur_motDePasse.Text = "Erreur de mot de passe";
                        valide = false;
                    }
                }

                if ((tbx_id_admin.Text.Trim() != "") && (pbx_motDePasse.Password.Trim() != ""))
                {
                    if (!pass)
                    {
                        tbx_erreur_authentification.Visibility = Visibility.Visible;
                    }
                }

                if (user && pass)
                {
                    valide = true;
                    tbx_erreur_authentification.Visibility = Visibility.Collapsed;
                    this.Result = SignInResult.SignInAdmin;
                }
            }
        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (args.Result == ContentDialogResult.Primary)
            {
                if (valide == false)
                    args.Cancel = true;
                else
                    args.Cancel = false;
            }
            else if (args.Result == ContentDialogResult.Secondary)
                args.Cancel = false;
            else
                args.Cancel = false;
        }

        private void resetErreurs()
        {
            tbx_erreur_id_adherent.Text = String.Empty;
            tbx_erreur_id_admin.Text = String.Empty;
            tbx_erreur_motDePasse.Text = String.Empty;
        }

        static string Hash(string input)
        {
            return Convert.ToHexString(SHA1.HashData(Encoding.UTF8.GetBytes(input)));
        }

        private void utilisateur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (utilisateur.SelectedIndex == 0)
            {
                stkpnl_adherent.Visibility = Visibility.Visible;
                stkpnl_admin.Visibility = Visibility.Collapsed;
            }
            else
            {
                stkpnl_adherent.Visibility = Visibility.Collapsed;
                stkpnl_admin.Visibility = Visibility.Visible;
            }
        }
    }
}
