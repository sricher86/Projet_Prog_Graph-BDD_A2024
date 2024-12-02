using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Prog_Graph_BDD_A2024.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Connexion : Page
    {
        static ObservableCollection<Administrateur> adminList;
        Window m_window;

        public Connexion()
        {
            this.InitializeComponent();
            Singleton_BD.getInstance().getAdmins();
            adminList = new ObservableCollection<Administrateur>();
            adminList = Singleton_BD.getInstance().getListeAdministrateur();
        
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            Boolean user = false;
            Boolean pass = false;
            string passwordHashed = Hash(password.Password).ToLower();

            foreach (Administrateur a in adminList)
            {
                if (username.Text.Equals(a.IdAdmin))
                {
                    user = true;
                    
                    if (passwordHashed.Equals(a.MotDePasse))
                    {
                        pass = true;
                    }
                }
            }

            if (user && pass)
            {
                this.m_window = new Window();
                Frame rootFrame = new Frame();
                this.m_window.Content = rootFrame;
                m_window.Activate();
                rootFrame.Navigate(typeof(PageAdminActivites));
            }

            if (!user) errUser.Text = "Erreur de nom d'usager";
            if (!pass) errPass.Text = "Erreur de mot de passe";

            //if (user && pass)
            //{
            //    MainWindow newWindow = new MainWindow();
            //    newWindow.GetMainFrame.Navigate(typeof(PageAdministrateur));
            //}
        }

        static string Hash(string input)
        {
            return Convert.ToHexString(SHA1.HashData(Encoding.UTF8.GetBytes(input)));
        }
            
    }
}
