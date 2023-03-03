using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace Einmalzahlung200
{
    /// <summary>
    /// Interaktionslogik für WindowSigned.xaml
    /// </summary>
    public partial class WindowSigned : Window
    {
        string dateipfadDT = "C:\\Users\\justi\\Desktop\\Einmalzahlung200\\data.txt";
        public WindowSigned()
        {
            InitializeComponent();
            this.Background = Brushes.White;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Height = 500;
            this.Width = 400;
            this.LogInBeta.Foreground = (Brush)(new BrushConverter().ConvertFrom("#007986"));
            this.SignOut.Background= (Brush)(new BrushConverter().ConvertFrom("#007986"));
            this.SignOut.Foreground = Brushes.White;
            this.ErrorLogo1.Visibility = Visibility.Hidden;
            this.typeKeyLabelError.Foreground = (Brush)(new BrushConverter().ConvertFrom("#F15249"));
            this.typeKeyLabelError.Visibility = Visibility.Hidden;
            this.ErrorLogo2.Visibility = Visibility.Hidden;
            this.chooseIbanLabelError.Foreground = (Brush)(new BrushConverter().ConvertFrom("#F15249"));
            this.chooseIbanLabelError.Visibility = Visibility.Hidden;

        }
        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w1 = new MainWindow();
            w1.Show();
            this.Close();
        }

        private void resetErrorSigns()
        {
            this.ErrorLogo1.Visibility = Visibility.Hidden;
            this.typeKeyLabelError.Visibility = Visibility.Hidden;
            this.ErrorLogo2.Visibility = Visibility.Hidden;
            this.chooseIbanLabelError.Visibility = Visibility.Hidden;
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            //B_Correct wird benutzt um alle if loops abzulaufen und zu schauen,
            //ob irgendwo noch fehler auftreten. Solange es false ist werden keine Daten
            //abgespeichert. Dies ermöglicht im gegensatz zur bloßen verwendung von "return" in der If Kondition
            //Dass beide Konditionen geprüft werden und auch zwei fehler angezeigt werden

            bool b_correct = true;
            //Bei jedem neuklicken Fehler resetten
            resetErrorSigns();
            if (typeKey.Text.Length == 0)
            {
                this.typeKeyLabelError.Visibility = Visibility.Visible;
                this.ErrorLogo1.Visibility = Visibility.Visible;
                b_correct = false;
            }

            if (chooseIban.Text.Length != 22)
            {
                this.chooseIbanLabelError.Visibility = Visibility.Visible;
                this.ErrorLogo2.Visibility = Visibility.Visible;
                b_correct = false;
            }
            //Hier wird die Funnktion dann abgebrochen wenn in einem der beiden Felder ein Fehler 
            //zu finden war
            if (b_correct == false)
            {
                return;
            }

            StreamWriter stIBAN = new StreamWriter(dateipfadDT, true);
            stIBAN.WriteLine(chooseIban.Text);
            stIBAN.Close();
        }
    }
}
