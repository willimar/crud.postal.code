using crud.api.core.attributes;
using crud.api.core.entities;

namespace postal.code.core.entities
{
    public class State: BaseEntity
    {
        [IsRequiredField]
        public string Name { get; set; }
        [IsRequiredField]
        public string Initials { get; set; }
        [IsRequiredField]
        public Country Country { get; set; }
    }
}