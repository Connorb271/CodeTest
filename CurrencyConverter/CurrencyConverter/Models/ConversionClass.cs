using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Models
{
    class ConversionRate
    {
        public decimal CompleteConversion(decimal input)
        {
            return input * Rate;
        }
        public string InputCurrency { get; set; }
        public string OutputCurrency { get; set; }
        public decimal Rate { get; set; }
    }

    class Conversion
    {
        public string ConversionData()
        {
            return InputCurrency + " " + Input + " to " + OutputCurrency + " " + Output + " at a rate of " + Rate + " at " + Time.ToString();
        }
        public decimal Input { get; set; }
        public decimal Output { get; set; }
        public string InputCurrency { get; set; }
        public string OutputCurrency { get; set; }
        public decimal Rate { get; set; }
        public DateTime Time { get; set; }
    }
}
