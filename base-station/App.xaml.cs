using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace base_station
{
    
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        

       
        public static string sendCommand(string host, string username, string command, string password)
        {
            SshClient client = new SshClient(host, username, password);
            client.Connect();

            var cmd = client.CreateCommand(command);

            string output = cmd.Execute();

            
            if (output == null)
            {
                return "no data";
            }
            else
            {
                return output;
            }
        }


        public static string readData(string host, string username, string password)
        {
            
            string data = sendCommand(host, username, "head -n 4 /dev/ttyUSB0", password);
            return data;
        }
    }
}
