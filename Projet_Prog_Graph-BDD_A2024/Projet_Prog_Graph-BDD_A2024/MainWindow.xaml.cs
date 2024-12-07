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
using Projet_Prog_Graph_BDD_A2024.Pages;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Prog_Graph_BDD_A2024
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            mainFrame.Navigate(typeof(PageConnexion), mainFrame);
        }

        //public Frame GetMainFrame {
        //    get { return mainFrame; }
        //    set { mainFrame = value; }
        //}

        //private void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        //{
        //    var item = args.SelectedItem as NavigationViewItem;

        //    if (item != null)
        //    {
        //        switch (item.Name)
        //        {
        //            case "iActivites":
        //                mainFrame.Navigate(typeof(PagePublique), mainFrame);
        //                break;
        //            case "iConnexion":
        //                mainFrame.Navigate(typeof(Connexion));
        //                break;
        //        }
        //    }
        //}
    }
}
