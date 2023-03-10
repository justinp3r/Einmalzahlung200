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
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Einmalzahlung200
{
    /// <summary>
    /// Interaktionslogik für WindowRegister.xaml
    /// </summary>
    public partial class WindowRegister : Window
    {
        //Dateipfad zur Datenbank
        public string data = "C:\\Users\\justi\\Desktop\\Einmalzahlung200\\allData.txt";
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

        //Klasse Nutzer beschreibt einen account
        class Nutzer
        {
            public string username;
            public string password;
            public string key;

            public Nutzer(string un, string pd)
            {
                this.username = un;
                this.password = pd;
                this.key = sha256_hash(un+pd);
            }
        }

        //Diese Funktion erstellt einen neuen Nutzer
        //Der nutzer bekommt eine eigene Line mit allen Daten
        public void createNewUser(string Username, string Password)
        {
            Nutzer N1 = new Nutzer(Username, Password);
            StreamWriter stun = new StreamWriter(data, true);
            stun.WriteLine(N1.username+"|"+N1.password+"|"+N1.key);
            stun.Close();
        }

        //Erstellt einen SHA256 Hash
        public static String sha256_hash(String UN)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(UN));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }
        //Diese Funktion checkt ob der Username verfügbar ist
        //Und legt dann ein Konto an.
        private void register_Click(object sender, RoutedEventArgs e)
        {
            resetErrorSigns();

            bool bUsername = true;
            bool bPassword = true;

            int lineCount = File.ReadLines(data).Count();
            StreamReader rd = new StreamReader(data);
            //Gchecken ob username schon in datenbank existiert.
            for (int i = 0; i < lineCount; i++) {
                string dataline = rd.ReadLine();

                //Splitted den String bei |
                if (dataline.Split(new char[] {'|'})[0] == chooseUsername.Text) {
                    this.ErrorUsername.Visibility = Visibility.Visible;
                    this.ErrorLogo1.Visibility = Visibility.Visible;
                    bUsername = false;
                }
            }
            rd.Close();

            //Passwortlängenkondition, mindestens 8 Zeichen!
            if (choosePassword.Password.ToString().Length < 8)
            {
                this.ErrorPasswordTooShort.Visibility = Visibility.Visible;
                this.ErrorLogo2.Visibility = Visibility.Visible;
                this.ErrorLogo3.Visibility = Visibility.Visible;
                bPassword = false;
            }
            //Wenn beide Passwörter nicht übereinstimmen => Fehlermeldung
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
