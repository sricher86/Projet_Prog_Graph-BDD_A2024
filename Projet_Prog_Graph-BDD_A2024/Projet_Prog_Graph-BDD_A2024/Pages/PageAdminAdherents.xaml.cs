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
using Projet_Prog_Graph_BDD_A2024.Dialogs;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Prog_Graph_BDD_A2024.Pages
{
    public sealed partial class PageAdminAdherents : Page
    {
        public PageAdminAdherents()
        {
            this.InitializeComponent();
        }

        private async void btn_ajouter_Click(object sender, RoutedEventArgs e)
        {
            DialogAdminAjoutAdherent dialog = new DialogAdminAjoutAdherent();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Ajout d'un adhérent";
            dialog.PrimaryButtonText = "Ajouter";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();
        }
    }
}
