using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                var minimumTotal = double.MaxValue;
                var groupLists = GetAllPossibleGroupings();
                foreach (var groupList in groupLists)
                {
                    var temp = GetPriceForEachGroupList(groupList) * 8;
                    minimumTotal = GetMinimum(minimumTotal, temp);
                }
                return minimumTotal;
            }
        }

        IEnumerable<GroupingProtocol> GetAllPossibleGroupings()
        {
            IList<GroupingProtocol> result = new List<GroupingProtocol>();
            var distinctBooks = _books.Distinct(_comparer).Select(book => book.Title);
            var possibleGroupings = GetPossibleGroups(distinctBooks);
            foreach (var grouping in possibleGroupings)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in grouping)
                {
                    sb.Append(s + "...");
                }
                Console.WriteLine("Apply possible groupings1 " + sb.ToString());
                _books.ForEach(book => book.Discount = 1); 
                
                var newProtocol = new GroupingProtocol(grouping);
                // create new strategy
                 // apply the grouping
                ApplyGroupingToBookList(grouping, _books);
                // do the recursion
                DoRecursionOn(_books.Where(book => book.Discount == 1), result, newProtocol);
            }

            return result;
        }

        void DoRecursionOn(IEnumerable<PotterBook> books, IList<GroupingProtocol> protocols, GroupingProtocol protocol)
        {
            Console.WriteLine("~~~ DoREcursionOn called with books count {0} ", books.Count());
            Console.WriteLine("With protocol {0}", protocol);

            var distinctBooks = books.Distinct(_comparer).Select(book => book.Title);
            var possibleGroupings = GetPossibleGroups(distinctBooks);

            if (possibleGroupings.Count() == 0) 
            {
                protocol.Remainder = books.Where(book => book.Discount == 1).Count();
                Console.WriteLine(protocol);
                protocols.Add(protocol);
                return;
            }
            foreach (var grouping in possibleGroupings)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in grouping)
                {
                    sb.Append(s + "...");
                }
                Console.WriteLine("  temp possible groupings " + sb.ToString());
            }

            foreach (var grouping in possibleGroupings)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in grouping)
                {
                    sb.Append(s + "...");
                }
                Console.WriteLine("Apply possible groupings " + sb.ToString());
                books.ForEach(book => book.Discount = 1);
           
                // create new strategy
                var newProtocol = new GroupingProtocol(protocol, grouping);
                // apply the grouping
                ApplyGroupingToBookList(grouping, books);
                // do the recursion
                DoRecursionOn(books.Where(book => book.Discount == 1), protocols, newProtocol);
            }
        }

        void ApplyGroupingToBookList(IEnumerable<string> grouping, IEnumerable<PotterBook> books)
        {
            var count = grouping.Count();
            Console.WriteLine("ApplyGroupingToBookList called with grouping count {0}, books count{1} ", count, books.Count());
            foreach (var title in grouping) 
            {
                Console.WriteLine(" ** {0} count{1}", title, count);
            }
            foreach (var title in grouping)
            {
                books.First(book => book.Title == title).Discount = discount(count);
            }
        }

        IEnumerable<IEnumerable<string>> GetPossibleGroups(IEnumerable<string> enumerable)
        {
            Console.WriteLine("GetPossibleGroups called with {0}", enumerable.Count());
            if (enumerable.Count() < 2) return new List<IEnumerable<string>>();

            IList<IEnumerable<string>> result = new List<IEnumerable<string>>();

            PickOutOfTheArray(2, new string[] { }, enumerable.ToArray(), result);
            PickOutOfTheArray(3, new string[] { }, enumerable.ToArray(), result);
            PickOutOfTheArray(4, new string[] { }, enumerable.ToArray(), result);
            PickOutOfTheArray(5, new string[] { }, enumerable.ToArray(), result);

            return result;
        }

        // Define other methods and classes here
        void PickOutOfTheArray(int size, string[] fixedArray, string[] array, IList<IEnumerable<string>> resultList)
        {
            if (size == 1)
            {
                foreach (var s in array)
                {
                    var result = new List<string>();
                    foreach (var t in fixedArray)
                    {
                        result.Add(t);
                    }
                    result.Add(s);
                    resultList.Add(result);
                }
                return;
            }

            for (var i = 0; i < (array.Length - size + 1); i++)
            {
                var s = array[i];
                var newFixedArray = new List<string>(fixedArray);
                newFixedArray.Add(s);
                var newArray = new List<string>(array);
                for (var j = 0; j <= i; j++) newArray.RemoveAt(0);
                PickOutOfTheArray(size - 1, newFixedArray.ToArray(), newArray.ToArray(), resultList);
            }
        }

        double GetPriceForEachGroupList(GroupingProtocol protocol)
        {
            double result = 0;
            StringBuilder sb = new StringBuilder();
            foreach (var group in protocol.Groups)
            {
                var count = group.Count();
                result += discount(count) * count;
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

    public class GroupingProtocol {
        IList<IEnumerable<string>> _groups; 
        public int Remainder;

        public GroupingProtocol(IEnumerable<string> startingWith)
        {
            _groups = new List<IEnumerable<string>>();
            _groups.Add(startingWith);
        }

        public GroupingProtocol(GroupingProtocol protocol, IEnumerable<string> another)
        {
            _groups = new List<IEnumerable<string>>(protocol.Groups);
            _groups.Add(another);
        }

        public IEnumerable<IEnumerable<string>> Groups
        {
            get { return _groups; }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var list in _groups)
            {
                sb.Append("{");
                foreach (var s in list)
                {
                    sb.AppendFormat("'{0}'", s);
                }
                sb.Append("} ");
            }
            sb.Append("Remainer: " + Remainder);
            return sb.ToString();
        }
    }

}