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
using System.ComponentModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Prog_Graph_BDD_A2024
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            this.InitializeComponent();

            if (Singleton_Session.Connexion)
            {
                if (Singleton_Session.UserConnected)
                {
                    iConnexion.Visibility = Visibility.Collapsed;
                    iDeconnexion.Visibility = Visibility.Visible;
                    iActivites.Visibility = Visibility.Visible;
                    iAdminAdherents.Visibility = Visibility.Collapsed;
                    iAdminSeances.Visibility = Visibility.Collapsed;
                    iAdminStat.Visibility = Visibility.Collapsed;

                    Singleton_Session.PageCourante = "PagePublique";
                    mainFrame.Navigate(typeof(PagePublique));
                }
                else
                {
                    iConnexion.Visibility = Visibility.Collapsed;
                    iDeconnexion.Visibility = Visibility.Visible;
                    iActivites.Visibility = Visibility.Visible;
                    iAdminAdherents.Visibility = Visibility.Visible;
                    iAdminSeances.Visibility = Visibility.Visible;
                    iAdminStat.Visibility = Visibility.Visible;

                    Singleton_Session.PageCourante = "PageAdminActivites";
                    mainFrame.Navigate(typeof(PageAdminActivites));
                }
            }
            else
            {
                iConnexion.Visibility = Visibility.Visible;
                iDeconnexion.Visibility = Visibility.Collapsed;
                iActivites.Visibility = Visibility.Visible;
                iAdminAdherents.Visibility = Visibility.Collapsed;
                iAdminSeances.Visibility = Visibility.Collapsed;
                iAdminStat.Visibility = Visibility.Collapsed;

                Singleton_Session.PageCourante = "PagePublique";
                mainFrame.Navigate(typeof(PagePublique));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        //Singleton_Session.Connexion.OnPropertyChange
        private async void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = args.SelectedItem as NavigationViewItem;

            if (item != null)
            {
                switch (item.Name)
                {
                    case "iActivites":
                        if(Singleton_Session.UserConnected || !Singleton_Session.Connexion)
                        {
                            Singleton_Session.PageCourante = "PagePublique";
                            mainFrame.Navigate(typeof(PagePublique));
                        }
                        else if (Singleton_Session.Admin)
                        {
                            Singleton_Session.PageCourante = "PageAdminActivites";
                            mainFrame.Navigate(typeof(PageAdminActivites));
                        }
                        break;
                    case "iAdminAdherents":
                        if (Singleton_Session.Admin)
                        {
                            Singleton_Session.PageCourante = "PageAdminAdherents";
                            mainFrame.Navigate(typeof(PageAdminAdherents));
                        }
                        break;
                    case "iAdminSeances":
                        if (Singleton_Session.Admin)
                        {
                            Singleton_Session.PageCourante = "PageAdminSeances";
                            mainFrame.Navigate(typeof(PageAdminSeances));
                        }
                        break;
                    case "iAdminStat":
                        if (Singleton_Session.Admin)
                        {
                            Singleton_Session.PageCourante = "PageAdminStat";
                            mainFrame.Navigate(typeof(PageAdminStat));
                        }
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
                            if(Singleton_Session.Connexion)
                            {
                                iDeconnexion.Visibility = Visibility.Visible;
                                iConnexion.Visibility = Visibility.Collapsed;

                                if (Singleton_Session.UserConnected)
                                {
                                    iConnexion.Visibility = Visibility.Collapsed;
                                    iDeconnexion.Visibility = Visibility.Visible;
                                    iActivites.Visibility = Visibility.Visible;
                                    iAdminAdherents.Visibility = Visibility.Collapsed;
                                    iAdminSeances.Visibility = Visibility.Collapsed;
                                    iAdminStat.Visibility = Visibility.Collapsed;

                                    if (Singleton_Session.PageCourante == "PageDetail")
                                        mainFrame.Navigate(typeof(PageDetail), Singleton_Session.ActiviteCourante);
                                    else if (Singleton_Session.PageCourante == "PagePublique")
                                        mainFrame.Navigate(typeof(PagePublique));

                                    navView.SelectedItem = null;
                                }
                                if (Singleton_Session.Admin)
                                {
                                    iConnexion.Visibility = Visibility.Collapsed;
                                    iDeconnexion.Visibility = Visibility.Visible;
                                    iActivites.Visibility = Visibility.Visible;
                                    iAdminAdherents.Visibility = Visibility.Visible;
                                    iAdminSeances.Visibility = Visibility.Visible;
                                    iAdminStat.Visibility = Visibility.Visible;

                                    Debug.WriteLine("admin logged in");

                                    mainFrame.Navigate(typeof(PageAdminActivites));

                                    navView.SelectedItem = null;
                                }

                            }
                        }
                        break;
                    case "iDeconnexion":

                        if (Singleton_Session.Admin)
                            Singleton_Session.getInstance().adminDeconn();
                        if (Singleton_Session.UserConnected)
                            Singleton_Session.getInstance().adherentDeconn();

                        if (Singleton_Session.PageCourante == "PageDetail")
                            mainFrame.Navigate(typeof(PageDetail), Singleton_Session.ActiviteCourante);
                        else if (Singleton_Session.PageCourante == "PagePublique")
                            mainFrame.Navigate(typeof(PagePublique));
                        else if (Singleton_Session.PageCourante == "PageAdminActivites")
                            mainFrame.Navigate(typeof(PageAdminActivites));
                        else if (Singleton_Session.PageCourante == "PageAdminAdherents")
                            mainFrame.Navigate(typeof(PageAdminAdherents));
                        else if (Singleton_Session.PageCourante == "PageAdminSeances")
                            mainFrame.Navigate(typeof(PageAdminSeances));
                        else if (Singleton_Session.PageCourante == "PageAdminStat")
                            mainFrame.Navigate(typeof(PageAdminStat));
                        else
                            mainFrame.Navigate(typeof(PagePublique));

                        iDeconnexion.Visibility = Visibility.Collapsed;
                        iConnexion.Visibility = Visibility.Visible;
                        navView.SelectedItem = null;
                        break;
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
