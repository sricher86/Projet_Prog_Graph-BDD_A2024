using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Projet_Prog_Graph_BDD_A2024.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Prog_Graph_BDD_A2024.Pages
{
    public sealed partial class PageAdminStat : Page
    {
        public PageAdminStat()
        {
            this.InitializeComponent();
            Singleton_BD.getInstance().getAdherents();
            Singleton_BD.getInstance().getActivites();

            stat1.Text = "Nombre total d’adhérents : " + Singleton_BD.getInstance().getListeAdherents().Count.ToString();

            stat2.Text = "Nombre total d’activités : " + Singleton_BD.getInstance().getListeActivites().Count.ToString();

            stat3.ItemsSource = Singleton_BD.getInstance().getListeActivites();

            stat4.ItemsSource = Singleton_BD.getInstance().getListeActivites();

            stat5.Text = "Participant plus actif : " + Singleton_BD.getInstance().getPartPlusActif();
        }
    }
}
