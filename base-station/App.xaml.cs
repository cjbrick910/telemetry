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

        

        /*
         * ########################
         * # sendCommand Function #
         * ########################
         *
         * Takes in Host, Username, Command, and Password 
         * Creates new client and connects, then creates command and executes it
         */
        public static string sendCommand(string host, string username, string command, string password)
        {
            //create and connect client
            SshClient client = new SshClient(host, username, password);
            client.Connect();

            //create command and execute it
            var cmd = client.CreateCommand(command);
            string output = cmd.Execute();
            
            //check if data is null; if not, return the data
            if (output == null)
            {
                return "no data";
            }
            else
            {
                return output;
            }
        }

        /*
         * #####################
         * # readData Function #
         * #####################
         *
         * Takes in Host, Username, Command, and Password 
         * Creates new client and connects, then creates command and executes it
         */
        public static string readData(SshClient client)
        {
            var command = client.CreateCommand("head -n 1 /dev/ttyUSB0");
            string data = command.Execute();
            
            return data;
        }
        

        //Other readData function that will create a connection each time it is called (not ideal)

        /*
        public static string readData(string host, string username, string password)
        {
            string data = sendCommand(host, username, "head -n 4 /dev/ttyUSB0", password);
            return data;
        }
        */
    }
}
