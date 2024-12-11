using Google.Protobuf.Reflection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Projet_Prog_Graph_BDD_A2024.Dialogs;
using Projet_Prog_Graph_BDD_A2024.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT;
using static Google.Protobuf.Reflection.FeatureSet.Types;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Prog_Graph_BDD_A2024.Pages
{
    public sealed partial class PageConnexion : Page
    {
        public PageConnexion()
        {
            this.InitializeComponent();
            connexionFrame.Navigate(typeof(PagePublique));
        }

        public Frame GetConnexionFrame
        {
            get { return connexionFrame; }
            set { connexionFrame = value; }
        }

        private void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = args.SelectedItem as NavigationViewItem;

            if (item != null)
            {
                switch (item.Name)
                {
                    case "iActivites":
                        connexionFrame.Navigate(typeof(PagePublique));
                        break;
                    //case "iConnexion":
                    //    connexionFrame.Navigate(typeof(Connexion), mainFrame);
                    //    break;
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                connexionFrame.Navigate(typeof(PagePublique));
            }
        }

        private async void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            DialogAuthentification dialog = new DialogAuthentification();
            dialog.XamlRoot = this.Content.XamlRoot;
            dialog.PrimaryButtonText = "Connexion";
            dialog.SecondaryButtonText = "Deconnexion";
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
            if (resultat == ContentDialogResult.Secondary)
                Frame.Navigate(typeof(PagePublique));
        }
    }
}
