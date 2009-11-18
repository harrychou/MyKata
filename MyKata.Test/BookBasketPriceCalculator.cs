using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            var minimumTotal = double.MaxValue;
            var protocols = GetAllPossibleGroupingProtocols(titles,  new ProtocolGenerator());
            foreach (var protocol in protocols)
            {
                var temp = GetPriceForProtocol(protocol) * 8;
                minimumTotal = GetMinimum(minimumTotal, temp);
            }
            return minimumTotal;
        }

        IEnumerable<GroupingProtocol> GetAllPossibleGroupingProtocols(IList<string> titles, ProtocolGenerator generator)
        {
           return generator.GenerateProtocols(titles);
        }


   
        double GetPriceForProtocol(GroupingProtocol protocol)
        {
            double result = 0;
            var sb = new StringBuilder();
            foreach (var group in protocol.Groups)
            {
                var count = group.Count();
                result += GetVolumnDiscountFor(count) * count;
                sb.Append("Add group of " + count + "...");
            }
            sb.Append("Remainder " + protocol.Remainder + "...");
            Console.WriteLine(sb.ToString());
            return result + protocol.Remainder;
        }


        double GetMinimum(double total, double groupingTotal)
        {
            if (total < groupingTotal) return total;
            return groupingTotal;
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

        double GetVolumnDiscountFor(int count)
        {
            if (count == 1) return 1;
            if (count == 2) return .95;
            if (count == 3) return .9;
            if (count == 4) return .8;
            return .75;
        }

    }
}