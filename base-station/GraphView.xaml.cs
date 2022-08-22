using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Diagnostics;
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
        public static Stopwatch elapsedtime = new Stopwatch();
        public string Host { get; set; }    
        public string Username { get; set; }    
        public string Password { get; set; } 
        public double datapoint { get; set; }

        public GraphView()
        {
            InitializeComponent();

            elapsedtime.Start();
            var ts = elapsedtime.Elapsed;
          

            Host = MainWindow.host;
            Username = MainWindow.username;
            Password = MainWindow.password;

            var datathread = new ThreadStart(() => addData(Host, Username, Password));
            var backgroundThread = new Thread(datathread);
            backgroundThread.SetApartmentState(ApartmentState.STA);
            backgroundThread.Start();

        }
            
        public void addData (string host, string username, string password)
        {
            SshClient client = new SshClient(host, username, password); 
            client.Connect();

            
            int x = 0;
            //main logic loop
            while (true)
            {
                string output = App.readData(client);

                //main try-catch loop, will make sure program doesnt crash if the uplink computer gives us bad data
                try
                {
                    //convert rpm to double for progressbar use
                    double rpm = Convert.ToDouble(output);

                    //need to use these dispatchers since the values of these objects aren't owned by this thread
                    this.Dispatcher.Invoke(() =>
                    {
                        rpmgraph.Plot.AddPoint(x, rpm);
                        rpmgraph.Render();
                        rpmgraph.Refresh();
                        

                    });
                }

                catch (System.FormatException)
                {
                   
                }

                x++;
                //sleep for a set amount of time
                //TODO: make this a user-specified value
                Thread.Sleep(1000);
            }
        }

    }
  
}

 

