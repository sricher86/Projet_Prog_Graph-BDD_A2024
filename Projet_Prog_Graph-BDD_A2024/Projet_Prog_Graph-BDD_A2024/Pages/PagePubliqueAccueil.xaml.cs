using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Projet_Prog_Graph_BDD_A2024.Classes;
using Projet_Prog_Graph_BDD_A2024.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Prog_Graph_BDD_A2024.Pages
{
    public sealed partial class PagePubliqueAccueil : Page
    {
        public PagePubliqueAccueil()
        {
            this.InitializeComponent();
            Singleton_BD.getInstance().getActivites();
            Singleton_BD.getInstance().getCategories();
            activites.ItemsSource = Singleton_BD.getInstance().getListeActivites();
        }


        private async void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            DialogAuthentification dialog = new DialogAuthentification();
            dialog.XamlRoot = this.Content.XamlRoot;
            dialog.PrimaryButtonText = "Connexion";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                if (dialog.Result == SignInResult.SignInAdherent)
                    Frame.Navigate(typeof(PageConnexion));

                if (dialog.Result == SignInResult.SignInAdmin)
                    Frame.Navigate(typeof(PageAdministrateur));
            }
        }
    }
}
