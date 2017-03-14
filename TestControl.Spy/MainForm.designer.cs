namespace TestControl.Spy
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxGetAccObjects = new System.Windows.Forms.CheckBox();
            this.dragHandleImage = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageInspector = new System.Windows.Forms.TabPage();
            this.objectGrid = new System.Windows.Forms.PropertyGrid();
            this.tabPageHistory = new System.Windows.Forms.TabPage();
            this.tabPageAccObjects = new System.Windows.Forms.TabPage();
            this.listBoxAccObjects = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonDblClick = new System.Windows.Forms.Button();
            this.buttonSingleClick = new System.Windows.Forms.Button();
            this.buttonRightCapture = new System.Windows.Forms.Button();
            this.textBoxAccObjPath = new System.Windows.Forms.TextBox();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.architectureLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripCoord = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dragHandleImage)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageInspector.SuspendLayout();
            this.tabPageHistory.SuspendLayout();
            this.tabPageAccObjects.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(332, 426);
            this.richTextBox1.TabIndex = 19;
            this.richTextBox1.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBoxGetAccObjects);
            this.panel1.Controls.Add(this.dragHandleImage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(626, 61);
            this.panel1.TabIndex = 20;
            // 
            // checkBoxGetAccObjects
            // 
            this.checkBoxGetAccObjects.AutoSize = true;
            this.checkBoxGetAccObjects.Location = new System.Drawing.Point(12, 21);
            this.checkBoxGetAccObjects.Name = "checkBoxGetAccObjects";
            this.checkBoxGetAccObjects.Size = new System.Drawing.Size(156, 17);
            this.checkBoxGetAccObjects.TabIndex = 17;
            this.checkBoxGetAccObjects.Text = "Capture Accessible Objects";
            this.checkBoxGetAccObjects.UseVisualStyleBackColor = true;
            // 
            // dragHandleImage
            // 
            this.dragHandleImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dragHandleImage.Image = global::TestControl.Spy.Properties.Resources.bullseye;
            this.dragHandleImage.Location = new System.Drawing.Point(579, 17);
            this.dragHandleImage.Name = "dragHandleImage";
            this.dragHandleImage.Size = new System.Drawing.Size(32, 29);
            this.dragHandleImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dragHandleImage.TabIndex = 11;
            this.dragHandleImage.TabStop = false;
            this.dragHandleImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragHandleMouseDown);
            this.dragHandleImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragHandleImageMouseMove);
            this.dragHandleImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DragHandleImageMouseUp);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.statusStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 61);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(626, 484);
            this.panel2.TabIndex = 21;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageInspector);
            this.tabControl1.Controls.Add(this.tabPageHistory);
            this.tabControl1.Controls.Add(this.tabPageAccObjects);
            this.tabControl1.Controls.Add(this.tabPageAbout);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(5, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(616, 452);
            this.tabControl1.TabIndex = 22;
            // 
            // tabPageInspector
            // 
            this.tabPageInspector.Controls.Add(this.objectGrid);
            this.tabPageInspector.Location = new System.Drawing.Point(4, 22);
            this.tabPageInspector.Name = "tabPageInspector";
            this.tabPageInspector.Padding = new System.Windows.Forms.Padding(6);
            this.tabPageInspector.Size = new System.Drawing.Size(338, 432);
            this.tabPageInspector.TabIndex = 4;
            this.tabPageInspector.Text = "Object Inspector";
            this.tabPageInspector.UseVisualStyleBackColor = true;
            // 
            // objectGrid
            // 
            this.objectGrid.CategoryForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.objectGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectGrid.Location = new System.Drawing.Point(6, 6);
            this.objectGrid.Name = "objectGrid";
            this.objectGrid.Size = new System.Drawing.Size(326, 420);
            this.objectGrid.TabIndex = 0;
            // 
            // tabPageHistory
            // 
            this.tabPageHistory.Controls.Add(this.richTextBox1);
            this.tabPageHistory.Location = new System.Drawing.Point(4, 22);
            this.tabPageHistory.Name = "tabPageHistory";
            this.tabPageHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHistory.Size = new System.Drawing.Size(338, 432);
            this.tabPageHistory.TabIndex = 0;
            this.tabPageHistory.Text = "History";
            this.tabPageHistory.UseVisualStyleBackColor = true;
            // 
            // tabPageAccObjects
            // 
            this.tabPageAccObjects.Controls.Add(this.splitContainer1);
            this.tabPageAccObjects.Controls.Add(this.panel3);
            this.tabPageAccObjects.Location = new System.Drawing.Point(4, 22);
            this.tabPageAccObjects.Name = "tabPageAccObjects";
            this.tabPageAccObjects.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAccObjects.Size = new System.Drawing.Size(608, 426);
            this.tabPageAccObjects.TabIndex = 2;
            this.tabPageAccObjects.Text = "Accessible Objects";
            this.tabPageAccObjects.UseVisualStyleBackColor = true;
            // 
            // listBoxAccObjects
            // 
            this.listBoxAccObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxAccObjects.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxAccObjects.FormattingEnabled = true;
            this.listBoxAccObjects.ItemHeight = 14;
            this.listBoxAccObjects.Location = new System.Drawing.Point(5, 5);
            this.listBoxAccObjects.Name = "listBoxAccObjects";
            this.listBoxAccObjects.Size = new System.Drawing.Size(295, 340);
            this.listBoxAccObjects.TabIndex = 0;
            this.listBoxAccObjects.SelectedIndexChanged += new System.EventHandler(this.listBoxAccObjects_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonDblClick);
            this.panel3.Controls.Add(this.buttonSingleClick);
            this.panel3.Controls.Add(this.buttonRightCapture);
            this.panel3.Controls.Add(this.textBoxAccObjPath);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 353);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(602, 70);
            this.panel3.TabIndex = 2;
            // 
            // buttonDblClick
            // 
            this.buttonDblClick.Location = new System.Drawing.Point(274, 6);
            this.buttonDblClick.Name = "buttonDblClick";
            this.buttonDblClick.Size = new System.Drawing.Size(55, 23);
            this.buttonDblClick.TabIndex = 4;
            this.buttonDblClick.Text = "DblClick";
            this.buttonDblClick.UseVisualStyleBackColor = true;
            this.buttonDblClick.Click += new System.EventHandler(this.buttonDblClick_Click);
            // 
            // buttonSingleClick
            // 
            this.buttonSingleClick.Location = new System.Drawing.Point(234, 5);
            this.buttonSingleClick.Name = "buttonSingleClick";
            this.buttonSingleClick.Size = new System.Drawing.Size(38, 23);
            this.buttonSingleClick.TabIndex = 3;
            this.buttonSingleClick.Text = "Click";
            this.buttonSingleClick.UseVisualStyleBackColor = true;
            this.buttonSingleClick.Click += new System.EventHandler(this.buttonSingleClick_Click);
            // 
            // buttonRightCapture
            // 
            this.buttonRightCapture.Location = new System.Drawing.Point(200, 40);
            this.buttonRightCapture.Name = "buttonRightCapture";
            this.buttonRightCapture.Size = new System.Drawing.Size(129, 23);
            this.buttonRightCapture.TabIndex = 2;
            this.buttonRightCapture.Text = "Right Click && Capture";
            this.buttonRightCapture.UseVisualStyleBackColor = true;
            this.buttonRightCapture.Click += new System.EventHandler(this.buttonRightCaptureClick);
            // 
            // textBoxAccObjPath
            // 
            this.textBoxAccObjPath.Location = new System.Drawing.Point(3, 6);
            this.textBoxAccObjPath.Name = "textBoxAccObjPath";
            this.textBoxAccObjPath.ReadOnly = true;
            this.textBoxAccObjPath.Size = new System.Drawing.Size(230, 20);
            this.textBoxAccObjPath.TabIndex = 1;
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.label8);
            this.tabPageAbout.Controls.Add(this.label7);
            this.tabPageAbout.Controls.Add(this.label5);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAbout.Size = new System.Drawing.Size(338, 432);
            this.tabPageAbout.TabIndex = 3;
            this.tabPageAbout.Text = "About";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(26, 148);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(189, 14);
            this.label8.TabIndex = 5;
            this.label8.Text = "http://www.testcontrol.org";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Coral;
            this.label7.Location = new System.Drawing.Point(26, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 18);
            this.label7.TabIndex = 4;
            this.label7.Text = "TestControl.Net";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(26, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "QAMatic 2010 - 2014";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Ready.";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.architectureLabel,
            this.toolStripStatusLabel1,
            this.toolStripCoord});
            this.statusStrip1.Location = new System.Drawing.Point(5, 457);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(616, 22);
            this.statusStrip1.TabIndex = 20;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // architectureLabel
            // 
            this.architectureLabel.Name = "architectureLabel";
            this.architectureLabel.Size = new System.Drawing.Size(12, 17);
            this.architectureLabel.Text = "_";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabel1.Text = "||";
            // 
            // toolStripCoord
            // 
            this.toolStripCoord.Name = "toolStripCoord";
            this.toolStripCoord.Size = new System.Drawing.Size(45, 17);
            this.toolStripCoord.Text = "Ready..";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBoxAccObjects);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.splitContainer1.Size = new System.Drawing.Size(602, 350);
            this.splitContainer1.SplitterDistance = 305;
            this.splitContainer1.TabIndex = 3;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(5, 5);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(283, 340);
            this.propertyGrid1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 545);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "TestControl.Net - Spy";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dragHandleImage)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageInspector.ResumeLayout(false);
            this.tabPageHistory.ResumeLayout(false);
            this.tabPageAccObjects.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageAbout.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox dragHandleImage;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageHistory;
        private System.Windows.Forms.TabPage tabPageAccObjects;
        private System.Windows.Forms.ListBox listBoxAccObjects;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBoxAccObjPath;
        private System.Windows.Forms.Button buttonRightCapture;
        private System.Windows.Forms.CheckBox checkBoxGetAccObjects;
        private System.Windows.Forms.Button buttonSingleClick;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonDblClick;
        private System.Windows.Forms.TabPage tabPageInspector;
        private System.Windows.Forms.ToolStripStatusLabel toolStripCoord;
        private System.Windows.Forms.ToolStripStatusLabel architectureLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.PropertyGrid objectGrid;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
    }
}

