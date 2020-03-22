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
}
