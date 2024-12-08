using Google.Protobuf.Reflection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Projet_Prog_Graph_BDD_A2024.Dialogs;
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
        List<Object> listeParams;
        string user;
        Frame mainFrame;

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
                listeParams = (List<Object>)e.Parameter;
                mainFrame = (Frame)listeParams[0];
                user = (string)listeParams[1];
                listeParams.Add(mainFrame);
                listeParams.Add(user);
                connexionFrame.Navigate(typeof(PagePublique), listeParams);
            }
        }

        private async void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            DialogAuthentification dialog = new DialogAuthentification();
            dialog.XamlRoot = this.Content.XamlRoot;
            dialog.PrimaryButtonText = "Connexion";
            dialog.SecondaryButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                if (dialog.Result == SignInResult.SignInAdherent)
                    MainWindow.getInstance().MainFrame.Navigate(typeof(PageConnexion));

                if (dialog.Result == SignInResult.SignInAdmin)
                    MainWindow.getInstance().MainFrame.Navigate(typeof(PageAdministrateur));
            }
        }
    }
}
