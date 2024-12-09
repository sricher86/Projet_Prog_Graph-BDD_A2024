using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using Projet_Prog_Graph_BDD_A2024;
using Projet_Prog_Graph_BDD_A2024.Classes;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Prog_Graph_BDD_A2024.Pages
{
    public sealed partial class PagePublique : Page
    {
        Activite activiteChoisi;

        public PagePublique()
        {
            this.InitializeComponent();
            Singleton_BD.getInstance().getActivites();
            Singleton_BD.getInstance().getCategories();
            activites.ItemsSource = Singleton_BD.getInstance().getListeActivites();
        }

        private void activites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (activites.SelectedItem != null)
            {
                activiteChoisi = (Activite) activites.SelectedItem;
                //err.Text = activiteChoisi.Nom;
                Frame.Navigate(typeof(PageDetail), activiteChoisi);
            }
        }


        //private void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        //{
        //    if (args.SelectedItem != null)
        //    {
        //        var item = args.SelectedItem as NavigationViewItem;

        //        switch (item.Name)
        //        {
        //            case ("iActivites"):
        //                PubliqueFrame.Navigate(typeof (PagePubliqueAccueil));
        //                break;
        //            case ("iSeances"):
        //                PubliqueFrame.Navigate(typeof(PagePubliqueAccueil));
        //                break;
        //            case ("iInscription"):
        //                PubliqueFrame.Navigate(typeof(PagePubliqueAccueil));
        //                break;
        //        }
        //    }

        //}

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null) userType = (string)e.Parameter;
        }
    }
}
