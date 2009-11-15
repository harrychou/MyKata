using System.Collections.Generic;

namespace MyKata.Test
{
    public class PotterBookComparer : IEqualityComparer<PotterBook>
    {
        public bool Equals(PotterBook x, PotterBook y)
        {
            return x.Title.Equals(y.Title);
        }

        public int GetHashCode(PotterBook obj)
        {
            return obj.Title.GetHashCode();
        }
    }
}