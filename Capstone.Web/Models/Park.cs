using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Park
    {
        public string ParkCode { get; set; }
        public string ImageName { get; set; }
        public string ParkName { get; set; }
        public string State { get; set; }
        public int Acreage { get; set; }
        public int Elevation { get; set; }
        public double TrailMileage { get; set; }
        public int NumberOfCampsites { get; set; }
        public string Climate { get; set; }
        public int YearFounded { get; set; }
        public int AnnualVisitors { get; set; }
        public string Quote { get; set; }
        public string QuoteSrc { get; set; }
        public string Description { get; set; }
        public int EntryFee { get; set; }
        public int AnimalSpecies { get; set; }
        public Weather WeatherForecast { get; set; }

    }
}