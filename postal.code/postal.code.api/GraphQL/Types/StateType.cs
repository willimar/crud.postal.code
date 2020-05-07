using GraphQL.Types;
using postal.code.core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace postal.code.api.GraphQL.Types
{
    public class StateType : ObjectGraphType<State>
    {
        public StateType()
        {
            Field(x => x.Name).Description("The name of the state.");
            Field(x => x.Initials).Description("The initials of the state.");
            Field(typeof(CountryType), nameof(Country));
        }
    }
}
