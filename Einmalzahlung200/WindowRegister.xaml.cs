using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Timers;
using System.Threading;

namespace Einmalzahlung200
{
    /// <summary>
    /// Interaktionslogik für WindowRegister.xaml
    /// </summary>
    public partial class WindowRegister : Window
    {
        //Dateipfade zur Datenbank
        public string dateipfadUN = "C:\\Users\\justi\\Desktop\\Einmalzahlung200\\Usernames.txt";
        public string dateipfadPW = "C:\\Users\\justi\\Desktop\\Einmalzahlung200\\Passwords.txt";
        public string dateipfadDT = "C:\\Users\\justi\\Desktop\\Einmalzahlung200\\data.txt";
        public WindowRegister()
        {
            //Elemente beschreiben(Farben und Höhe)
            InitializeComponent();
            this.Background = Brushes.White;
            this.Height = 500;
            this.Width = 400;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.chooseRegister.Background= (Brush)(new BrushConverter().ConvertFrom("#007986"));
            this.chooseRegister.Foreground = Brushes.White;
            this.chooseBack.Background = Brushes.White;
            this.chooseBack.Foreground = (Brush)(new BrushConverter().ConvertFrom("#007986"));
            this.chooseBack.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#007986"));
            this.ErrorUsername.Foreground = (Brush)(new BrushConverter().ConvertFrom("#F15249"));
            this.ErrorPassword.Foreground = (Brush)(new BrushConverter().ConvertFrom("#F15249"));
            this.ErrorPasswordTooShort.Foreground = (Brush)(new BrushConverter().ConvertFrom("#F15249"));
            this.ErrorLogo1.Visibility = Visibility.Hidden;
            this.ErrorLogo2.Visibility = Visibility.Hidden;
            this.ErrorLogo3.Visibility = Visibility.Hidden;
            this.ErrorPasswordTooShort.Visibility = Visibility.Hidden;
            resetErrorSigns();
        }

        //Setzt die Fehlermeldungen zurück
        private void resetErrorSigns()
        {
            this.ErrorUsername.Visibility = Visibility.Hidden;
            this.ErrorPassword.Visibility = Visibility.Hidden;
            this.ErrorLogo1.Visibility = Visibility.Hidden;
            this.ErrorLogo2.Visibility = Visibility.Hidden;
            this.ErrorLogo3.Visibility = Visibility.Hidden;
            this.ErrorPasswordTooShort.Visibility = Visibility.Hidden;
        }
        private void chooseBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w1 = new MainWindow();
            w1.Show();
            this.Close();
        }
        //Diese Funktion erstellt einen neuen Nutzer
        public void createNewUser(string Username, string Password)
        {
            StreamWriter stun = new StreamWriter(dateipfadUN, true);
            stun.WriteLine(Username);
            stun.Close();

            StreamWriter stpw = new StreamWriter(dateipfadPW, true);
            stpw.WriteLine(Password);
            stpw.Close();


        }

        //Diese Funktion checkt ob der Username verfügbar ist
        //Und legt dann ein Konto an.
        private void register_Click(object sender, RoutedEventArgs e)
        {
            resetErrorSigns();
            //Diese beiden Container werden mit allen Login Daten gefüllt
            List<string> ContainerUN = new List<string>();
            List<string> ContainerPW = new List<string>();

            //Zählt wie viele Lines es in der Datei gibt (für den Loop)
            int lineCount = File.ReadLines(dateipfadUN).Count();


            //Container Passwörter füllen
            StreamReader rdpw = new StreamReader(dateipfadPW);
            for (int i = 0; i < lineCount; i++)
            {
                ContainerPW.Add(rdpw.ReadLine());
            }
            rdpw.Close();

            //Container Usernames füllen
            StreamReader rdun = new StreamReader(dateipfadUN);
            for (int i = 0; i < lineCount; i++)
            {
                ContainerUN.Add(rdun.ReadLine());
            }
            rdun.Close();

            bool bUsername = true;
            bool bPassword = true;
            //Gibt Fehler am Bildschirm aus
            if(ContainerUN.Contains(chooseUsername.Text)){
                this.ErrorUsername.Visibility = Visibility.Visible;
                this.ErrorLogo1.Visibility = Visibility.Visible;
                bUsername = false;
            }

            //Passwortlängenkondition, mindestens 8 Zeichen!
            if (choosePassword.Password.ToString().Length < 7)
            {
                this.ErrorPasswordTooShort.Visibility = Visibility.Visible;
                this.ErrorLogo2.Visibility = Visibility.Visible;
                this.ErrorLogo3.Visibility = Visibility.Visible;
                bPassword = false;
            }
            if (choosePassword.Password.ToString() != choosePasswordBest.Password.ToString())
            {
                this.ErrorPassword.Visibility = Visibility.Visible;
                this.ErrorLogo2.Visibility = Visibility.Visible;
                this.ErrorLogo3.Visibility = Visibility.Visible;
                bPassword = false;
            }

            //Bricht die Funktion ab wenn mind. eine Fehlermeldung vorliegt
            if(bPassword==false || bUsername == false)
            {
                return;
            }
            createNewUser(chooseUsername.Text ,choosePassword.Password.ToString());

            MainWindow w1 = new MainWindow();
            w1.Show();
            this.Close();
        }
    }
}
