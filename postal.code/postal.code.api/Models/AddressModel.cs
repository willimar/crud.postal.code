using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace postal.code.api.Models
{
    public class AddressModel
    {
        public string PublicPlace { get; set; }
        public string StreetName { get; set; }
        public string FullStreetName { get; set; }
        public string PostalCode { get; set; }
        public string District { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string StateInitials { get; set; }
        public string CountryName { get; set; }
        public string CountryInitials { get; set; }
    }
}
