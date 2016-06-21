using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Spatial;
using System.ComponentModel.DataAnnotations;

namespace Search_as_a_service_azure.Models
{
    public class Hotel
    {
      
        public string hotelId { get; set; }

        public string hotelName { get; set; }

        public string  baseRate { get; set; }

        public string category { get; set; }

        public string[] tags { get; set; }

        public Boolean parkingIncluded { get; set; }

        public DateTimeOffset  lastRenovationDate { get; set; }

        public  int  rating { get; set; }

        public GeographyPoint location { get; set; }
    }
}