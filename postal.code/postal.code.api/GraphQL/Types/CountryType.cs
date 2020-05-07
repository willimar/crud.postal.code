using GraphQL.Types;
using postal.code.core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace postal.code.api.GraphQL.Types
{
    public class CountryType : ObjectGraphType<Country>
    {
        public CountryType()
        {
            Field(x => x.Name).Description("The name of the country.");
            Field(x => x.Initials).Description("The initials of the country.");
        }
    }
}
