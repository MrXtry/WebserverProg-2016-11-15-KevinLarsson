using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebserverProg_2016_11_15_KevinLarsson
{
    class Robot
    {
        public Robot(string name, int xValue, int yValue)
        {
            Name = name;
            XValue = xValue;
            YValue = yValue;
        }
        public string Name { get; set; }
        public int XValue { get; set; }
        public int YValue { get; set; }
    }
}
