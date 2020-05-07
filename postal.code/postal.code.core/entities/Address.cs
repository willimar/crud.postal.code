using crud.api.core.attributes;
using crud.api.core.entities;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;

namespace postal.code.core.entities
{
    public class Address: BaseEntity
    {
        [IsRequiredField]
        public string PublicPlace { get; set; }
        [IsRequiredField]
        public string StreetName { get; set; }
        [IsRequiredField]
        public string FullStreetName { get; set; }
        [IsRequiredField]
        public string PostalCode { get; set; }
        public string District { get; set; }
        [IsRequiredField]
        public City City { get; set; }
    }
}
