using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MemoryGame
{
    class DaneTekstowe
    {
        List<String> slowa = new List<string>();        
        int dlugosc;
               
        public DaneTekstowe()
        {
            dlugosc = 0;
        }

        // Data from text file.
        public void pobierzDane()
        {
            try
            {
                string path = @"Words.txt";
                foreach (string line in System.IO.File.ReadLines(path))                
                    slowa.Add(line);
                
                dlugosc = slowa.Count;
            }
            catch (Exception)
            {
                Console.WriteLine("Data could not be retrieved from the text file.");                
            }            
        }

        // That function gets one word from list which includes words from txt file.
        public string getSlowo(int nr)
        {
            if (nr >= 0 && nr < dlugosc)            
                return slowa[nr];
            
            return "Out of Range";
        }

        // getter of the value 'dlugosc'
        public int getDlugosc() { return dlugosc; }
        // -----
    }
}
