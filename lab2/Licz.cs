using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class Licz
    {
        private int value;

        public Licz(int value)
        {
            this.value = value;
        }

        public void Dodaj(int liczba)
        {
            value += liczba;
        }

        public void Odejmij(int liczba)
        {
            value -= liczba;
        }

        public void Wyswietl()
        {
            Console.WriteLine("Wartosc " + value);
        }
    }
}