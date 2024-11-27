using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Prog_Graph_BDD_A2024
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAdministrateur : Page
    {
        public PageAdministrateur()
        {
            this.InitializeComponent();

            adminFrame.Navigate(typeof(PageAdminActivites));
        }

        private void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = args.SelectedItem as NavigationViewItem;

            switch (item.Name)
            {
                case "iAdherents":
                    adminFrame.Navigate(typeof(PageAdminAdherents));
                    break;
                case "iActivites":
                    adminFrame.Navigate(typeof(PageAdminActivites));
                    break;
                case "iSeances":
                    adminFrame.Navigate(typeof(PageAdminSeances));
                    break;
                case "iStat":
                    adminFrame.Navigate(typeof(PageAdminStat));
                    break;
                case "iCompte":
                    adminFrame.Navigate(typeof(PageAdminCompte));
                    break;
                case "iDeconnexion":
                    adminFrame.Navigate(typeof(PageAuthentification));
                    break;
                default:
                    break;
            }
        }
    }
}
