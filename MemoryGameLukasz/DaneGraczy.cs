using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MemoryGame
{
    class DaneGraczy
    {
        Gracz[] graczTab = new Gracz[10];


        public DaneGraczy()
        {
            for (int i = 0; i < 10; i++)
            {
                graczTab[i] = new Gracz();
            }
        }

        // That function shows data on the console when the user choices value '1' in the menu.
        public void wyswietlScoreBoard()
        {
            zapiszGraczy();    // jeśli niebyłoby pliku to uzupelni go defaulotwymi danymi
            odczytGraczy();
            Console.Clear();
            wyswietlGraczy();
        }


        // That function saves players to the txt file.
        public void zapiszGraczy()
        {
            string pathGracz = @"scoreboard.txt";       // scieżka pliku do scoreboard
            StreamWriter sw;

            if (!File.Exists(pathGracz))                // jeżeli nie ma takiego pliku to stworz
                sw = File.CreateText(pathGracz);        // jeśli jest to otworz do niego dostep.
            else
                sw = new StreamWriter(pathGracz);

            for (int i = 0; i < 10; i++)
            {
                sw.WriteLine(graczTab[i].getName());
                sw.WriteLine(graczTab[i].getIloscProb());
                sw.WriteLine(graczTab[i].getCzas());
            }
            sw.Close();
        }

        // That function reads players from txt file.
        public void odczytGraczy()
        {
            string pathGracz = @"scoreboard.txt";       // scieżka pliku do scoreboard
            StreamReader sr;

            if (File.Exists(pathGracz))                // jeżeli nie ma takiego pliku to stworz
            {
                sr = File.OpenText(pathGracz);
                string tekst = "";
                int nr = 0;

                while ((tekst = sr.ReadLine()) != null)
                {
                    try
                    {
                        graczTab[nr].setName(tekst);
                        if ((tekst = sr.ReadLine()) != null)
                            graczTab[nr].setIloscProb(int.Parse(tekst));
                        if ((tekst = sr.ReadLine()) != null)
                            graczTab[nr].setCzas(tekst);
                    }
                    catch (Exception)
                    {
                        //Console.WriteLine("\nSorry. The scoreboard is missing. :(\n");
                        sr.ReadLine();
                        continue;
                    }

                    nr++;
                }
                sr.Close();
            }
        }

        // This function sorts players on the boardscore from the worst to the best.
        // Default values are ignored.
        // Program uses that function only when the new player is added to the scoreboard.
        public void sortujGraczy()
        {
            Gracz temp = new Gracz();

            for (int j = 0; j < 10; j++)
            {
                for (int i = 9; i > 0; i--)
                {
                    if ((graczTab[i].getIloscProb() < graczTab[i - 1].getIloscProb() && graczTab[i].getIloscProb() > 0) || graczTab[i].getIloscProb() > 0 && graczTab[i - 1].getIloscProb() == 0) // jeśli mniejszy to zamiana
                    {
                        // przypisanie do temp wartosci z i-1
                        temp.setName(graczTab[i - 1].getName());
                        temp.setCzas(graczTab[i - 1].getCzas());
                        temp.setIloscProb(graczTab[i - 1].getIloscProb());

                        // do wartosci i-1 przypisanie wartosci z i
                        graczTab[i - 1].setName(graczTab[i].getName());
                        graczTab[i - 1].setCzas(graczTab[i].getCzas());
                        graczTab[i - 1].setIloscProb(graczTab[i].getIloscProb());

                        // do wartosci i przypisanie temp
                        graczTab[i].setName(temp.getName());
                        graczTab[i].setCzas(temp.getCzas());
                        graczTab[i].setIloscProb(temp.getIloscProb());
                    }
                }
            }
        }

        // This function shows on console all from scoreboard.
        public void wyswietlGraczy()
        {
            string name1, czas1;
            int proby1;

            Console.Write("        {0,-10}    {1,-6}     {2,-10}", "NAME", "ATTEMPTS", "GAME TIME");
            Console.WriteLine();

            for (int i = 0; i < 10; i++)
            {
                name1 = graczTab[i].getName();
                czas1 = graczTab[i].getCzas();
                proby1 = graczTab[i].getIloscProb();

                Console.WriteLine("{0,4}.   {1,-10} |     {2,-5} |   {3,-14} ", i + 1, name1, proby1, czas1);
            }
        }

        // That function adds a new player to the boardscore.
        // It is possible only when the player has better score than the worst score on the boardscore.
        public void dodajGracza(Gracz nowy)
        {
            graczTab[9].setName(nowy.getName());
            graczTab[9].setCzas(nowy.getCzas());
            graczTab[9].setIloscProb(nowy.getIloscProb());
        }


        // Function returns the worst value of attempt from the scoreboard.
        public int najgorszyWynik()
        {
            int wynik = 0;
            int iloscProb;
            for (int i = 0; i < 10; i++)
            {
                iloscProb = graczTab[i].getIloscProb();
                if (iloscProb > wynik) wynik = iloscProb;
            }
            return wynik;
        }

        // Function returns value true/false. It checks for free spaces on a scoreboard.
        public bool czyWolneMiejsce()
        {
            bool decyzja = false;
            int iloscProb;

            for (int i = 0; i < 10; i++)
            {
                iloscProb = graczTab[i].getIloscProb();
                if (iloscProb == 0) decyzja = true;
            }
            return decyzja;
        }
    }
}
