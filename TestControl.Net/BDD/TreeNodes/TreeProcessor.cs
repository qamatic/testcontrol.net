// ===================================
// The use and distribution terms for this software are covered by the Microsoft public license, 
// visit for more info : http://testcontrol.codeplex.com
// 
// You must not remove this copyright notice, or any other, from this software
// 
// Senthil Maruthaiappan  senips@gmail.com
// ===================================

using System.Windows.Forms;

namespace TestControl.Net.BDD.TreeNodes
{
    public class TreeProcessor
    {
        public TreeProcessor(TreeNode node, ProcessTreeNode eventHandler)
        {
            if (eventHandler != null)
            {
                ProcessTreeNodeEventHandler += eventHandler;
                Process(node);
            }
        }

        public event ProcessTreeNode ProcessTreeNodeEventHandler;

        public void Process(TreeNode pnode)
        {
            ProcessTreeNodeEventHandler(pnode);
            for (int i = 0; i < pnode.Nodes.Count; ++i)
                Process(pnode.Nodes[i]);
        }
    }

    public delegate void ProcessTreeNode(object sender);
}