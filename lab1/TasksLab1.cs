using ConsoleApp2;
namespace ConsoleApp2
{
    /// <summary>
    /// Zestaw metod realizujących zadania laboratoryjne 1
    /// </summary>
    /// <remarks>
    /// Autor: Raf<br/>
    /// Data: 12.10.2025
    /// Środiwsko: .net8.0
    /// </remarks>
    internal class TasksLab1
    {
        public void Run()
        {
            //Zadanie1();
            //Zadanie2();
            //Zadanie3();
            //Zadanie4();
            Zadanie5();
        }
        /// <summary>
        /// Zadanie 1 : Oblicza wyróznik delty i wyznaczna pierwiastki równania kwadratowego
        /// </summary>
        /// <remarks> Wyniki wypisywane są na konsole</remarks>

        /// <summary>
        /// Losuje tablicę n liczb double z przedziału [min, max]. 
        /// </summary> 

        private void SortWstaw(int[] arr)
        {

            for (int i = 2; i < arr.Length; i++)
            {
              
                int klucz = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > klucz)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }

                    arr[j + 1] = klucz;
            }
        }
        private void SortBabel(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int v1 = arr[j];
                        int v2 = arr[j + 1];
                        arr[j] = v2;
                        arr[j + 1] = v1;
                    }
                }
            }
        }
        private void WyswietlTablice(int[] arr) 
        {
            Console.Write("Tablica: ");

            for (int i = 0; i < arr.Length; i++) 
            {
                Console.Write(arr[i]+",");
            }
        }
        private int[] LosujTabliceInt(int n, int min, int max)
        {
            var rng = new Random(); // opcjonalnie: nowy Random(seed) dla powtarzalności
            int[] arr = new int[n];
            int zakres = max - min;
            for (int i = 0; i < n; i++)
            {
                // NextDouble() -> [0,1), skalujemy do [min, max]
                double los;
                los = min + rng.NextDouble() * zakres;
                arr[i] = Convert.ToInt32(los);
            }
            return arr;
        }
        private int[] WprowadzTabliceInt(int n)
        {
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write("Podaj liczbe "+i+" :");
                int liczba = Convert.ToInt32(Console.ReadLine());
                //if (liczba >= 0 || liczba < 0)
                //{
                    arr[i] = liczba;
                //}
                //else
                //{
                //    liczba = 0;
                //}
            }
            return arr;
        }
        private double[] LosujTabliceDouble(int n, double min, double max)
        {
            var rng = new Random(); // opcjonalnie: nowy Random(seed) dla powtarzalności
            double[] arr = new double[n];
            double zakres = max - min;
            for (int i = 0; i < n; i++)
            { 
                // NextDouble() -> [0,1), skalujemy do [min, max]
                arr[i] = min + rng.NextDouble() * zakres; 
            }
            return arr; 
        }
        private double LosujLiczbeOdUzytkownika()
        {
            Console.Write("Podaj dolną granicę przedziału (min): "); 
            double min = Convert.ToDouble(Console.ReadLine());

            Console.Write("Podaj górną granicę przedziału (max): "); 
            double max = Convert.ToDouble(Console.ReadLine());

            if (min > max) 
            { 
                double temp = min;
                min = max;
                max = temp; 
                Console.WriteLine($"Zamieniono granice. Nowy przedział: [{min}, {max}]");
            }
            Random rng = new Random(); 
            double wynik = min + rng.NextDouble() * (max - min);

            Console.WriteLine($"Wylosowana liczba: {wynik:F2}");
            return wynik;
        }
        private void Zadanie5()
        {
            //Napisz program umożliwiający wprowadzanie n liczb 
            //oraz sortujący te liczby metodą bąbelkową lub wstawiania. 
            //Wyniki wyświetlaj na konsoli.
            //Console.Write("Podaj liczbe " + i + " :");
            //int liczba = Convert.ToInt32(Console.ReadLine());
            //WprowadzTabliceInt(liczba);
            int[] tablica;
            //tablica = WprowadzTabliceInt(4);
            tablica = LosujTabliceInt(6, 0, 10);
            WyswietlTablice(tablica);
            SortBabel(tablica);
            Console.Write("Po sortowaniu babelkowym: ");
            WyswietlTablice(tablica);
        }
        private void Zadanie4() 
        {
            while (true)
            {
                Console.WriteLine("podaj liczbe: ");
                //convert do 32bitowy
                int liczba = Convert.ToInt32(Console.ReadLine());
                if (liczba < 0) 
                {
                    break;
                }
            }
            
        }

        private void Zadanie3()
        {
            for (int i = 20; i >= 0; i--)
            {
                if (i == 19 || i == 15 || i == 9 || i == 6 || i == 2)
                {
                    continue;
                }
                Console.WriteLine(i);
            }
        }
        private void Zadanie2()
        {
            double[] arr;
            arr = LosujTabliceDouble(10, 1, 100);
            int suma = 0;
            //int[] arr2 = new int[arr.Length];
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    arr2[i] = (int)arr[i];
            //}
            for (int i = 0; i < arr.Length; i++)
            {
                //suma += arr2[i];
                suma += (int)arr[i];
            }
            Console.WriteLine($"Suma elementów: {suma}");
        }

        private void Zadanie1()
        {
            double delta, x1, x2;
            Console.WriteLine("Podaj wartość a :");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Podaj wartość b :");
            double b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Podaj wartość c :");
            double c = Convert.ToDouble(Console.ReadLine());
            if (a != 0)
            {
                delta = (b * b) - (4 * a * c);
                if (delta < 0)
                    Console.WriteLine("Brak rozwiązania w zbiorze liczb rzeczywistych");
                else if (delta > 0)
                {
                    x1 = (-b - Math.Sqrt(delta)) / (2 * a);
                    x2 = (-b + Math.Sqrt(delta)) / (2 * a);
                    Console.WriteLine($"Dwa rozwiązania x1 = {x1:F2}, \t x2 = {x2:F2}");
                    Console.WriteLine("Dwa rozwiąznaia x1 = " + x1 + " x2 = " + x2);
                }
                else
                {
                    x1 = -b / (2 * a);
                    Console.WriteLine($"Jedno rozwiązania x1 = {x1:F2}");
                }
            }
            else Console.WriteLine("To nie jest rownanie kwadratowe");
        }

    }

}