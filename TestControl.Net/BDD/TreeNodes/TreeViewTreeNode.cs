// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System.Windows.Forms;
using TestControl.Net.BDD.Interfaces;
using TestControl.Net.BDD.Ioc;

namespace TestControl.Net.BDD.TreeNodes
{
    [InstanceBehaviour(typeof (IPersistanceTreeNode), InstanceBehaviourType.AlwaysCreate)]
    public class TreeViewTreeNode : TreeNode, IPersistanceTreeNode
    {
        public TreeViewTreeNode()
        {
        }

        public TreeViewTreeNode(string text) : base(text)
        {
        }


        public IServices Services { get; set; }

        #region IPersistanceTreeNode Members

        public void Clear()
        {
            base.Nodes.Clear();
        }

        public ITestNode TestItem { get; set; }

        public IPersistanceTreeNode ParentNode
        {
            get { return Parent as IPersistanceTreeNode; }
        }

        public IPersistanceTreeNode PreviousNode
        {
            get { return base.PrevNode as IPersistanceTreeNode; }
        }

        public new IPersistanceTreeNode NextNode
        {
            get { return base.NextNode as IPersistanceTreeNode; }
        }

        public int Count
        {
            get { return Nodes.Count; }
        }

        public void AddChildNode(IPersistanceTreeNode node)
        {
            Nodes.Add(node as TreeViewTreeNode);
        }

        public IPersistanceTreeNode getChild(int index)
        {
            return base.Nodes[index] as TreeViewTreeNode;
        }

        #endregion
    }
}