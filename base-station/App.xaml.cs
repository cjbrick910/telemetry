using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Renci.SshNet;
using Renci.SshNet.Common;
using OxyPlot;
using OxyPlot.Wpf;

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
            try
            {
                //create a new SshClient object and connect
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
            //catching if there's no arguments
            catch (System.ArgumentException) {
                MessageBox.Show("Please Put in your credentials");
            }
            return "Standby";
        }
        public static async Task<string> readDataAsync(SshClient client, string sensor)
        {
            SshCommand command;
            /*
             * ##########################
             * # Sensor Selection Logic #
             * ##########################
             * 
             * Based on the string we get from the calling function, we can choose what sensor we get info from and therefore what info that function recieves
             * TODO: Find a way to better differentiate the sensors
             * 
             */
            switch (sensor)
            {
                //Sends Brake Pressure from the pressure sensor
                case "breakPressure":

                    command = client.CreateCommand("head -n 1 /dev/tty/USB1 | cut -b 1-4");


                    string pressure = command.Execute();

                    return pressure;
                //Sends RPM from the hall effect
                case "rpm":

                    command = client.CreateCommand("head -n 1 /dev/tty/USB0  | cut -b 1-3");

                    //command to test output
                    //command = client.CreateCommand("echo \"hello\"");
                    
                    string rotations = command.Execute();

                    return rotations;
                //Sends nothing if the string recieved is something different
                default:
                    return "null";
            }

            //Old Data Output logic, has just one serial port it gets data from

            // var command = client.CreateCommand("head -n 1 /dev/ttyUSB0 | cut -b 1-4");

            // string data = command.Execute();

            // return data;
        }
        /*
         * #####################
         * # readData Function #
         * #####################
         *
         * Takes in Host, Username, Command, and Password 
         * Creates new client and connects, then creates command and executes it
         * Returns a string with the data (It will not format the string automatically, that should be done on the other side)
         */
        public static string readData(SshClient client, string sensor)
        {
            SshCommand command;
            /*
             * ##########################
             * # Sensor Selection Logic #
             * ##########################
             * 
             * Based on the string we get from the calling function, we can choose what sensor we get info from and therefore what info that function recieves
             * TODO: Find a way to better differentiate the sensors
             * 
             */
            switch(sensor)
            {
                //Sends Brake Pressure from the pressure sensor
                case "breakPressure":

                    command = client.CreateCommand("head -n 1 /dev/tty/USB1 | cut -b 1-4");


                    string pressure = command.Execute();

                    return pressure; 
                //Sends RPM from the hall effect
                case "rpm":

                    command = client.CreateCommand("head -n 1 /dev/tty/USB0  | cut -b 1-3");
                
                    string rotations = command.Execute();

                    return rotations;
                //Sends nothing if the string recieved is something different
                default:
                    return "null";
            }

            //Old Data Output logic, has just one serial port it gets data from

            // var command = client.CreateCommand("head -n 1 /dev/ttyUSB0 | cut -b 1-4");
    
            // string data = command.Execute();
            
            // return data;
        }
        
    }
}
