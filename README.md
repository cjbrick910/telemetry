# telemetry

A slightly customized way to get data from sensors connected to another device over the network

It uses SSH to login to a linux machine (like a Raspberry Pi) and run commands on it, then shows the output

##Features
* Fully custom GUI using Windows Presentation Forms (WPF)
* RPM readout in both text and visual form
* Custom command input and output window

##TODO
* Add graphs
* Clean up GUI
* Add the ability to save data to a file
* Add readouts from other sensors

##Installation/Building

###From Releases
You can download the most current build from the releases page. This will have the latest stable release in ZIP file format (which includes the executable and required DLLs)

###Building in Visual Studio
The solution has all of the settings you will need to build this, however you should need to install these NuGet packages:
* SSH.NET by Renci
* OxyPlot.Core by OxyPlot
* OxyPlot.Wpf 

Once you have those installed, you should be able to build this like any other c# program in VS