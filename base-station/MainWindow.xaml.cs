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

        public void dataread(string host, string username, string password)
        {
            
            
            while (true)
            {
                string output = App.readData(host, username, password);
                string rpmtest = "1200.00";
                double rpm = Convert.ToDouble(rpmtest);
                this.Dispatcher.Invoke(() =>
                {
                    
                    dataout.Text = output;
                    rpmbar.Value = rpm;
                });
                Thread.Sleep(1000);
            }
        }
            


        
        public void sshLogin(object sender, RoutedEventArgs e)
        {
            string username = userInput.Text.ToString();
            string password = SSH_Password.Password.ToString();
            string host = ipAddress.Text.ToString();

            var ts = new ThreadStart(() => dataread(host, username, password));
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
            string username = userInput.Text.ToString();
            string host = ipAddress.Text.ToString();
            string command = commandInput.Text.ToString();
            string password = SSH_Password.Password.ToString();

            string commandOutput = App.sendCommand(host, username, command, password);

            output.Text = commandOutput;

        }


    }
}
