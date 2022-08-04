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
using Renci.SshNet;

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

        /*
         * #####################
         * # dataread function #
         * #####################
         * 
         * takes in host, username, password
         * creates client and constantly calls the readData function
         * 
         */
        public void dataread(string host, string username, string password)
        {
            
            //create and connect client
            SshClient client = new SshClient(host, username, password);
            client.Connect();
            
            //main logic loop
            while (true)
            {
                //this is for the other readData function (not ideal, will delete soon)
                //string output = App.readData(host, username, password);

                string output = App.readData(client);
                //TODO: make this a real value
                string rpmtest = "1200.00";

                //convert rpm to double for progressbar use
                double rpm = Convert.ToDouble(rpmtest);
                this.Dispatcher.Invoke(() =>
                {
                    //setting value of text block and progressbar
                    dataout.Text = output;
                    rpmbar.Value = rpm;
                });
                //sleep for a set amount of time
                //TODO: make this a user-specified value
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
