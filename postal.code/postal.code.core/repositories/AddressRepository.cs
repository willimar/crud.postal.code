using crud.api.core.repositories;
using data.provider.core;
using postal.code.core.entities;

namespace postal.code.core.repositories
{
    public class AddressRepository : BaseRepository<Address>
    {
        public AddressRepository(IDataProvider provider) : base(provider)
        {
        }
    }
}
