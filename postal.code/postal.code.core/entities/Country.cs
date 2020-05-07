using crud.api.core.attributes;
using crud.api.core.entities;

namespace postal.code.core.entities
{
    public class Country: BaseEntity
    {
        [IsRequiredField]
        public string Name { get; set; }
        [IsRequiredField]
        public string Initials { get; set; }
    }
}