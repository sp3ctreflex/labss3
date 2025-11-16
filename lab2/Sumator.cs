using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Sumator
    {
        private int[] Liczby;
        public Sumator(int[] liczby)
        {
            Liczby = liczby;
        }
        public int Suma()
        {
            int suma = 0;
            foreach (int liczba in Liczby)
            {
                suma += liczba;
            }
            return suma;
        }
        public int SumaPodziel2()
        {
            int suma = 0;
            foreach (int liczba in Liczby)
            {
                if (liczba % 2 == 0)
                    suma += liczba;
            }
            return suma;
        }
        public int IleElementów()
        {
            return Liczby.Length;
        }
        public void WypiszElemTab()
        {
            Console.WriteLine("Tablica: ");
            foreach (int liczba in Liczby)
            {
                Console.Write(liczba + " ");
            }
        }
        public void WypiszZakres(int lowIndex, int highIndex)
        {
            Console.WriteLine("Elementy od indeksu" + lowIndex + " do " + highIndex + ": ");

            if (lowIndex < 0) lowIndex = 0;
            if (highIndex >= Liczby.Length) highIndex = Liczby.Length - 1;
            if (lowIndex > highIndex)
            {
                Console.WriteLine("Brak elemen w zakr");
                return;
            }

            for (int i = lowIndex; i <= highIndex; i++)
            {
                Console.Write(Liczby[i] + " ");
            }
        }
    }
}
