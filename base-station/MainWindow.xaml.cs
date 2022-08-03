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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace base_station
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            
            
            InitializeComponent();

        }

        private void sshLogin(object sender, RoutedEventArgs e)
        {
            string password = SSH_Password.Password.ToString();

            status.Text = App.Connect(password);
        }

        private void sendCommand(object sender, RoutedEventArgs e)
        {
            string command = commandInput.Text.ToString();
            string password = SSH_Password.Password.ToString();

            string commandOutput = App.sendCommand(command, password);

            output.Text = commandOutput;

        }
    }
}
