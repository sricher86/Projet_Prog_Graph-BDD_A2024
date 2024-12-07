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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageConnexion : Page
    {
        Frame mainFrame;

        public PageConnexion()
        {
            this.InitializeComponent();

            connexionFrame.Navigate(typeof(PagePublique), mainFrame);
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
                        connexionFrame.Navigate(typeof(PagePublique), mainFrame);
                        break;
                    case "iConnexion":
                        connexionFrame.Navigate(typeof(Connexion), mainFrame);
                        break;
                }
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
