using System;
using System.Collections.Generic;
using System.Linq;

namespace MyKata.Test
{
    public class PotterBookBasket
    {
        readonly IEqualityComparer<PotterBook> _comparer;
        List<PotterBook> _books;

        public PotterBookBasket(IEnumerable<PotterBook> books, IEqualityComparer<PotterBook> comparer)
        {
            _comparer = comparer;
            _books = new List<PotterBook>(books);
        }

        public PotterBookBasket(IEnumerable<PotterBook> books): this(books, new PotterBookComparer())
        {
            _books = new List<PotterBook>(books);
        }

        public double CalculatePrice()
        {
            if (_books.AreAllTheSameTitle(_comparer))
            {
                _books.ForEach(x => x.Discount = 1);
                return GetTotal();
            }
            else if (_books.AreAllDifferentTitle(_comparer))
            {
                _books.ForEach(x => x.Discount = discount(_books.Count));
                return GetTotal();
            }
            else
            {
                return GroupingBooksIntoBags();
            }
        }

        double GroupingBooksIntoBags()
        {
            var minimumTotal = double.MaxValue;

            List<int[]> sizesToTry = new List<int[]>();

            sizesToTry.Add(new int[] { 5, 4, 3, 2 });
            sizesToTry.Add(new int[] { 5, 4, 2, 3 });
            sizesToTry.Add(new int[] { 5, 3, 4, 2 });
            sizesToTry.Add(new int[] { 5, 3, 2, 4 });
            sizesToTry.Add(new int[] { 5, 2, 3, 4 });
            sizesToTry.Add(new int[] { 5, 2, 4, 3 });

            sizesToTry.Add(new int[] { 4, 5, 3, 2 });
            sizesToTry.Add(new int[] { 4, 5, 2, 3 });
            sizesToTry.Add(new int[] { 4, 3, 5, 2 });
            sizesToTry.Add(new int[] { 4, 3, 2, 5 });
            sizesToTry.Add(new int[] { 4, 2, 3, 5 });
            sizesToTry.Add(new int[] { 4, 2, 5, 3 });

            sizesToTry.Add(new int[] { 3, 4, 5, 2 });
            sizesToTry.Add(new int[] { 3, 4, 2, 5 });
            sizesToTry.Add(new int[] { 3, 5, 4, 2 });
            sizesToTry.Add(new int[] { 3, 5, 2, 4 });
            sizesToTry.Add(new int[] { 3, 2, 5, 4 });
            sizesToTry.Add(new int[] { 3, 2, 4, 5 });

            sizesToTry.Add(new int[] { 2, 4, 3, 5 });
            sizesToTry.Add(new int[] { 2, 4, 5, 3 });
            sizesToTry.Add(new int[] { 2, 3, 4, 5 });
            sizesToTry.Add(new int[] { 2, 3, 5, 4 });
            sizesToTry.Add(new int[] { 2, 5, 3, 4 });
            sizesToTry.Add(new int[] { 2, 5, 4, 3 });

            foreach (var sizes in sizesToTry)
            {
                _books.ForEach(book => book.Discount = 1);
                var temp = AggressiveGroupingPreferring(sizes);
                Console.WriteLine("temp : " + temp);
                minimumTotal = GetMinimum(minimumTotal,temp);
                Console.WriteLine("minimumTotal : " + minimumTotal);
            }
            return minimumTotal;
        }

        double GetMinimum(double total, double groupingTotal)
        {
            if (total < groupingTotal) return total;
            return groupingTotal;
        }

        double AggressiveGroupingPreferring(params int[] sizes)
        {
            ApplyPreferredSizeGrouping(sizes);
              
            Console.WriteLine("Applying discount");
            var distinctBooks = _books.Where(book => book.Discount == 1).Distinct(_comparer);
            while (distinctBooks.Count() > 1)
            {
                var count = distinctBooks.Count();
                Console.WriteLine("Applying discount for " + count);
                distinctBooks.ForEach(book => book.Discount = discount(count));
                distinctBooks = _books.Where(book => book.Discount == 1).Distinct(_comparer);
            }

            return GetTotal();
        }

        void ApplyPreferredSizeGrouping(params int[] sizes)
        {
            foreach (var size in sizes)
            {
                Console.WriteLine("Applying size: " + size);
                var distinctBooks = _books.Where(book => book.Discount == 1).Distinct(_comparer);
                while (distinctBooks.Count() >= size)
                {
                    var enumerator = distinctBooks.GetEnumerator();
                    for (var i = 0; i < size; i++)
                    {
                        enumerator.MoveNext();
                        enumerator.Current.Discount = discount(size);
                    }
                    distinctBooks = _books.Where(book => book.Discount == 1).Distinct(_comparer);
                }    
            }
        }

        double GetTotal() {
            double total = 0;
            foreach (var book in _books)
            {
                total += book.GetFinalPrice();
            }
            return total;
        }

        double discount(int count)
        {
            if (count == 1) return 1;
            if (count == 2) return .95;
            if (count == 3) return .9;
            if (count == 4) return .8;
            return .75;
        }
    }
}