using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Projet_Prog_Graph_BDD_A2024.Pages;
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
    public sealed partial class PagePublique : Page
    {
        public PagePublique()
        {
            this.InitializeComponent();
            PubliqueFrame.Navigate(typeof(PagePubliqueAccueil));
        }

        
        private void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                var item = args.SelectedItem as NavigationViewItem;

                switch (item.Name)
                {
                    case ("iActivites"):
                        PubliqueFrame.Navigate(typeof (PagePubliqueAccueil));
                        break;
                    case ("iSeances"):
                        PubliqueFrame.Navigate(typeof(PagePubliqueAccueil));
                        break;
                    case ("iInscription"):
                        PubliqueFrame.Navigate(typeof(PagePubliqueAccueil));
                        break;
                }
            }

        }
    }
}
