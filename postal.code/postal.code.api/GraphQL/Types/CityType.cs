using GraphQL.Types;
using postal.code.core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace postal.code.api.GraphQL.Types
{
    public class CityType : ObjectGraphType<City>
    {
        public CityType()
        {
            Field(x => x.Name).Description("The name of the city.");
            Field(typeof(StateType), nameof(State));
        }
    }
}
