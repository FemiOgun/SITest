using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SkyInsuranceThird.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public string Registration { get; set; }
        public string MakeModel { get; set; }

        //internal static List<Vehicle> List()
        //{
        //    throw new NotImplementedException();
        //}
    }
}