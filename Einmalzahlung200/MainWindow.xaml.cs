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
        public string data = "C:\\Users\\justi\\Desktop\\Einmalzahlung200\\allData.txt";
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

        //Diese Funktion checkt ob die eingegebenen Daten denen der Datenbank(Textfiles) entsprechen..
        //
        public void validate(string UN, string PW)
        {
            resetErrorSigns();

            //Zählt wie viele Lines es in der Datei gibt (für den Loop)
            int lineCount = File.ReadLines(data).Count();

            bool UNexists = false;
            bool PWcorrect = false;
            //Dieses Array wird temporär angelegt um die Userdata der aktuellen Person aus der Datenbank zwischen zu speichern
            string[] userData = new string[3];

            //Hier wird geschaut ob der Username überhaupt existiert
            //Der StreamReader loopt durch die gesamten Dateien und füllt den Array userData mit den Daten des gesuchten Accounts
            //sofern dieser existiert.
            StreamReader rd = new StreamReader(data);
            for (int zl = 0; zl < lineCount; zl++)
            {
                string dataline = rd.ReadLine();
                string[] datalineArray = dataline.Split(new char[] { '|' });
                //Checken ob es den Username überhaupt gibt

                if (datalineArray[0].ToString() == UN)
                {
                    UNexists = true;
                    zl = lineCount; //forschleife beenden, da Value gefunden wurde
                    userData = datalineArray; //Daten in Array außerhalb der loop abspeichern
                }
            }
            rd.Close();

            //Im folgenden werden Username und Passwort auf existenz und richtigkeit überprüft
            if (!UNexists)
            {
                this.ErrorUserNotExists.Visibility = Visibility.Visible;
            }

            if (UNexists && !PWcorrect)
            {
                this.ErrorWrongPassword.Visibility = Visibility.Visible;
            }

            //Checken ob das Passwort aus Datenbank mit eingegebenen Passwort übereinstimmt
            if (userData[1] == PW)
            {
                PWcorrect = true;
            }

            //Nur wenn Username existiert und Passwort korrekt ist, kommt man in die Log-In Form.
            if (UNexists && PWcorrect)
            {
                //Daten mit denen im neuen Fenster angemeldet wird, werden zugleich an das neue Fenster übertragen
                Window w1 = new WindowSigned(userData[0], userData[1], userData[2]);
                w1.Show();
                this.Hide();
            }
        }
    }
}
