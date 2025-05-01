using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaHotelaria.Models.Entities.Enums;
using SistemaHotelaria.Models.Exception;

namespace SistemaHotelaria.Models.Entities
{
    class Reservation
    {
        private List<Person> Guests { get; set; } = new List<Person>();
        public Suite Suite { get; set; }
        public int ReservedDays { get; set; }

        public Reservation(Suite suite, int reservedDays)
        {
            Suite = suite;
            ReservedDays = reservedDays;
        }

        public void AddGuests(List<Person> guests)
        {
            if (Guests.Count + guests.Count <= Suite.Capacity)
            {
                Guests.AddRange(guests);
            }
            else
            {
                throw new DomainException
                    ("Unable to make reservation. Number of guests exceeded the number of suites.");
            }
        }

        public void AddSuite(Suite suite)
        {
            Suite = suite;
        }

        public int GetQuantityGuests()
        {
            return Guests.Count;
        }

        public decimal GetTotalValue()
        {
            decimal total = ReservedDays * Suite.ValuePerDay;
            if (ReservedDays >= 10)
            {
                total *= 0.9m;
            }
            return total;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Suite Information: {Suite.ToString()}");
            sb.AppendLine("Guests Information:");
            foreach (var guest in Guests)
            {
                sb.AppendLine($"Name: {guest.Name}, Surname: {guest.Surname}, Type: {guest.TypePerson}");
            }
            sb.AppendLine($"Number of guests: {GetQuantityGuests()}");
            sb.AppendLine($"Total value: {GetTotalValue().ToString("F2")}");
            sb.AppendLine($"Reserved days: {ReservedDays}");

            return sb.ToString();
        }
    }
}
