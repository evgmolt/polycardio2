using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polycardio2
{
    public class AbiMeasurementResult
    {
        public double LeftAbi { get; set; }
        public double RightAbi { get; set; }
        public double MaxArmSystolic { get; set; }
        public bool IsLeftValid => LeftAbi > 0;
        public bool IsRightValid => RightAbi > 0;
    }
}
