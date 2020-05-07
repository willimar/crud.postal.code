using crud.api.core.repositories;
using graph.simplify.core.queries;
using postal.code.api.GraphQL.Types;
using postal.code.core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace postal.code.api.GraphQL.Queries
{
    public class PostalCodeQuery : AppQuery<Address, PostalCodeType>
    {
        public PostalCodeQuery(IRepository<Address> repository) : base(repository)
        {
        }
    }
}
