﻿using System;
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

        

       
        public static string sendCommand(string command, string password)
        {
            string host = "10.10.0.95";
            string username = "lenin";

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


        public static string readData(string password)
        {
            
            string data = sendCommand("head -n 4 /dev/ttyUSB0", password);
            return data;
        }
    }
}
