using System.Collections.Generic;

namespace MyKata.KataPotter
{
    public interface IBookBasketPriceCalculator {
        double CalculateBookPrice(IEnumerable<PotterBook> books, IEqualityComparer<PotterBook> comparer);
    }
}