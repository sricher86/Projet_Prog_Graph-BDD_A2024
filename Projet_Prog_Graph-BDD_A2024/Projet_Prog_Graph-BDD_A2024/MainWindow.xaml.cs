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
        public MainWindow()
        {
            this.InitializeComponent();
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
                if (dialog.Result == SignInResult.SignInAdherent)
                    mainFrame.Navigate(typeof(PageConnexion), mainFrame);

                if (dialog.Result == SignInResult.SignInAdmin)
                    mainFrame.Navigate(typeof(PageAdministrateur), mainFrame);
            }
        }
    }
}
