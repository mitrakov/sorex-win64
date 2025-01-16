namespace Sorex
{
    partial class Sorex
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.editBox = new System.Windows.Forms.TextBox();
            this.wpfWidget = new System.Windows.Forms.Integration.ElementHost();
            this.markdown = new SorexMarkdown.MdControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.editBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.wpfWidget);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 266;
            this.splitContainer1.TabIndex = 0;
            // 
            // editBox
            // 
            this.editBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editBox.Location = new System.Drawing.Point(0, 0);
            this.editBox.Multiline = true;
            this.editBox.Name = "editBox";
            this.editBox.Size = new System.Drawing.Size(266, 450);
            this.editBox.TabIndex = 0;
            this.editBox.TextChanged += new System.EventHandler(this.editBox_TextChanged);
            // 
            // wpfWidget
            // 
            this.wpfWidget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpfWidget.Location = new System.Drawing.Point(0, 0);
            this.wpfWidget.Name = "wpfWidget";
            this.wpfWidget.Size = new System.Drawing.Size(530, 450);
            this.wpfWidget.TabIndex = 0;
            this.wpfWidget.Child = this.markdown;
            // 
            // Sorex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Sorex";
            this.Text = "Sorex";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox editBox;
        private System.Windows.Forms.Integration.ElementHost wpfWidget;
        private SorexMarkdown.MdControl markdown;
    }
}

