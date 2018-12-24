using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplateMod.Utils
{
	public class RBTree<T> where T : IComparable<T>
	{
		class TreeNode
		{
			public T Value { get; set; }
			public bool IsRed { get; set; }
			public TreeNode Left { get; set; }
			public TreeNode Right { get; set; }
			public TreeNode Parent { get; set; }
			public TreeNode(T value)
			{
				Value = value;
				IsRed = false;
				Left = null;
				Right = null;
				Parent = null;
			}
		}
		private TreeNode _root;
		public RBTree()
		{
			_root = null;
		}

		public void Insert(T value)
		{
			if (_root == null)
			{
				TreeNode node = new TreeNode(value);
				_root = node;
				return;
			}
			_root = _put(_root, value);
		}

		public bool Contains(T value)
		{
			TreeNode node = _root;
			while(node != null)
			{
				if (value.CompareTo(node.Value) > 0)
					node = node.Right;
				else if (value.CompareTo(node.Value) < 0)
					node = node.Left;
				else
					return true;
			}
			return false;
		}

		public void Delete(T value)
		{

		}

		private void deleteNode(T value)
		{
			
		}

		private bool isRed(TreeNode node)
		{
			return node != null && node.IsRed;
		}

		private TreeNode rotateLeft(TreeNode node)
		{
			TreeNode x = node.Right;
			node.Parent = x;
			node.Right = x.Left;
			x.Left.Parent = node;
			x.Left = node;
			x.IsRed = node.IsRed;
			node.IsRed = true;
			return x;
		}

		private TreeNode rotateRight(TreeNode node)
		{
			TreeNode x = node.Left;
			node.Parent = x;
			node.Left = x.Right;
			x.Right.Parent = node;
			x.Right = node;
			x.IsRed = node.IsRed;
			node.IsRed = true;
			return x;
		}

		private void flipColor(TreeNode node)
		{
			node.IsRed = true;
			node.Left.IsRed = false;
			node.Right.IsRed = false;
		}

		private TreeNode _put(TreeNode node, T value)
		{
			TreeNode newnode = new TreeNode(value);
			newnode.IsRed = true;
			if (node == null)
				return newnode;

			if (value.CompareTo(node.Value) < 0)
				node.Left = _put(node.Left, value);
			else if (value.CompareTo(node.Value) > 0)
				node.Right = _put(node.Right, value);
			else
				node.Value = value;

			if (isRed(node.Right) && !isRed(node.Left))
				node = rotateLeft(node);
			if (isRed(node.Left) && isRed(node.Left.Left))
				node = rotateRight(node);
			if (isRed(node.Left) && isRed(node.Right))
				flipColor(node);

			return node;
		}
	}
}
