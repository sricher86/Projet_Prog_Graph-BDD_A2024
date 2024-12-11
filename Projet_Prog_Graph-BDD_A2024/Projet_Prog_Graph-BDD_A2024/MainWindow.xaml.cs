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
using Projet_Prog_Graph_BDD_A2024.Classes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Prog_Graph_BDD_A2024
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            if (Singleton_Session.UserConnected) iDeconnexion.Visibility = Visibility.Visible;
            else iConnexion.Visibility = Visibility.Visible;

            Singleton_Session.PageCourante = "PagePublique";
            mainFrame.Navigate(typeof(PagePublique));
        }

        private async void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = args.SelectedItem as NavigationViewItem;

            if (item != null)
            {
                switch (item.Name)
                {
                    case "iActivites":
                        //if(Singleton_Session)
                        Singleton_Session.PageCourante = "PagePublique";
                        mainFrame.Navigate(typeof(PagePublique));
                        break;
                    case "iConnexion":
                        DialogAuthentification dialog = new DialogAuthentification();
                        dialog.XamlRoot = this.Content.XamlRoot;
                        dialog.PrimaryButtonText = "Connexion";
                        dialog.CloseButtonText = "Annuler";
                        dialog.DefaultButton = ContentDialogButton.Primary;

                        ContentDialogResult resultat = await dialog.ShowAsync();

                        if (resultat == ContentDialogResult.Primary)
                        {
                            if (Singleton_Session.UserConnected)
                            {
                                iDeconnexion.Visibility = Visibility.Visible;
                                iConnexion.Visibility = Visibility.Collapsed;
                                if (Singleton_Session.PageCourante == "PageDetail")
                                    mainFrame.Navigate(typeof(PageDetail), Singleton_Session.ActiviteCourante);
                                navView.SelectedItem = null;
                            }
                        }
                        break;
                    case "iDeconnexion":
                        Singleton_Session.getInstance().adherentDeconn();
                        if (Singleton_Session.PageCourante == "PageDetail")
                            mainFrame.Navigate(typeof(PageDetail), Singleton_Session.ActiviteCourante);
                        navView.SelectedItem = null;
                        break;
                }
            }
        }
    }
}
