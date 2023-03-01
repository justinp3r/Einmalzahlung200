using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Einmalzahlung200
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string dateipfadUN = "C:\\Users\\justi\\Desktop\\Einmalzahlung200\\Usernames.txt";
        public string dateipfadPW = "C:\\Users\\justi\\Desktop\\Einmalzahlung200\\Passwords.txt";
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Height = 500;
            this.Width= 400;
            this.SignIn.Background = (Brush)(new BrushConverter().ConvertFrom("#007986"));
            this.UsernameLabel.Foreground = this.PasswordLabel.Foreground = Brushes.DimGray;
            this.SignIn.Foreground = Brushes.White;
            this.register.Background = Brushes.White;
            this.resetPW.Background = Brushes.White;
            this.register.Foreground = (Brush)(new BrushConverter().ConvertFrom("#007986"));
            this.resetPW.Foreground = (Brush)(new BrushConverter().ConvertFrom("#007986"));
            this.register.BorderBrush = this.register.Foreground = (Brush)(new BrushConverter().ConvertFrom("#007986"));
            this.ErrorUserNotExists.Foreground = (Brush)(new BrushConverter().ConvertFrom("#F15249"));
            this.ErrorWrongPassword.Foreground = (Brush)(new BrushConverter().ConvertFrom("#F15249"));
            this.ErrorWrongPassword.Visibility = Visibility.Hidden;
            this.ErrorUserNotExists.Visibility = Visibility.Hidden;

        }

        //Diese Funktion wird ausgeführt wenn der Anmeldebutton getätigt wird
        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            //Hier werden username und password aus den textboxen gelesen und variablen zugewiesen
            string username = Username.Text.ToString();
            string password = Password.Password.ToString();
            validate(username, password);
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            //Neues Fenster Öffnen mit registrierdaten
            Window r1 = new WindowRegister();
            r1.Show();
            this.Hide();
        }

        //Setzt die Fehlermeldungen zurück
        private void resetErrorSigns()
        {
            this.ErrorWrongPassword.Visibility = Visibility.Hidden;
            this.ErrorUserNotExists.Visibility = Visibility.Hidden;
        }

        //Diese Funktion ruft die Textdatei mit den Usernames auf
        //und überträgt alle Einträge in eine liste, die sie dann zurück gibt
        public List <string> currentUsernames(int lines)
        {
            List<string> liste = new List<string>();
            StreamReader rdun = new StreamReader(dateipfadUN);
            for (int i = 0; i < lines; i++)
            {
                liste.Add(rdun.ReadLine());
            }
            rdun.Close();
            return liste;
        }

        //Diese Funktion ruft die Textdatei mit den Passwörtern auf
        //und überträgt alle Einträge in eine liste, die sie dann zurück gibt
        public List<string> currentPasswords(int lines)
        {
            List<string> liste = new List<string>();
            //Container Passwörter füllen
            StreamReader rdpw = new StreamReader(dateipfadPW);
            for (int i = 0; i < lines; i++)
            {
                liste.Add(rdpw.ReadLine());
            }
            rdpw.Close();
            return liste;
        }
        //Diese Funktion checkt ob die eingegebenen Daten denen der Datenbank(Textfiles) entsprechen.
        //Es wird mit zwei Listen gearbeitet, beide sind als Listen implementiert und werden mit den Werten
        //der textfile gefüllt.
        //Die Textdateien sind so aufgebaut, dass der 6. Wert der Username Datei zum 6. Wert der Passwort Datei gehört.
        //Dieser Vergleich wird dann am Ende der Funktion auch ausgeführt.
        public void validate(string UN, string PW)
        {
            resetErrorSigns();

            //Zählt wie viele Lines es in der Datei gibt (für den Loop)
            int lineCount = File.ReadLines(dateipfadUN).Count();

            //Diese beiden Container werden mit allen Login Daten gefüllt
            List<string> ContainerUN = currentUsernames(lineCount);
            List<string> ContainerPW = currentPasswords(lineCount);

            int index = 0;

            for (int zl = 0; zl < lineCount; zl++)
            {
                //Checken ob eingegebener Username in der Datennbank(Textdatei) existiert
                if (ContainerUN[zl]==UN)
                {
                    index = zl;//Index merken
                    zl = lineCount; //for schleife vorzeitig beenden
                }
            }

            //Vergleicht ob eingabe und datenbankeintrag bei passwort und username identisch sind
            if (ContainerUN[index] == UN && ContainerPW[index]==PW)
            {
                Window w1 = new WindowSigned();
                w1.Show();
                this.Hide();
            }
            //Wenn Username stimmt, Passwort aber nicht, dann soll eine Fehlermeldung ausgegeben werden
            else if (ContainerUN[index]==UN && ContainerPW[index] != PW)
            {
                this.ErrorWrongPassword.Visibility = Visibility.Visible;
            }
            //Wenn weder UN noch PW stimmen, soll entsprechende Fehlermeldung dafür ausgegeben werden
            else
            {
                this.ErrorUserNotExists.Visibility = Visibility.Visible;
            }
        }
    }
}
