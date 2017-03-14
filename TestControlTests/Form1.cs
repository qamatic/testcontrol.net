using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestControlTests;

namespace WinFormAutomationDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = Person.getExampleBinding1();
            dataGridView1.AutoGenerateColumns = true;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            textBox1.Text = "clicked";           
        }

  
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Text = "double clicked node: " + treeView1.SelectedNode.Text;
            textBox2.Text = "Node Name: "+treeView1.SelectedNode.Name;
        }

        private void listBox1_Enter(object sender, EventArgs e)
        {
            var control = (Control)sender;
            textBox1.Text = control.Name+ " got focus";
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                textBox1.Text = "selecteditem is null";
            } else
            textBox1.Text = listBox1.SelectedItem.ToString();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            textBox1.Text = e.ClickedItem.Text;
        }

        private void menuStrip1_Click(object sender, EventArgs e)
        {
            
        }

        private void file1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "file1ToolStripMenuItem";
        }

      
    }
}
