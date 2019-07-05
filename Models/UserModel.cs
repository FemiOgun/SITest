using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyInsuranceThird.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Postcode { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}