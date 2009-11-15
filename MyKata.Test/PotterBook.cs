 
using System;


namespace MyKata.Test
{
    public class PotterBook: IEquatable<PotterBook>
    {
        string title;
        public double Price = 8;
        public double Discount { get; set; }

        PotterBook(int i)
        {
            title = "Potter Book " + i;
            Discount = 1;
        }

        public string Title
        {
            get { return title; }
        }

        static public PotterBook CreateBook(int i)
        {
            return new PotterBook(i);
        }

        public bool Equals(PotterBook other)
        {
            return this.Title.Equals(other.Title);
        }

        public double GetFinalPrice()
        {
            return Discount* Price;
        }
    }

}
