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
using WinRT;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Prog_Graph_BDD_A2024.Pages
{
    public sealed partial class PageAdministrateur : Page
    {
        Frame mainFrame;

        public PageAdministrateur()
        {
            this.InitializeComponent();

            adminFrame.Navigate(typeof(PageAdminActivites));
        }

        private void navViewAdmin_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = args.SelectedItem as NavigationViewItem;

            switch (item.Name)
            {
                case "iActivites":
                    adminFrame.Navigate(typeof(PageAdminActivites));
                    break;
                case "iAdherents":
                    adminFrame.Navigate(typeof(PageAdminAdherents));
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
                    mainFrame.Navigate(typeof(PageConnexion), mainFrame);
                    break;
                default:
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                mainFrame = e.Parameter.As<Frame>();
            }
        }
    }
}
