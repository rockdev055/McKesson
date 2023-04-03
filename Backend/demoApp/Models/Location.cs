using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demoApp.Models
{
    public class Location
    {
        public int ID { get; set; }
        public string City { get; set; }
        public TimeSpan Time { get; set; }
    }
}