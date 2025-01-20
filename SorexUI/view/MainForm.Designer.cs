namespace SorexUI.view
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
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            editBox = new System.Windows.Forms.TextBox();
            wpfWidget = new System.Windows.Forms.Integration.ElementHost();
            markdown = new SorexMarkdownLibrary.SorexMarkdown();
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(editBox);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(wpfWidget);
            splitContainer1.Size = new System.Drawing.Size(800, 450);
            splitContainer1.SplitterDistance = 266;
            splitContainer1.TabIndex = 0;
            // 
            // editBox
            // 
            editBox.Dock = System.Windows.Forms.DockStyle.Fill;
            editBox.Location = new System.Drawing.Point(0, 0);
            editBox.Multiline = true;
            editBox.Name = "editBox";
            editBox.Size = new System.Drawing.Size(266, 450);
            editBox.TabIndex = 0;
            editBox.TextChanged += new System.EventHandler(editBox_TextChanged);
            // 
            // wpfWidget
            // 
            wpfWidget.Dock = System.Windows.Forms.DockStyle.Fill;
            wpfWidget.Location = new System.Drawing.Point(0, 0);
            wpfWidget.Name = "wpfWidget";
            wpfWidget.Size = new System.Drawing.Size(530, 450);
            wpfWidget.TabIndex = 0;
            wpfWidget.Child = markdown;
            // 
            // Sorex
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "Sorex";
            Text = "Sorex";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox editBox;
        private System.Windows.Forms.Integration.ElementHost wpfWidget;
        private SorexMarkdownLibrary.SorexMarkdown markdown;
    }
}

