using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

namespace radio
{

    #region Feladatok

    class Program
    {
        private static readonly List<Adas> Adasok = new List<Adas>(); // Adas tipusu lista


        private static void Feladat1()
        {
            var reader = new StreamReader("veetel.txt"); // veetel.txt beolvasasa

            while (!reader.EndOfStream)
            {
                Adasok.Add(new Adas(reader.ReadLine().Split(' '), reader.ReadLine()));
            }

            reader.Close();
        }

        private static void Feladat2()
        {
            WriteLine("2.Feladat");
            WriteLine(Adasok.First().Sorszam + ". sorszamu"); // elso adas sorszama
            WriteLine(Adasok.Last().Sorszam + ". sorszamu"); // utolso adas sorszama
        }


        private static void Feladat3()
        {
            WriteLine();
            WriteLine("3.Feladat");
            WriteLine("Ezekben a feljegyzesekben megtalalhato a 'farkas' :");

            foreach (Adas a in Adasok) // vegigmegyunk az adasokon
            {
                if (a.VanFarkas) // ha tartalmazza a "farkas" szot kiirjuk a konzolba
                {
                    WriteLine(a.Napszam + ". nap " + a.Sorszam + ". radioamator");
                }
            }
        }


        private static void Feladat4()
        {
            int[] statisztika = new int[11]; // 11 elemu integer tomb(11 nap van)

            foreach (Adas a in Adasok)
                statisztika[a.Napszam - 1]++; // vegigmegyunk az adasokon, noveljuk a statisztika tomb megfelelo elemet

            WriteLine();
            WriteLine("4.Feladat");

            for (int i = 0; i < statisztika.Length; i++) // vegigmegyunk a statisztika tombon, kiirjuk a konzolba
            {
                WriteLine("A(z) " + (i + 1) + ". napon " + statisztika[i] + " darab feljegyzes volt.");
            }
        }


        private static readonly Helyes[] HelyesAdasok = new Helyes[11]; // 11 elemu Helyes tomb(11 nap van)

        private static void Feladat5()
        {
            for (int i = 0; i < 11; i++) // letrehozunk 11db Helyes objektumot
                HelyesAdasok[i] = new Helyes();


            for (int k = 0; k < 90; k++) // 0. tol 90. karakterig vegigmegyunk a sorokon
            {
                foreach (Adas a in Adasok) // 90-szer vegigmegyunk az osszes adason
                {
                    if ((a.Uzenet[k] != '#') && !HelyesAdasok[a.Napszam - 1].Rendezett[k])
                        // ha az a. adas k. karaktere nem #, es a helyreallitott resz soron kovetkezo karaktere meg nincs a helyen
                    {
                        HelyesAdasok[a.Napszam - 1].Rendezettstring += a.Uzenet[k];
                            // ezesetben az a. adas k. karakteret hozzafuzzuk a helyreallitott reszhez
                        HelyesAdasok[a.Napszam - 1].Rendezett[k] = true; // es jelezzuk hogy a karakter a helyere kerult
                    }
                }
            }

            WriteLine();
            WriteLine("5.Feladat");
            for (int i = 0; i < 11; i++)
                WriteLine(HelyesAdasok[i].Rendezettstring); // kiirjuk a konzolba a helyreallitott stringeket
            WriteLine();
        }

        private static void Feladat7()
        {
            WriteLine("7.Feladat");
            Write("Add meg egy nap sorszamat!:  ");
            var napszam = int.Parse(ReadLine()); // bekerjuk a nap sorszamat
            WriteLine();
            Write("Add meg egy radioamator sorszamat!:  ");
            ReadLine();
            WriteLine("A megfigyelt egyedek szama: " + HelyesAdasok[napszam - 1].Egyedek);
        }


        private static void Main()
        {
            Feladat1(); //1. feladat
            Feladat2(); //2. feladat
            Feladat3(); //3. feladat
            Feladat4(); //4. feladat
            Feladat5(); //5. feladat
            Feladat7(); //7. feladat
            ReadLine();
        }
    }

    #endregion

    #region Osztalyok

    class Helyes
    {
        public bool[] Rendezett = new bool[90];
        public string Rendezettstring = "";

        public string Egyedek
        {
            get
            {
                string[] split = Rendezettstring.Split(' ')[0].Split('/');
                    // elso elvalasztokarakter: space > [0]= space elott, [1] = space mogott, masodik: /  > elotti es utani elemeket belerakjuk a split tombbe
                if (szame(split[0])) // ha a split tomb elso eleme szam ( amugy akkor a masodik is az )
                {
                    int osszeg = int.Parse(split[0]) + int.Parse(split[1]);
                        // osszeadjuk a kifejlett es kolyok egyedek szamat
                    if (osszeg == 0) // ha az osszeg 0
                    {
                        return "Nincs informacio"; // akkor nincs informacio
                    }
                    return osszeg.ToString(); // ha nem nulla akkor a visszatero ertek = osszeg
                }
                return "Nincs ilyen feljegyzés"; // ha a split tomb eleme nem szam akkor nem volt feljegyzes
            }
        }

        private bool szame(string szo)
        {
            for (var i = 0; i < szo.Length; i++)
            {
                if ((szo[i] < '0') || (szo[i] > '9')) return false;
            }

            return true;
        }
    }

    class Adas
    {
        public int Napszam;
        public int Sorszam;
        public string Uzenet;
        public bool VanFarkas;

        public Adas(string[] sor1, string sor2)
        {
            Napszam = int.Parse(sor1[0]);
            Sorszam = int.Parse(sor1[1]);
            Uzenet = sor2;
            VanFarkas = Farkasvanebenne();
        }

        public bool Farkasvanebenne()
        {
            return Uzenet.Contains("farkas");
        }
    }

    #endregion
}