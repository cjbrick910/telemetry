using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Wpf;

namespace base_station
{
    /// <summary>
    /// Interaction logic for GraphView.xaml
    /// </summary>
    public partial class GraphView : Window
    {
        public GraphView()
        {
            InitializeComponent();
        }
        
        public void RpmView()
        {


            this.rpmGraph = new PlotModel { Title = "RPM Graph" };
            this.Points = new List<DataPoint>();




        }
        public IList<DataPoint> Points { get; private set; }
        public PlotModel rpmGraph { get; private set; }


    }

    public class RpmViewModel
    {
        
    }
}
