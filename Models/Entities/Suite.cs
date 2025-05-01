using System.Text;
using SistemaHotelaria.Models.Entities.Enums;

namespace SistemaHotelaria.Models.Entities
{
    class Suite
    {
        public TypeSuite SuiteType { get; set; }
        public int Capacity { get; set; }
        public decimal ValuePerDay { get; set; }

        public Suite(TypeSuite suiteType, int capacity, decimal valuePerDay)
        {
            SuiteType = suiteType;
            Capacity = capacity;
            ValuePerDay = valuePerDay;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Suite: {SuiteType}");
            sb.AppendLine($"Capacity: {Capacity}");
            sb.AppendLine($"Value per day: {ValuePerDay.ToString("F2")}");

            return sb.ToString();
        }
    }
}
