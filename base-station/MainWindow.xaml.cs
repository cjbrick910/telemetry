using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public void dataread()
        {
            string password = SSH_Password.Password.ToString();
            while (true)
            {
                string output = App.readData(password);
                this.Dispatcher.Invoke(() =>
                {
                    dataout.Text = output;
                });
            }
        }
            


        
        public void sshLogin(object sender, RoutedEventArgs e)
        {
            
            string password = SSH_Password.Password.ToString();
            
            var ts = new ThreadStart(dataread);
            var backgroundThread = new Thread(ts);
            backgroundThread.Start();

            //status.Text = App.Connect(password);
            /*
            while (true)
            {
                dataout.Text = App.readData(password);
            }
            */
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
