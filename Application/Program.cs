using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SistemaHotelaria.Models.Entities;
using SistemaHotelaria.Models.Entities.Enums;
using SistemaHotelaria.Models.Exception;

namespace SistemaHotelaria.Application
{
    class Program
    {
        public static int LerInteiro(string prompt, int min, int max)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result) && result >= min && result <= max)
                {
                    return result;
                }
                Console.WriteLine($"Por favor, insira um número entre {min} e {max}.");
            }
        }

        public static int LerInteiroPositivo(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result) && result > 0)
                {
                    return result;
                }
                Console.WriteLine("Por favor, insira um número válido maior que zero.");
            }
        }

        public static decimal LerDecimal(string prompt)
        {
            decimal result;
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                {
                    return result;
                }
                Console.WriteLine("Por favor, insira um valor decimal válido.");
            }
        }

        public static void SalvarReservaEmJson(Reservation reserva)
        {
            string pastaDestino = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Application", "Reservas");

            if (!Directory.Exists(pastaDestino))
            {
                Directory.CreateDirectory(pastaDestino);
            }

            string caminhoArquivo = Path.Combine(pastaDestino, "reserva.json");

            string json = JsonConvert.SerializeObject(reserva, Formatting.Indented);

            File.WriteAllText(caminhoArquivo, json);

            Console.WriteLine("Reserva salva com sucesso no arquivo JSON!");
        }

        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("WELCOME TO THE HOTEL!");

                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Choose between our suites:");
                Console.WriteLine("Premium - 1");
                Console.WriteLine("Luxury - 2");
                Console.WriteLine("Standard - 3");
                int typeSuite = LerInteiro("Type the suite you want: ", 1, 3);

                decimal valuePerDay = LerDecimal("Type the value per day: ");
                int capacity = LerInteiroPositivo("Type the capacity of the suite: ");

                Suite suite = new Suite((TypeSuite)typeSuite, capacity, valuePerDay);
                Console.WriteLine("Suite selected successfully!");
                Console.WriteLine("------------------------------------------");
                Console.ReadKey();
                Console.Clear();

                int n = LerInteiroPositivo("Please inform how many guests: ");
                if (n > capacity)
                {
                    throw new DomainException($"The suite only supports up to {capacity} guests.");
                }

                List<Person> guests = new List<Person>();

                for (int i = 1; i <= n; i++)
                {
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine($"Guest #{i} data:");

                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Surname: ");
                    string surname = Console.ReadLine();

                    int typePerson = LerInteiro("Type of person (1 - Adult, 2 - Child, 3 - Eldery): ", 1, 3);

                    Person person = new Person(name, surname, (TypePerson)typePerson);
                    guests.Add(person);
                    Console.Clear();
                }

                int reservedDays = LerInteiroPositivo("How many days to reserve: ");

                Reservation reservation = new Reservation(suite, reservedDays);
                reservation.AddGuests(guests);
                reservation.AddSuite(suite);

                Console.WriteLine("Reservation made successfully!");
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Reservation details:");
                Console.WriteLine(reservation.ToString());
                Console.WriteLine("------------------------------------------");

                SalvarReservaEmJson(reservation);
            }
            catch (DomainException e)
            {
                Console.WriteLine("Business error: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected error: " + e.Message);
            }
        }
    }
}
