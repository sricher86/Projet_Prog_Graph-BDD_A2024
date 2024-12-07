using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Projet_Prog_Graph_BDD_A2024.Pages;
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
    public sealed partial class DialogAdminAuthentification : ContentDialog
    {
        static ObservableCollection<Administrateur> adminList;
        Frame mainFrame;
        bool valide = true;

        public DialogAdminAuthentification()
        {
            this.InitializeComponent();
            Singleton_BD.getInstance().getAdmins();
            adminList = new ObservableCollection<Administrateur>();
            adminList = Singleton_BD.getInstance().getListeAdministrateur();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            resetErreurs();

            Boolean user = false;
            Boolean pass = false;

            string passwordHashed = Hash(pbx_motDePasse.Password).ToLower();

            foreach (Administrateur a in adminList)
            {
                if (tbx_id.Text.Equals(a.IdAdmin))
                {
                    user = true;

                    if (passwordHashed.Equals(a.MotDePasse)) pass = true;
                }
            }

            if (tbx_id.Text.Trim() == "")
            {
                tbx_erreur_id.Text = "Le no. d'identification ne peut pas être vide";
                valide = false;
            }
            else
            {
                if (!user)
                {
                    tbx_erreur_id.Text = "Erreur de no. d'identification";
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

            if ((tbx_id.Text.Trim() != "") && (pbx_motDePasse.Password.Trim() != ""))
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
            tbx_erreur_id.Text = String.Empty;
            tbx_erreur_motDePasse.Text = String.Empty;
        }

        static string Hash(string input)
        {
            return Convert.ToHexString(SHA1.HashData(Encoding.UTF8.GetBytes(input)));
        }
    }
}
