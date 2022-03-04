using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryGame
{
    class Gra
    {
        int poziomTrudnosci;    // 0 - easy, 1 - hard
        int iloscSzans;         // 10 - easy, 15 - hard        

        DaneTekstowe daneTekstowe = new DaneTekstowe();

        string[] matrixA = new string[8];
        string[] matrixB = new string[8];

        string[] mA = new string[8];
        string[] mB = new string[8];

        int[] mA_rozwiazania = new int[8];
        int[] mB_rozwiazania = new int[8];


        // 
        public Gra()
        {
            poziomTrudnosci = 10;
            iloscSzans = 0;

            for (int i = 0; i < 8; i++)
            {
                mA[i] = "X";
                mB[i] = "X";
            }
        }


        //Start - Start the game
        // Returns:
        //      0 - gdy wybrany easy
        //      1 - gry wybrany hard
        public int Start()
        {
            String wybor;
            int wybor_int;
            string message = "Invalid value. Try again.. ";

            Console.WriteLine("\nChoose your difficulty level: ");
            Console.WriteLine("  [0] easy");
            Console.WriteLine("  [1] hard");
            Console.Write("\nChoice: ");

            do
            {
                wybor = Console.ReadLine();

                try
                {
                    wybor_int = int.Parse(wybor);

                    if (wybor_int == 0 || wybor_int == 1)
                    {
                        if (wybor_int == 0)
                        {
                            poziomTrudnosci = 0;
                            iloscSzans = 10;
                        }
                        else
                        {
                            poziomTrudnosci = 1;
                            iloscSzans = 15;
                        }
                    }
                    else
                    {
                        Console.Write(message);
                    }
                }
                catch (Exception)
                {
                    Console.Write(message);
                }
            } while (poziomTrudnosci > 1);
            return poziomTrudnosci;
        }

        // Function which downloads data from the text file.
        void PobierzDane()
        {
            daneTekstowe.pobierzDane();
        }

       // Function which returns a random word from the text file.
        string WylosujSlowo()
        {
            Random rdn = new Random();
            int liczba = rdn.Next(0, 100);

            return daneTekstowe.getSlowo(liczba);
        }


        // function changes positions in the 'B' matrix
        static string[] ZamienMiejscami(string[] ListaDoZamiany, int rozmiarTablicy)
        {
            Random rdm = new Random();
            int Indx1 = rdm.Next(rozmiarTablicy);
            int Indx2 = rdm.Next(rozmiarTablicy);

            string Wartosc1 = ListaDoZamiany[Indx1];
            string Wartosc2 = ListaDoZamiany[Indx2];

            ListaDoZamiany[Indx1] = Wartosc2;
            ListaDoZamiany[Indx2] = Wartosc1;

            return ListaDoZamiany;
        }

        // The function writes actual game situation on the console.
        public void wyswietl(string[] mA_local, string[] mB_local, int rozmiarTablicy)
        {
            string level;

            Console.Clear();

            if (poziomTrudnosci == 0) level = "easy";
            else level = "hard";

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Level: {0}", level);
            Console.WriteLine("Guess chances: {0}", iloscSzans);
            Console.WriteLine();

            Console.Write("     ");

            
            for (int i = 0; i < rozmiarTablicy; i++)
            {
                Console.Write("        {0,-8}", i+1);
            }
            Console.WriteLine();

            Console.Write("A:   ");            

            for (int i = 0; i < rozmiarTablicy; i++)
            {
                int dl = mA_local[i].Length;

                if (dl == 1) Console.Write("|       {0,-8}", mA_local[i]);
                else
                    Console.Write("|   {0,-12}", mA_local[i]);
            }
            Console.Write("|");

            Console.WriteLine();
            Console.Write("B:   ");

            for (int i = 0; i < rozmiarTablicy; i++)
            {
                int dl = mB_local[i].Length;

                if (dl == 1) Console.Write("|       {0,-8}", mB_local[i]);
                else
                    Console.Write("|   {0,-12}", mB_local[i]);
            }
            Console.Write("|");

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------");

        }

        int Rozgrywka(int rozmiarTablicy)
        {
            int wyborA = 10, wyborB = 10;
            string wA, wB;

            wyswietl(mA, mB, rozmiarTablicy);       

            if (iloscSzans == 0)
            {
                return 0;
            }
            
            Console.WriteLine();
            Console.Write("Enter the coordinates of the parameter in the row 'A': (from 1 to {0})  ", rozmiarTablicy);

            do
            {
                wA = Console.ReadLine();

                try
                {
                    wyborA = int.Parse(wA);

                    if (mA_rozwiazania[wyborA - 1] == 1)
                    {
                        wyborA = 10;
                        Console.Write("This word is already uncovered. Enter a different value: ");
                    }
                    else if (wyborA <= 0 || wyborA > rozmiarTablicy)
                    {
                        Console.Write("Value out of range. Enter the correct value: ");
                    }
                }
                catch (Exception)
                {
                    Console.Write("Invalid value. Enter the correct value: ");
                }

            } while (wyborA <= 0 || wyborA > rozmiarTablicy);

            wyborA--; 
            mA[wyborA] = matrixA[wyborA];

            
            wyswietl(mA, mB, rozmiarTablicy);

            // uncovered word in A row

            // ----------------------

            
            Console.WriteLine();
            Console.Write("Enter the coordinates of the parameter in the row 'B': (from 1 to {0})  ", rozmiarTablicy);

            do
            {
                wB = Console.ReadLine();

                try
                {
                    wyborB = int.Parse(wB);

                    if (mB_rozwiazania[wyborB - 1] == 1)
                    {
                        wyborB = 10;
                        Console.Write("This word is already uncovered. Enter a different value: ");
                    }
                    else if (wyborB <= 0 || wyborB > rozmiarTablicy)
                    {
                        Console.Write("Value out of range. Enter the correct value: ");
                    }
                }
                catch (Exception)
                {
                    Console.Write("Invalid value. Enter the correct value: ");
                }

            } while (wyborB <= 0 || wyborB > rozmiarTablicy);

            wyborB--; 
            mB[wyborB] = matrixB[wyborB];

            wyswietl(mA, mB, rozmiarTablicy);

            // uncovered word in row B


            // Checking..

            if (mA[wyborA] == mB[wyborB])
            {
                mA_rozwiazania[wyborA] = 1;
                mB_rozwiazania[wyborB] = 1;

                Console.Write("Great! These words fit together! Press any key to continue.. ");
                Console.ReadKey();

                return 1;
            }
            else
            {
                mA[wyborA] = "X";
                mB[wyborB] = "X";
                --iloscSzans;

                Console.Write("Ups. These words don't match. Press any key to continue.. ");
                Console.ReadKey();
                return 0;
            }
        }



        // level 
        public int LevelStart(int rozmiar)
        {
            int wynik = 0, wynik_temp;
            PobierzDane();

            if (daneTekstowe.getDlugosc() > 0)
            {                 
                for (int i = 0; i < rozmiar; i++)
                {
                    matrixA[i] = WylosujSlowo();
                    matrixB[i] = matrixA[i];
                }

                for (int i = 0; i < 100; i++)
                    matrixB = ZamienMiejscami(matrixB, rozmiar);

                do
                {
                    wynik_temp = Rozgrywka(rozmiar);
                    wynik = wynik + wynik_temp;
                } while (wynik < rozmiar && iloscSzans != 0);

                wyswietl(mA, mB, rozmiar);

                if (wynik == rozmiar)
                {
                    return iloscSzans;
                }
                else if (iloscSzans == 0)
                    return 0;
            }
            return -1;
        }
    }
}
