using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Weather
    {

        public string ParkCode { get; set; }
        public int[] LowTemps { get; set; }
        public int[] HighTemps { get; set; }
        public string[] DaySummaries { get; set; }
        public string[] Messages { get; set; }
        public string TempScale { get; set; }

    }
}