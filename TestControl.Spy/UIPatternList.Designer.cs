namespace TestControl.Spy
{
    partial class UIPatternList
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
            this.listUiPatterns = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listUiPatterns
            // 
            this.listUiPatterns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listUiPatterns.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listUiPatterns.FormattingEnabled = true;
            this.listUiPatterns.ItemHeight = 16;
            this.listUiPatterns.Location = new System.Drawing.Point(12, 12);
            this.listUiPatterns.Name = "listUiPatterns";
            this.listUiPatterns.Size = new System.Drawing.Size(248, 216);
            this.listUiPatterns.TabIndex = 0;
            // 
            // UIPatternList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 240);
            this.Controls.Add(this.listUiPatterns);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "UIPatternList";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UI Patterns";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox listUiPatterns;

    }
}