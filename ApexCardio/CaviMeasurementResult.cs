using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polycardio2
{
    public class CaviMeasurementResult
    {
        public BodySide Side { get; set; }
        public double Cavi { get; set; }
        public double PwvMetersPerSec { get; set; }
        public double MeanDelayMs { get; set; }
        public int ValidBeatsCount { get; set; }
        public bool IsValid => ValidBeatsCount > 0 && !double.IsNaN(Cavi);
    }
}
