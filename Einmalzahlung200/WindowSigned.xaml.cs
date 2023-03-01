using System;
using System.Collections.Generic;
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

namespace Einmalzahlung200
{
    /// <summary>
    /// Interaktionslogik für WindowSigned.xaml
    /// </summary>
    public partial class WindowSigned : Window
    {
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
        }
        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w1 = new MainWindow();
            w1.Show();
            this.Close();
        }
    }
}
