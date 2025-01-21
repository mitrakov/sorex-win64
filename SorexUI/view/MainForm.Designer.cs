﻿using System.Windows.Forms.Integration;
using SorexMarkdownLibrary;

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
            mainMenu = new MenuStrip();
            fileMenuItem = new ToolStripMenuItem();
            openRecentMenuItem = new ToolStripMenuItem();
            separator1 = new ToolStripSeparator();
            newFileMenuItem = new ToolStripMenuItem();
            openMenuItem = new ToolStripMenuItem();
            separator2 = new ToolStripSeparator();
            closeFileMenuItem = new ToolStripMenuItem();
            separator3 = new ToolStripSeparator();
            exitMenuItem = new ToolStripMenuItem();
            helpMenuItem = new ToolStripMenuItem();
            aboutSorexMenuItem = new ToolStripMenuItem();
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
            editBox.TextChanged += EditBox_TextChanged;
            // 
            // wpfWidget
            // 
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
            newFileMenuItem.Click += OnNewFileClick;
            // 
            // openMenuItem
            // 
            openMenuItem.Name = "openMenuItem";
            openMenuItem.Size = new Size(224, 26);
            openMenuItem.Text = "Open...";
            openMenuItem.Click += OnOpenFileClick;
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
            closeFileMenuItem.Click += OnCloseFile;
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
            exitMenuItem.Click += OnExitClick;
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
            aboutSorexMenuItem.Click += OnAboutSorexClick;
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

        protected void InitializeComponents()
        {
            // WPF widget should be initialized separately because of the bug in Visual Studio (https://github.com/dotnet/winforms/issues/9443)
            markdown = new();
            wpfWidget.Child = markdown;
            openRecentMenuItem.DropDownItems.AddRange(user.Default.recentFiles.Cast<string>().Select(file => new ToolStripMenuItem(file, null, OnRecentFileClick)).ToArray());
        }

        private SplitContainer splitContainer1;
        private TextBox editBox;
        private ElementHost wpfWidget;
        private SorexMarkdown markdown;
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

