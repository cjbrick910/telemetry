using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Renci.SshNet;
using OxyPlot;
using OxyPlot.Wpf;


namespace base_station
{


    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //"Global" parameters for username, password, and host (needed so that other windows can view the login info)
        public static string username;
        public static string password;
        public static string host;
        public static Boolean loggedIn;
        public static Boolean started;
        public Task readData;
        private static System.Timers.Timer refreshRate;
        public static SshClient client;


        public MainWindow()
        {
            InitializeComponent();
            //not sure if this other InitializeComponent call is needed, but I'm too scared to delete it
            InitializeComponent();
            /*
            var ts = new ThreadStart(() => dataread(host, username, password));
            var backgroundThread = new Thread(ts);
            backgroundThread.SetApartmentState(ApartmentState.STA);
            */
            //readData = new Task(() => dataread(host, username, password));
            SetupTimer();
            
        }

        public void SetupTimer()
        {
            refreshRate = new System.Timers.Timer(1000);
            refreshRate.Elapsed += TimerPulse;

        }


        public void TimerPulse(Object source, ElapsedEventArgs e)
        {
            var rpm = dataread("rpm").Result.ToString();
            this.Dispatcher.Invoke(() =>
            {
                dataout.Text = rpm;
                rpmbar.Value = Convert.ToDouble(rpm);
            });
            

        }

        public async Task<string> dataread(string sensor)
        {
            
            var output = await App.readDataAsync(client, sensor);

            return output;
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
        /*
        public void dataread(string host, string username, string password)
        {
            
            //create and connect client
            SshClient client = new SshClient(host, username, password);
            client.Connect();

            if (client.IsConnected)
            {
                this.Dispatcher.Invoke(() =>
                {
                    status.Text = "Connected!";
                });
            }
            
            //main logic loop
            while (true)
            {
                //this is for the other readData function (not ideal, will delete soon)
                //string output = App.readData(host, username, password);
                
                string outputRotations = App.readData(client, "rpm");

                string outputPressure = App.readData(client, "breakPressure");

                //main try-catch loop, will make sure program doesnt crash if the uplink computer gives us bad data
                try
                {
                    //convert rpm to double for progressbar use
                    double rpm = Convert.ToDouble(outputRotations);

                    double breakPressure = Convert.ToDouble(outputPressure);

                    //need to use these dispatchers since the values of these objects aren't owned by this thread
                    this.Dispatcher.Invoke(() =>
                    {
                        //setting value of text block and progressbar
                        dataout.Text = outputRotations;
                        rpmbar.Value = rpm;
                        breakPressureDisplay.Text = outputPressure;
                    });
                }

                catch(System.FormatException)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        status.Text = "Error: Data Format is in the wrong format";
                    });
                }

                //sleep for a set amount of time
                //TODO: make this a user-specified value
                Thread.Sleep(1000);
            }
        }
        */

        /*
         * #####################
         * # sshLogin Function #
         * #####################
         * 
         * Is called when the "Login" button is pressed on the main window
         */
        public void sshLogin(object sender, RoutedEventArgs e)
        {
            
            username = userInput.Text.ToString();
            password = SSH_Password.Password.ToString();
            host = ipAddress.Text.ToString();
            loggedIn = true;

            client = new SshClient(host, username, password);
            client.Connect();
            if (client.IsConnected)
            {
                status.Text = "Connected!";
            }



            /*
            var ts = new ThreadStart(() => dataread(host, username, password));
            var backgroundThread = new Thread(ts);
            backgroundThread.SetApartmentState(ApartmentState.STA);
            backgroundThread.Start();
            */


            //status.Text = App.Connect(password);
            /*
            while (true)
            {
                dataout.Text = App.readData(password);
            }
            */
        }

       

        /*
         * ########################
         * # sendCommand Function #
         * ########################
         * 
         * Is called when the "Send" button is pressed in the main window
         * requires user info to be put in
         */
        private void sendCommand(object sender, RoutedEventArgs e)
        {

            string username = userInput.Text.ToString();
            string host = ipAddress.Text.ToString();
            string command = commandInput.Text.ToString();
            string password = SSH_Password.Password.ToString();

            string commandOutput = App.sendCommand(host, username, command, password);

            output.Text = commandOutput;
           
        }
        
        /*
         * ##############################
         * # Menu Bar Interaction Logic #
         * ##############################
         * 
         * Handles the interaction logic for the different menu bar options
         * 
         */
        //view the About Window
        private void AboutWindow(object sender, RoutedEventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.Show();
        }

        //view the Graph Window
        private void viewGraph(object sender, RoutedEventArgs e)
        {
            GraphView graphWindow = new GraphView();
            graphWindow.Show();
            
        }

        //exit the application
        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void startStopLogic(object sender, RoutedEventArgs e)
        {

            if (loggedIn)
            {
                
                if (started == false)
                {

                    refreshRate.Start();
                    started = true;
                    startStop.Content = "Stop";
                }
                else if (started == true)
                {
                    refreshRate.Stop();
                    started = false;
                    startStop.Content = "Start";
                }
            }
        }
    }
}
