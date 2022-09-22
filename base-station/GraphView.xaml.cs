using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Diagnostics;
using System.Timers;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ScottPlot.WPF;
using ScottPlot;
using Renci.SshNet;
using System.Threading;

namespace base_station
{
    /// <summary>
    /// Interaction logic for GraphView.xaml
    /// </summary>
    public partial class GraphView : Window
    {
        //creating stopwatch for time recording reasons
        public static Stopwatch elapsedtime = new Stopwatch();
        public string Host { get; set; }    
        public string Username { get; set; }    
        public string Password { get; set; } 
        public double datapoint { get; set; }

        public double TotalTime;

        public GraphView()
        {
            InitializeComponent();

            //not being used right now, but will be soon (read TODO In the addData function)
            elapsedtime.Start();
            var ts = elapsedtime.Elapsed;
          
            //this stuff probably isnt needed, but it's nice to have local variables assigned rather than using the global ones (even if the locals are just using the global values)
            Host = MainWindow.host;
            Username = MainWindow.username;
            Password = MainWindow.password;

            MainWindow.refreshRate.Elapsed += TimerPulse;

            if (MainWindow.loggedIn == false)
            {
                MessageBox.Show("Please Enter your Credentials");
            }
            else
            {
                /*
                //starting new thread that calls the addData function (needed because apparently the graph needs to be updated from the GraphView function, it cannot be updated from another function)
                var datathread = new ThreadStart(() => addData(Host, Username, Password, "rpm"));
                var backgroundThread = new Thread(datathread);
                backgroundThread.SetApartmentState(ApartmentState.STA);
                backgroundThread.Start();
                */
            }
        }


        public void TimerPulse(Object source, ElapsedEventArgs e)
        {
            TotalTime = TotalTime + 1000;
            var rpm = Convert.ToDouble(dataread("rpm").Result);
            this.Dispatcher.Invoke(() =>
            {
                //TODO: make the x value a time value instead of just a variable that count's up each time
                rpmgraph.Plot.AddPoint((TotalTime/1000), rpm); //adding a point

                //not sure if these do the same thing, but I'll keep them both just in case (it's not like we're low on memory or storage)
                rpmgraph.Render();
                rpmgraph.Refresh();
            });


        }

        public async Task<string> dataread(string sensor)
        {

            var output = await App.readDataAsync(MainWindow.client, sensor);


            return output;
        }

        /*
         * ####################
         * # addData Function #
         * ####################
         * 
         * takes in host, username, password, and name of sensor to create graph
         * creates a new ssh client and reads the data by calling App.readData
         * updates graph using a dispatcher
         */
        /*
        public void addData (string host, string username, string password, string sensor)
        {
            try
            {
                SshClient client = new SshClient(host, username, password);
                client.Connect();
                
                int x = 0;
                //main logic loop
                while (true)
                {
                    string output = App.readData(client, "rpm");

                    //main try-catch loop, will make sure program doesnt crash if the uplink computer gives us bad data
                    try
                    {
                        //convert rpm to double for graph
                        double rpm = Convert.ToDouble(output);

                        //need to use these dispatchers since the values of these objects aren't owned by this thread
                        this.Dispatcher.Invoke(() =>
                        {
                            //TODO: make the x value a time value instead of just a variable that count's up each time
                            rpmgraph.Plot.AddPoint(x, rpm); //adding a point

                            //not sure if these do the same thing, but I'll keep them both just in case (it's not like we're low on memory or storage)
                            rpmgraph.Render();
                            rpmgraph.Refresh();
                        });
                    }

                    catch (System.FormatException)
                    {
                        //this currently does nothing, I should fix that
                    }

                    x++;
                    //sleep for a set amount of time
                    //TODO: make this a user-specified value
                    Thread.Sleep(1000);
                }
            }

            //TODO: Fix bug where graph will not update even if user puts in info after this message
            
            catch (System.ArgumentNullException)
            {
                //MessageBox.Show("Please Enter your Credentials");
                
            }
            
            
            
        }
        */
    }
  
}

 

