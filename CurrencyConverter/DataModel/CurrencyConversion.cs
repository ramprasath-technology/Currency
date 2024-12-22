using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.DataModel
{
    public class CurrencyConversion
    {
        public bool Success { get; set; }
        public int TimeStamp { get; set; }
        public string Base { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public Rates Rates { get; set; } = new Rates();
    }
}
