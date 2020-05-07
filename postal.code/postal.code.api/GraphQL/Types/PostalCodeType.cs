using GraphQL.Types;
using postal.code.core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace postal.code.api.GraphQL.Types
{
    public class PostalCodeType : ObjectGraphType<Address>
    {
        public PostalCodeType()
        {
            Field(x => x.Id, type: typeof(GuidGraphType)).Description("Property Id is Guid type and unique in database.");
            
            Field(x => x.RegisterDate).Description("The date and time that record was included in data base.");
            Field(x => x.LastChangeDate).Description("The date and time that record was changed in data base.");
            Field(x => x.PublicPlace).Description("The public place to address. informe a value like avenue, road or other type.");
            Field(x => x.StreetName).Description("The name of street from the address.");
            Field(x => x.FullStreetName).Description("The street name and public place.");
            Field(x => x.PostalCode).Description("The code postal of the place.");
            Field(x => x.District).Description("The district of the address.");

            //Field(x => x.City).Description("The City of the address.");

            Field(type: typeof(CityType), nameof(City));
        }
    }
}
