using System.Collections.Generic;

namespace MyKata.Test
{
    class TreeNode<T>
    {
        readonly TreeNode<T> _parent;
        readonly T _obj;
        readonly IList<TreeNode<T>> children = new List<TreeNode<T>>();
        public int Remainder;
        public TreeNode<T> Parent
        {
            get { return _parent; }
        }
        public T Item {get { return _obj; }}
        public IEnumerable<TreeNode<T>> Children { get { return children; } }

        public TreeNode(TreeNode<T> parent, T obj)
        {
            _parent = parent;
            _obj = obj;
        }

        public TreeNode(TreeNode<T> parent)
        {
            _parent = parent;
            _obj = default(T);
        }

        public void AddChild(TreeNode<T> node)
        {
            children.Add(node);
        }
    }
}