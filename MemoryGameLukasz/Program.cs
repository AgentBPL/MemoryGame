using System;
using System.Diagnostics;
using System.IO;

namespace MemoryGame
{
    class Program
    {
        static void Main(string[] args)
        {           
            int wybor = 111;
            string wyborString = "";


            DaneGraczy daneGraczy = new DaneGraczy();

            daneGraczy.odczytGraczy();
            daneGraczy.zapiszGraczy();
                        
            Console.WriteLine("Welcome to Memory Game!");
            
            do
            {

                Console.WriteLine("\nMENU: ");
                Console.WriteLine("     [0] Start Game!");
                Console.WriteLine("     [1] Show a scoreboard.");
                Console.WriteLine("     [2] Exit.");


                do
                {
                    Console.Write("What do you want to do? (from 0 to 2)  ");
                    wyborString = Console.ReadLine();

                    try
                    {
                        wybor = int.Parse(wyborString);
                    }
                    catch (Exception)
                    {
                    }

                } while (wybor < 0 || wybor > 2);

                switch (wybor)
                {
                    case 0:     // start game
                        Console.Clear();
                        Rozgrywka(daneGraczy);
                        break;
                    case 1:     // boardscore
                        Console.WriteLine();
                        daneGraczy.wyswietlScoreBoard();
                        break;
                    case 2:     // exit
                        Console.Clear();
                        Console.WriteLine("\nThanks! See you leter!");
                        break;
                }
            } while (wybor != 2);


            Console.ReadKey();
        }


        static void Rozgrywka(DaneGraczy daneGraczy)
        {

            int poziomRozgrywki = 10;
            int wynik = 111;

            
            Stopwatch stopwatch = new Stopwatch();
            Gra gra = new Gra();


            poziomRozgrywki = gra.Start();
            stopwatch.Reset();

            Console.Clear();

            if (poziomRozgrywki == 0)
            {
                stopwatch.Start();
                wynik = gra.LevelStart(4);
            }
            else if (poziomRozgrywki == 1)
            {
                stopwatch.Start();
                wynik = gra.LevelStart(8);
            }


            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;        // pobranie czasu, ktory uplynal

            // Format i sposob wyswietlania czasu 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);


            // koniec rozgrywki

            if (wynik > 0)          // wygrana
            {
                int najgorszyWynik = daneGraczy.najgorszyWynik();
                int iloscProb = 100;

                if (poziomRozgrywki == 0) iloscProb = 10 - wynik + 4;
                else if (poziomRozgrywki == 1) iloscProb = 15 - wynik + 8;

                if ((iloscProb < najgorszyWynik) || najgorszyWynik == 0 || daneGraczy.czyWolneMiejsce())
                {
                    Gracz gracz = new Gracz();
                    gracz.setIloscProb(iloscProb);
                    gracz.setCzas(elapsedTime);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nCONGRATULATIONS!! \nYour score is {0}. You are in TOP10. \nWrite your name: ", iloscProb);
                    Console.ResetColor();
                    string imie = Console.ReadLine();
                    gracz.setName(imie);

                    daneGraczy.dodajGracza(gracz);
                    daneGraczy.sortujGraczy();
                    daneGraczy.zapiszGraczy();
                }
                else
                    Console.WriteLine("\nCongratulations! You won! \nYou needed {0} attempts. Your time: {1}.", iloscProb, elapsedTime);

                Console.WriteLine();
                daneGraczy.wyswietlScoreBoard();
            }

            else if (wynik == 0)
            {
                Console.WriteLine("\nGAME OVER! \nTry one more time! \nGood Luck!");
                Console.WriteLine();
                daneGraczy.wyswietlScoreBoard();
            }

            else if (wynik == -1)
            {
                Console.WriteLine("Text file with 'words' does not exist.");
            }

        }
    }
}
