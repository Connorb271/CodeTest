using CurrencyConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter
{
    class Program
    {
        private static ConversionRate GBPUSD = new ConversionRate() { InputCurrency = "GPB", OutputCurrency = "USD", Rate = 1.2M };
        private static ConversionRate GBPAUD = new ConversionRate() { InputCurrency = "GPB", OutputCurrency = "AUD", Rate = 1.8M };
        private static ConversionRate GBPEUR = new ConversionRate() { InputCurrency = "GPB", OutputCurrency = "EUR", Rate = 1.4M };
        private static List<Conversion> conversions = new List<Conversion>();

        static void Main(string[] args)
        {
            CreateTestData();
            SelectConversion();
        }

        static void SelectConversion()
        {
            Console.WriteLine("Select the conversion you want to carry out.");
            Console.WriteLine("1) GPB to USD");
            Console.WriteLine("2) GPB to AUD");
            Console.WriteLine("3) GPB to EUR");
            Console.WriteLine("Or type Audit to check the logs.");
            Console.WriteLine("Simply type Exit to quit.");
            string choiceString = Console.ReadLine();
            if (choiceString.ToUpper() != "EXIT")
            {
                if(choiceString.ToUpper() == "AUDIT")
                {
                    Console.Clear();
                    Audit();
                } else
                {
                    int choice = 0;
                    if (!int.TryParse(choiceString, out choice))
                    {
                        Console.WriteLine("Please enter a valid input.");
                        SelectConversion();
                    }
                    if (choice < 1 || choice > 3)
                    {
                        Console.WriteLine("Please enter a valid number.");
                        SelectConversion();
                    }
                    switch (choice)
                    {
                        case 1:
                            CompleteConversion(GBPUSD);
                            break;
                        case 2:
                            CompleteConversion(GBPAUD);
                            break;
                        case 3:
                            CompleteConversion(GBPEUR);
                            break;
                        default:
                            Console.WriteLine("Please enter a valid number.");
                            SelectConversion();
                            break;
                    }
                }                
            }            
        }

        private static void CompleteConversion(ConversionRate conversionRate)
        {
            Console.WriteLine(conversionRate.InputCurrency + " to " + conversionRate.OutputCurrency + " selected.");
            Console.WriteLine("Please enter the amount of " + conversionRate.InputCurrency + " you want to convert.");
            string inputString = Console.ReadLine();
            decimal input = 0;
            if (!decimal.TryParse(inputString, out input))
            {
                Console.WriteLine("Please enter a valid number.");
                CompleteConversion(conversionRate);
            }
            if (input < 0 || input > 1000000)
            {
                Console.WriteLine("Please enter a valid number greater than 0 and less than 1,000,000");
                CompleteConversion(conversionRate);
            }
            var output = Math.Round(conversionRate.CompleteConversion(input), 2);
            conversions.Add(new Conversion() { Time = DateTime.UtcNow, Input = input, Output = output, InputCurrency = conversionRate.InputCurrency, OutputCurrency = conversionRate.OutputCurrency, Rate = conversionRate.Rate});
            Console.WriteLine(inputString + " in " + conversionRate.InputCurrency + " is " + output.ToString() + " in " + conversionRate.OutputCurrency + ".");
            SelectConversion();
        }

        private static void Audit()
        {
            Console.WriteLine("Please enter a start date for your query in the format DD/MM/YYYY");
            var startDateString = Console.ReadLine();
            DateTime startDate;
            if (!DateTime.TryParse(startDateString, out startDate))
            {
                Console.WriteLine("Please enter a valid date in the format DD/MM/YYYY");
            }
            Console.WriteLine("Please enter an end date for your query in the format DD/MM/YYYY");
            var endDateString = Console.ReadLine();
            DateTime endDate;
            if (!DateTime.TryParse(endDateString, out endDate))
            {
                Console.WriteLine("Please enter a valid date in the format DD/MM/YYYY");
            }
            var validConversions = conversions.Where(e => e.Time.Date <= endDate && e.Time.Date >= startDate).OrderByDescending(e=>e.Time).ToArray();
            foreach(var conversion in validConversions)
            {
                Console.WriteLine(conversion.ConversionData());
            }
            Console.Write("Press any key to continue.");
            Console.ReadLine();
            SelectConversion();
        }

        private static void CreateTestData()
        {
            DateTime startTime = new DateTime(2000, 1, 1, 10, 0, 0);
            DateTime endTime = DateTime.UtcNow;
            var random = new Random();

            for (var i = 0; i < 1000; i++)
            {
                TimeSpan timeSpan = endTime - startTime;
                TimeSpan newSpan = new TimeSpan(0, random.Next(0, (int)timeSpan.TotalMinutes), 0);
                DateTime newDate = startTime + newSpan;
                var rateDec = (decimal) random.Next(50, 200);
                var inputInt = random.Next(0, 1000000);
                decimal rate = rateDec / 100;
                var output = rate * inputInt;
                string outputCurrency = "AUD";
                if(output > 333)
                {
                    outputCurrency = "USD";
                } 
                if(output > 666)
                {
                    outputCurrency = "EUR";
                }
                conversions.Add(new Conversion() { Input = inputInt, Output = output, Rate = rate, Time = newDate, InputCurrency = "GBP", OutputCurrency = outputCurrency });
            }

        }
    }
}
