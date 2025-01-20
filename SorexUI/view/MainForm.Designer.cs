﻿using System.Windows.Forms.Integration;

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
            splitContainer1 = new SplitContainer();
            editBox = new TextBox();
            wpfWidget = new ElementHost();
            markdown = new SorexMarkdownLibrary.SorexMarkdown();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 28);
            splitContainer1.Margin = new Padding(3, 4, 3, 4);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(editBox);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(wpfWidget);
            splitContainer1.Size = new Size(800, 534);
            splitContainer1.SplitterDistance = 266;
            splitContainer1.TabIndex = 0;
            // 
            // editBox
            // 
            editBox.Dock = DockStyle.Fill;
            editBox.Location = new Point(0, 0);
            editBox.Margin = new Padding(3, 4, 3, 4);
            editBox.Multiline = true;
            editBox.Name = "editBox";
            editBox.Size = new Size(266, 534);
            editBox.TabIndex = 0;
            editBox.TextChanged += editBox_TextChanged;
            // 
            // wpfWidget
            // 
            wpfWidget.Dock = DockStyle.Fill;
            wpfWidget.Location = new Point(0, 0);
            wpfWidget.Margin = new Padding(3, 4, 3, 4);
            wpfWidget.Name = "wpfWidget";
            wpfWidget.Size = new Size(530, 534);
            wpfWidget.TabIndex = 0;
            wpfWidget.Child = markdown;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 562);
            Controls.Add(splitContainer1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "Sorex";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox editBox;
        private System.Windows.Forms.Integration.ElementHost wpfWidget;
        private SorexMarkdownLibrary.SorexMarkdown markdown;
    }
}

