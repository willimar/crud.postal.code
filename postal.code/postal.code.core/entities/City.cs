using crud.api.core.attributes;
using crud.api.core.entities;

namespace postal.code.core.entities
{
    public class City: BaseEntity
    {
        [IsRequiredField]
        public string Name { get; set; }
        [IsRequiredField]
        public State State { get; set; }
    }
}