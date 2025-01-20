using System.Windows.Forms.Integration;

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
            markdown = new SorexMarkdownLibrary.SorexMarkdown();
            splitContainer1 = new SplitContainer();
            editBox = new TextBox();
            wpfWidget = new ElementHost();
            mainMenu = new MenuStrip();
            fileMenuItem = new ToolStripMenuItem();
            openRecentMenuItem = new ToolStripMenuItem();
            separator1 = new ToolStripSeparator();
            newFileMenuItem = new ToolStripMenuItem();
            openMenuItem = new ToolStripMenuItem();
            separator2 = new ToolStripSeparator();
            closeFileMenuItem = new ToolStripMenuItem();
            helpMenuItem = new ToolStripMenuItem();
            aboutSorexMenuItem = new ToolStripMenuItem();
            separator3 = new ToolStripSeparator();
            exitMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            mainMenu.SuspendLayout();
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
            wpfWidget.Child = markdown;
            wpfWidget.Dock = DockStyle.Fill;
            wpfWidget.Location = new Point(0, 0);
            wpfWidget.Margin = new Padding(3, 4, 3, 4);
            wpfWidget.Name = "wpfWidget";
            wpfWidget.Size = new Size(530, 534);
            wpfWidget.TabIndex = 0;
            // 
            // mainMenu
            // 
            mainMenu.ImageScalingSize = new Size(20, 20);
            mainMenu.Items.AddRange(new ToolStripItem[] { fileMenuItem, helpMenuItem });
            mainMenu.Location = new Point(0, 0);
            mainMenu.Name = "mainMenu";
            mainMenu.Size = new Size(800, 28);
            mainMenu.TabIndex = 1;
            mainMenu.Text = "mainMenu";
            // 
            // fileMenuItem
            // 
            fileMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openRecentMenuItem, separator1, newFileMenuItem, openMenuItem, separator2, closeFileMenuItem, separator3, exitMenuItem });
            fileMenuItem.Name = "fileMenuItem";
            fileMenuItem.Size = new Size(46, 24);
            fileMenuItem.Text = "File";
            // 
            // openRecentMenuItem
            // 
            openRecentMenuItem.Name = "openRecentMenuItem";
            openRecentMenuItem.Size = new Size(224, 26);
            openRecentMenuItem.Text = "Open Recent";
            // 
            // separator1
            // 
            separator1.Name = "separator1";
            separator1.Size = new Size(221, 6);
            // 
            // newFileMenuItem
            // 
            newFileMenuItem.Name = "newFileMenuItem";
            newFileMenuItem.Size = new Size(224, 26);
            newFileMenuItem.Text = "New File...";
            // 
            // openMenuItem
            // 
            openMenuItem.Name = "openMenuItem";
            openMenuItem.Size = new Size(224, 26);
            openMenuItem.Text = "Open...";
            // 
            // separator2
            // 
            separator2.Name = "separator2";
            separator2.Size = new Size(221, 6);
            // 
            // closeFileMenuItem
            // 
            closeFileMenuItem.Name = "closeFileMenuItem";
            closeFileMenuItem.Size = new Size(224, 26);
            closeFileMenuItem.Text = "Close File";
            // 
            // helpMenuItem
            // 
            helpMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutSorexMenuItem });
            helpMenuItem.Name = "helpMenuItem";
            helpMenuItem.Size = new Size(55, 24);
            helpMenuItem.Text = "Help";
            // 
            // aboutSorexMenuItem
            // 
            aboutSorexMenuItem.Name = "aboutSorexMenuItem";
            aboutSorexMenuItem.Size = new Size(224, 26);
            aboutSorexMenuItem.Text = "About Sorex...";
            // 
            // separator3
            // 
            separator3.Name = "separator3";
            separator3.Size = new Size(221, 6);
            // 
            // exitMenuItem
            // 
            exitMenuItem.Name = "exitMenuItem";
            exitMenuItem.Size = new Size(224, 26);
            exitMenuItem.Text = "Exit";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 562);
            Controls.Add(splitContainer1);
            Controls.Add(mainMenu);
            MainMenuStrip = mainMenu;
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "Sorex";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            mainMenu.ResumeLayout(false);
            mainMenu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox editBox;
        private System.Windows.Forms.Integration.ElementHost wpfWidget;
        private SorexMarkdownLibrary.SorexMarkdown markdown;
        private MenuStrip mainMenu;
        private ToolStripMenuItem fileMenuItem;
        private ToolStripMenuItem openRecentMenuItem;
        private ToolStripSeparator separator1;
        private ToolStripMenuItem newFileMenuItem;
        private ToolStripMenuItem openMenuItem;
        private ToolStripSeparator separator2;
        private ToolStripMenuItem closeFileMenuItem;
        private ToolStripSeparator separator3;
        private ToolStripMenuItem exitMenuItem;
        private ToolStripMenuItem helpMenuItem;
        private ToolStripMenuItem aboutSorexMenuItem;
    }
}

