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

        static void Main(string[] args)
        {
            SelectConversion();
        }

        static void SelectConversion()
        {
            Console.WriteLine("Select the conversion you want to carry out.");
            Console.WriteLine("1) GPB to USD");
            Console.WriteLine("2) GPB to AUD");
            Console.WriteLine("3) GPB to EUR");
            Console.WriteLine("Simply type Exit to quit.");
            string choiceString = Console.ReadLine();
            if (choiceString.ToUpper() != "EXIT")
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
            Console.WriteLine(inputString + " in " + conversionRate.InputCurrency + " is " + output.ToString() + " in " + conversionRate.OutputCurrency + ".");
            SelectConversion();
        }
    }
}
