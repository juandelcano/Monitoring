using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMonitoring.ViewModel
{
    public class ChartVM
    {
        public List<String> XCoordinate { get; set; }

        public List<String> TitleElement { get; set; }
        public List<double> ListValue { get; set; }
    }
}
