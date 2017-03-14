// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com 
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System.Collections;
using System.Collections.Generic;
using TestControl.Net.BDD.Interfaces;

namespace  TestControl.Net.BDD.Scanners
{
    public class TreeChildEnumerator : ITreeChildEnumerator
    {
        private readonly ITestNode _node;
        private IList<ITestNode> _allNodes;
        private int _currentIdx;

        public TreeChildEnumerator(ITestNode node)
        {
            _node = node;
        }

        #region ITreeChildEnumerator Members

        public IEnumerator GetEnumerator()
        {
            _allNodes = new List<ITestNode>();
            GetAllChildren(_node, ref _allNodes);

            return this;
        }

        public object Current
        {
            get
            {
                ITestNode obj = _allNodes[_currentIdx];
                _currentIdx++;
                return obj;
            }
        }


        public bool MoveNext()
        {
            if (_currentIdx < _allNodes.Count)
            {
                return true;
            }
            Reset();
            return false;
        }

        public void Reset()
        {
            _currentIdx = 0;
        }

        public void ApplyLevelNumbers()
        {
            if (_node.ParentNode == null)
                _node.LevelNo = 0;
            ApplyNodeLevel(_node);
        }

        public IList<ITestNode> ToList()
        {
            GetEnumerator();
            return _allNodes;
        }

        public Hashtable ToHashtable()
        {
            IList<ITestNode> list = ToList();
            var hashTable = new Hashtable();
            foreach (ITestNode node in list)
                hashTable.Add(node.ID, node);
            return hashTable;
        }

        public Hashtable ToHashtableByPath()
        {
            IList<ITestNode> list = ToList();
            var hashTable = new Hashtable();
            foreach (ITestNode node in list)
                hashTable.Add(GetFullPath(node).ToUpper(), node);
            return hashTable;
        }

        #endregion

        private static void ApplyNodeLevel(ITestNode node)
        {
            if (node == null)
                return;
            foreach (ITestNode selectedNode in node)
            {
                selectedNode.LevelNo = selectedNode.ParentNode.LevelNo + 1;
                ApplyNodeLevel(selectedNode);
            }
        }

        public static void GetAllChildren(ITestNode node, ref IList<ITestNode> list)
        {
            if (node == null)
                return;
            foreach (ITestNode selectedNode in node)
            {
                list.Add(selectedNode);
                GetAllChildren(selectedNode, ref list);
            }
        }

        public static void FindParentPath(ITestNode node, ref string parentPath)
        {
            if ((node.ParentNode == null))
                return;
            parentPath = parentPath.Insert(0, node.ParentNode.Name + "\\");
            FindParentPath(node.ParentNode, ref parentPath);
        }

        public static string GetFullPath(ITestNode node)
        {
            string parentPath = string.Empty;
            FindParentPath(node, ref parentPath);
            parentPath += node.Name;
            return parentPath;
        }
    }
}