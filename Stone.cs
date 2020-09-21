using System;

namespace OkeyDemo
{
    public class Stone
    {
        private string Color;
        private string Symbol;
        private int Value;
        private bool IsOkey;

        public Stone(string color, string symbol)
        {
            this.Color = color;
            this.Symbol = symbol;
            this.IsOkey = false;
            if (!symbol.Equals("Fake"))
            {
                 this.Value = Int32.Parse(symbol);
            }
        }   

        public void setValue(int value)
        {
            this.Value = value;
        }

        public int getValue()
        {
            return this.Value;
        }

        public string getColor()
        {
            return this.Color;
        }

        public string getSymbol()
        {
            return this.Symbol;
        }

        public string ToString()
        {
            return this.Color + "-" + this.Symbol;
        } 
        
        public bool isOkey()
        {
            return this.IsOkey;
        }

        public void setOkey(bool isOkey)
        {
            this.IsOkey = isOkey;
        }
    }
}
