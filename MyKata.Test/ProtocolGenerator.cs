//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace MyKata.Test
//{
//    public class ProtocolGenerator 
//    {
//        IDictionary<string, double> calculatedPriceCache = new Dictionary<string, double>();

//        public IEnumerable<GroupingProtocol> GenerateProtocols(IList<string> titles)
//        {
//            var rootNode = new TreeNode<IEnumerable<string>>(null);

//            var distinctTitles = titles.Distinct().Select(title => title);
//            var possibleGroupings = GetPossibleGroupsRecursive(distinctTitles);
//            foreach (var grouping in possibleGroupings)
//            {
//                rootNode.AddChild(new TreeNode<IEnumerable<string>>(rootNode, grouping));
//            }

//            foreach (var childNode in rootNode.Children)
//            {
//                var myTitles = Clone(titles);

//                // apply the grouping
//                var newTitles = GetNewTitles(childNode.Item, myTitles);

//                GetPossibleGroupings(newTitles, childNode);
//            }

//            IList<GroupingProtocol> result = new List<GroupingProtocol>();
//            GetAllPossibleProtocols(rootNode, result);
//            return result;
//        }

//        void GetAllPossibleProtocols(TreeNode<IEnumerable<string>> node, IList<GroupingProtocol> result)
//        {

//            if (node.Children.Count() == 0) 
//                result.Add(GetGroupingProtocol(node));

//            foreach (var childNode in node.Children)
//            {
//                GetAllPossibleProtocols(childNode, result);
//            }
//        }

//        void GetPossibleGroupings(IEnumerable<string> titles, TreeNode<IEnumerable<string>> node)
//        {
//            var stringRepresentationOfTitleAndFrequency = GetStringKey(titles);

//            if (calculatedPriceCache.ContainsKey(stringRepresentationOfTitleAndFrequency)) 
//            {
//                node.RemainderPrice = calculatedPriceCache[stringRepresentationOfTitleAndFrequency];
//            }

//            var distinctTitles = titles.Distinct().Select(title => title);
//            var possibleGroupings = GetPossibleGroupsRecursive(distinctTitles);

//            if (possibleGroupings.Count() == 0)
//            {
//                Console.WriteLine("Found One");
          
//                node.Remainder = titles.Count();
//                return;
//            }

//            foreach (var grouping in possibleGroupings)
//            {
              
//                node.AddChild(new TreeNode<IEnumerable<string>>(node, grouping));
//            }

//            foreach (var childNode in node.Children)
//            {

//                var myTitles = Clone(titles);

//                // apply the grouping
//                var newTitles = GetNewTitles(childNode.Item, myTitles);

//                GetPossibleGroupings(newTitles, childNode);
//            }
//        }

//        string GetStringKey(IEnumerable<string> titles)
//        {
//            var counts = 
//                titles
//                .GroupBy(item => item.ToString())
//                .Select(g => new { Key = g.Key, Frequency = g.Count() })
//                .OrderByDescending(g => g.Frequency);

//            var sb = new StringBuilder();
//            foreach (var count in counts)
//            {
//                sb.Append(count.Frequency);
//            }
//            return sb.ToString();
//        }

//        GroupingProtocol GetGroupingProtocol(TreeNode<IEnumerable<string>> leafNode)
//        {
//            var result = new GroupingProtocol();

//            var tempNode = leafNode;
//            while (tempNode.Parent != null)
//            {
//                result.AddGrouping(tempNode.Item);
//                tempNode = tempNode.Parent;
//            }
//            result.Remainder = leafNode.Remainder;
//            return result;
//        }

//        IEnumerable<string> Clone(IEnumerable<string> titles)
//        {
//            return new List<string>(titles);
//        }


//        IEnumerable<string> GetNewTitles(IEnumerable<string> grouping, IEnumerable<string> titles)
//        {
//            var list = titles.ToList();
//            foreach (var t in grouping)
//            {
//                list.Remove(t);
//            }
//            return list;
//        }


//        IEnumerable<IEnumerable<string>> GetPossibleGroupsRecursive(IEnumerable<string> enumerable)
//        {
//            if (enumerable.Count() < 2) return new List<IEnumerable<string>>();

//            IList<IEnumerable<string>> result = new List<IEnumerable<string>>();

//            PickOutOfTheArray(2, new string[] { }, enumerable.ToArray(), result);
//            PickOutOfTheArray(3, new string[] { }, enumerable.ToArray(), result);
//            PickOutOfTheArray(4, new string[] { }, enumerable.ToArray(), result);
//            PickOutOfTheArray(5, new string[] { }, enumerable.ToArray(), result);

//            return result;

//        }

//        // Define other methods and classes here
//        void PickOutOfTheArray(int size, string[] fixedArray, string[] array, IList<IEnumerable<string>> resultList)
//        {
//            if (size == 1)
//            {
//                foreach (var s in array)
//                {
//                    var result = new List<string>();
//                    foreach (var t in fixedArray)
//                    {
//                        result.Add(t);
//                    }
//                    result.Add(s);
//                    resultList.Add(result);
//                }
//                return;
//            }

//            for (var i = 0; i < (array.Length - size + 1); i++)
//            {
//                var s = array[i];
//                var newFixedArray = new List<string>(fixedArray);
//                newFixedArray.Add(s);
//                var newArray = new List<string>(array);
//                for (var j = 0; j <= i; j++) newArray.RemoveAt(0);
//                PickOutOfTheArray(size - 1, newFixedArray.ToArray(), newArray.ToArray(), resultList);
//            }
//        }

//    }
//}