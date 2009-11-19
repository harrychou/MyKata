using System.Collections.Generic;
using System.Linq;

namespace MyKata.Test
{
    public class BookBasketPriceCalculator : IBookBasketPriceCalculator {

        public double CalculateBookPrice(IEnumerable<PotterBook> books, IEqualityComparer<PotterBook> comparer)
        {
            if (books.AreAllTheSameTitle(comparer))
            {
                books.ForEach(x => x.Discount = 1);
                return GetTotal(books);
            }

            if (books.AreAllDifferentTitle(comparer))
            {
                books.ForEach(x => x.Discount = GetVolumnDiscountFor(books.Count()));
                return GetTotal(books);
            }

            IList<string> titles = new List<string>(books.Select(book => book.Title));

            var rootNode = new TitleCalculatorTreeNode(null,
                new MyClass {grouping = new List<string>(), items_to_calculate = titles},
                GetVolumnDiscountFor);

            IDictionary<string, double> priceLookUp = new Dictionary<string, double>();
            return rootNode.CalculatePrice(priceLookUp);
        }


        double GetTotal(IEnumerable<PotterBook> books)
        {
            double total = 0;
            foreach (var book in books)
            {
                total += book.GetFinalPrice();
            }
            return total;
        }

        static double GetVolumnDiscountFor(int count)
        {
            if (count == 1) return 1;
            if (count == 2) return .95;
            if (count == 3) return .9;
            if (count == 4) return .8;
            return .75;
        }

    }
}