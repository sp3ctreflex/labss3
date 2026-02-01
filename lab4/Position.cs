using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Position : IContract
    {
        public decimal MonthlyRate { get; set; }
        public decimal Overtime { get; set; }

        public Position(decimal monthlyRate, decimal overtime)
        {
            MonthlyRate = monthlyRate;
            Overtime = overtime;
        }

        public decimal Salary() 
        {
            return MonthlyRate + Overtime * (MonthlyRate / 60m);
        }

        public override string? ToString()
        {
            //return base.ToString();
            return $"Umowa w ktorej pensja wynosi: ({MonthlyRate.ToString("C", new CultureInfo("pl-PL"))} + liczba nadgodzin: {Overtime})";
        }
    }
}
