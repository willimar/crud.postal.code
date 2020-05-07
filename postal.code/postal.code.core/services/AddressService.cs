using crud.api.core.repositories;
using crud.api.core.services;
using postal.code.core.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace postal.code.core.services
{
    public class AddressService : BaseService<Address>
    {
        public AddressService(IRepository<Address> repository) : base(repository)
        {
        }
    }
}
