using System.Collections.Generic;
using System.Text;

namespace MyKata.Test
{
    public class GroupingProtocol {
        IList<IEnumerable<string>> _groups; 
        public int Remainder;

        public GroupingProtocol()
        {
            _groups = new List<IEnumerable<string>>();
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

        public void AddGrouping(IEnumerable<string> grouping)
        {
            _groups.Add(grouping);
        }
    }
}