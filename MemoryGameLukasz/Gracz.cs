using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryGame
{
    class Gracz
    {
        string name;
        string czas;
        int ilosc_prob;

        public Gracz()
        {
            name = "unknown";
            czas = "00:00:00.00";
            ilosc_prob = 0;            
        }


        //  getters and setters of class 
        public void setName(string n) { this.name = n; }
        public string getName() { return this.name; }

        public void setCzas(string cz) { this.czas = cz; }
        public string getCzas() { return this.czas; }

        public void setIloscProb(int p ) { this.ilosc_prob = p;}
        public int getIloscProb() { return this.ilosc_prob; }
        // -------



    }
}
