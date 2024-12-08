using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Projet_Prog_Graph_BDD_A2024.Dialogs;
using Projet_Prog_Graph_BDD_A2024.Pages;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Prog_Graph_BDD_A2024
{
    public partial class MainWindow : Window
    {
        string userType = "";
        static MainWindow instance = null;
        List<Object> paras = new List<Object>();
        DialogAuthentification dialog;

        public MainWindow()
        {
            this.InitializeComponent();
            mainFrame.Navigate(typeof(PageConnexion));
        }
        public static MainWindow getInstance()
        {
            if (instance == null)
                instance = new MainWindow();

            return instance;
        }

        private async void mainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            DialogAuthentification dialog = new DialogAuthentification();
            dialog.XamlRoot = this.Content.XamlRoot;
            dialog.PrimaryButtonText = "Connexion";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                paras.Add(mainFrame);
                if (dialog.Result == SignInResult.SignInAdherent)
                {
                    userType = "adherent";
                    paras.Add(userType);
                    mainFrame.Navigate(typeof(PageConnexion), paras);
                }

                if (dialog.Result == SignInResult.SignInAdmin)
                {
                    userType = "admin";
                    paras.Add(userType);
                    mainFrame.Navigate(typeof(PageAdministrateur), paras);
                }

            }
        }

        public Frame MainFrame
        {
            get { return mainFrame; }
            set { this.mainFrame = value; }
        }

        public Frame getMainFrame()
        {
            return this.mainFrame;
        }

        public string UserType
        {
            get { return userType; }
            set { this.userType = value; }
        }

    }
}
