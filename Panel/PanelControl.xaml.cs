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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Umi.UrbanLCA.Panel
{
    public partial class PanelControl : UserControl
    {
        public PanelControl()
        {
            InitializeComponent();
        }
    }

    //Making new class for Bar Chart
    //public class year1
    //{
    //    public string emissionType { get; set; }
    //    public int emissionVal { get; set; }
    //}

    //public class emissions
    //{
    //    public List<year1> emissionList { get; set; } = getEmissions();
    //    public static List<year1>getEmissions()
    //    {
    //        return new List<year1>()
    //        {
    //            new year1() { emissionType = "Embodied", emissionVal = 20},
    //            new year1() { emissionType ="Operational", emissionVal = 80}
    //        }.ToList();
    //    }
    //}
}
