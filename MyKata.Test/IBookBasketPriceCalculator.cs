using System.Collections.Generic;

namespace MyKata.Test
{
    public interface IBookBasketPriceCalculator {
        double CalculateBookPrice(IEnumerable<PotterBook> books, IEqualityComparer<PotterBook> comparer);
    }
}