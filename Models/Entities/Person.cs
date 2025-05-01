using SistemaHotelaria.Models.Entities.Enums;

namespace SistemaHotelaria.Models.Entities
{
    class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public TypePerson TypePerson { get; set; }

        public Person(string name, string surname, TypePerson typePerson)
        {
            Name = name;
            Surname = surname;
            TypePerson = typePerson;
        }
    }
}
