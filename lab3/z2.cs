using System;

namespace CarsLab
{
    
    public class Samochod
    {
        
        private string marka;
        private string model;
        private string nadwozie;
        private string kolor;
        private int rokProdukcji;
        private double przebieg;

        public string Marka { get { return marka; } set { marka = value; } }
        public string Model { get { return model; } set { model = value; } }
        public string Nadwozie { get { return nadwozie; } set { nadwozie = value; } }
        public string Kolor { get { return kolor; } set { kolor = value; } }
        public int RokProdukcji { get { return rokProdukcji; } set { rokProdukcji = value; } }
        public double Przebieg 
        { 
            get { return przebieg; } 
            set 
            { 
                if(value < 0) 
                    przebieg = 0; 
                else 
                    przebieg = value; 
            } 
        }

        ==================
        public Samochod(string marka, string model, string nadwozie, string kolor, int rok, double przebieg)
        {
            Marka = marka;
            Model = model;
            Nadwozie = nadwozie;
            Kolor = kolor;
            RokProdukcji = rok;
            Przebieg = przebieg;
        }

        ==================
        public Samochod()
        {
            Console.Write("Podaj markę: ");
            Marka = Console.ReadLine();

            Console.Write("Podaj model: ");
            Model = Console.ReadLine();

            Console.Write("Podaj rodzaj nadwozia: ");
            Nadwozie = Console.ReadLine();

            Console.Write("Podaj kolor: ");
            Kolor = Console.ReadLine();

            Console.Write("Podaj rok produkcji: ");
            while (!int.TryParse(Console.ReadLine(), out int rok))
            {
                Console.WriteLine("Nieprawidłowa wartość, spróbuj ponownie.");
            }
            RokProdukcji = rok;

            Console.Write("Podaj przebieg: ");
            while (!double.TryParse(Console.ReadLine(), out double przebiegInput) || przebiegInput < 0)
            {
                Console.WriteLine("Przebieg nie może być ujemny. Spróbuj ponownie.");
            }
            Przebieg = przebiegInput;
        }

        
        public virtual void View()
        {
            Console.WriteLine($"Samochód: {Marka} {Model}, Nadwozie: {Nadwozie}, Kolor: {Kolor}, Rok: {RokProdukcji}, Przebieg: {Przebieg} km");
        }
    }

    
    public class SamochodOsobowy : Samochod
    {
        private double waga; 
        private double pojemnoscSilnika; 
        private int iloscOsob;

        public double Waga 
        { 
            get { return waga; } 
            set 
            { 
                if (value < 2 || value > 4.5)
                {
                    Console.WriteLine("Waga musi być w przedziale 2-4,5 t. Ustawiono 2 t.");
                    waga = 2;
                }
                else waga = value; 
            } 
        }

        public double PojemnoscSilnika
        {
            get { return pojemnoscSilnika; }
            set
            {
                if (value < 0.8 || value > 3.0)
                {
                    Console.WriteLine("Pojemność silnika musi być w przedziale 0,8-3,0 l. Ustawiono 1.0 l.");
                    pojemnoscSilnika = 1.0;
                }
                else pojemnoscSilnika = value;
            }
        }

        public int IloscOsob { get { return iloscOsob; } set { iloscOsob = value; } }

        
        public SamochodOsobowy() : base()
        {
            Console.Write("Podaj wagę (2-4,5 t): ");
            while (!double.TryParse(Console.ReadLine(), out double w) || w < 2 || w > 4.5)
            {
                Console.WriteLine("Nieprawidłowa wartość. Spróbuj ponownie.");
            }
            Waga = w;

            Console.Write("Podaj pojemność silnika (0,8-3,0 l): ");
            while (!double.TryParse(Console.ReadLine(), out double p) || p < 0.8 || p > 3.0)
            {
                Console.WriteLine("Nieprawidłowa wartość. Spróbuj ponownie.");
            }
            PojemnoscSilnika = p;

            Console.Write("Podaj ilość osób: ");
            while (!int.TryParse(Console.ReadLine(), out int o) || o < 1)
            {
                Console.WriteLine("Nieprawidłowa wartość. Spróbuj ponownie.");
            }
            IloscOsob = o;
        }

        
        public SamochodOsobowy(string marka, string model, string nadwozie, string kolor, int rok, double przebieg,
                                double waga, double pojemnoscSilnika, int iloscOsob)
            : base(marka, model, nadwozie, kolor, rok, przebieg)
        {
            Waga = waga;
            PojemnoscSilnika = pojemnoscSilnika;
            IloscOsob = iloscOsob;
        }

        
        public override void View()
        {
            base.View();
            Console.WriteLine($"Waga: {Waga} t, Pojemność silnika: {PojemnoscSilnika} l, Ilość osób: {IloscOsob}");
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Tworzenie samochodu osobowego:");
            SamochodOsobowy osobowy = new SamochodOsobowy();
            Console.WriteLine();

            Console.WriteLine("Tworzenie samochodu 1 (konstruktor z parametrami):");
            Samochod auto1 = new Samochod("Toyota", "Corolla", "Sedan", "Czerwony", 2018, 50000);
            Console.WriteLine();

            Console.WriteLine("Tworzenie samochodu 2 (konstruktor od użytkownika):");
            Samochod auto2 = new Samochod();
            Console.WriteLine();

            
            Console.WriteLine("Informacje o samochodach:");
            osobowy.View();
            Console.WriteLine();
            auto1.View();
            Console.WriteLine();
            auto2.View();
        }
    }
}
