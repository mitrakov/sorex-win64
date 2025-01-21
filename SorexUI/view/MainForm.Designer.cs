﻿
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            panelLeft = new Panel();
            tagsPanel = new FlowLayoutPanel();
            panelTop = new Panel();
            checkShowArchive = new CheckBox();
            textGlobalSearch = new TextBox();
            buttonNew = new Button();
            images = new ImageList(components);
            panelRight = new Panel();
            mainMenu = new MenuStrip();
            fileMenuItem = new ToolStripMenuItem();
            openRecentMenuItem = new ToolStripMenuItem();
            separator1 = new ToolStripSeparator();
            newFileMenuItem = new ToolStripMenuItem();
            openMenuItem = new ToolStripMenuItem();
            separator2 = new ToolStripSeparator();
            closeFileMenuItem = new ToolStripMenuItem();
            separator3 = new ToolStripSeparator();
            quitSorexMenuItem = new ToolStripMenuItem();
            helpMenuItem = new ToolStripMenuItem();
            aboutSorexMenuItem = new ToolStripMenuItem();
            panelLeft.SuspendLayout();
            panelTop.SuspendLayout();
            mainMenu.SuspendLayout();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.Controls.Add(tagsPanel);
            panelLeft.Controls.Add(panelTop);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 28);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(200, 693);
            panelLeft.TabIndex = 0;
            // 
            // tagsPanel
            // 
            tagsPanel.AutoScroll = true;
            tagsPanel.Dock = DockStyle.Fill;
            tagsPanel.FlowDirection = FlowDirection.TopDown;
            tagsPanel.Location = new Point(0, 70);
            tagsPanel.Name = "tagsPanel";
            tagsPanel.Size = new Size(200, 623);
            tagsPanel.TabIndex = 4;
            tagsPanel.WrapContents = false;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(checkShowArchive);
            panelTop.Controls.Add(textGlobalSearch);
            panelTop.Controls.Add(buttonNew);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(200, 70);
            panelTop.TabIndex = 0;
            // 
            // checkShowArchive
            // 
            checkShowArchive.AutoSize = true;
            checkShowArchive.Location = new Point(76, 36);
            checkShowArchive.Name = "checkShowArchive";
            checkShowArchive.Size = new Size(118, 24);
            checkShowArchive.TabIndex = 2;
            checkShowArchive.Text = "Show archive";
            checkShowArchive.UseVisualStyleBackColor = true;
            // 
            // textGlobalSearch
            // 
            textGlobalSearch.Location = new Point(66, 3);
            textGlobalSearch.Name = "textGlobalSearch";
            textGlobalSearch.PlaceholderText = "Global search...";
            textGlobalSearch.Size = new Size(131, 27);
            textGlobalSearch.TabIndex = 1;
            textGlobalSearch.WordWrap = false;
            // 
            // buttonNew
            // 
            buttonNew.ImageIndex = 0;
            buttonNew.ImageList = images;
            buttonNew.Location = new Point(3, 3);
            buttonNew.Name = "buttonNew";
            buttonNew.Size = new Size(57, 59);
            buttonNew.TabIndex = 0;
            buttonNew.Text = "New";
            buttonNew.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonNew.UseVisualStyleBackColor = true;
            // 
            // images
            // 
            images.ColorDepth = ColorDepth.Depth32Bit;
            images.ImageStream = (ImageListStreamer)resources.GetObject("images.ImageStream");
            images.TransparentColor = Color.Transparent;
            images.Images.SetKeyName(0, "square-plus.png");
            // 
            // panelRight
            // 
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(200, 28);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(806, 693);
            panelRight.TabIndex = 1;
            // 
            // mainMenu
            // 
            mainMenu.ImageScalingSize = new Size(20, 20);
            mainMenu.Items.AddRange(new ToolStripItem[] { fileMenuItem, helpMenuItem });
            mainMenu.Location = new Point(0, 0);
            mainMenu.Name = "mainMenu";
            mainMenu.Size = new Size(1006, 28);
            mainMenu.TabIndex = 0;
            // 
            // fileMenuItem
            // 
            fileMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openRecentMenuItem, separator1, newFileMenuItem, openMenuItem, separator2, closeFileMenuItem, separator3, quitSorexMenuItem });
            fileMenuItem.Name = "fileMenuItem";
            fileMenuItem.Size = new Size(46, 24);
            fileMenuItem.Text = "File";
            // 
            // openRecentMenuItem
            // 
            openRecentMenuItem.Name = "openRecentMenuItem";
            openRecentMenuItem.Size = new Size(177, 26);
            openRecentMenuItem.Text = "Open Recent";
            // 
            // separator1
            // 
            separator1.Name = "separator1";
            separator1.Size = new Size(174, 6);
            // 
            // newFileMenuItem
            // 
            newFileMenuItem.Name = "newFileMenuItem";
            newFileMenuItem.Size = new Size(177, 26);
            newFileMenuItem.Text = "New File...";
            newFileMenuItem.Click += OnNewFileClick;
            // 
            // openMenuItem
            // 
            openMenuItem.Name = "openMenuItem";
            openMenuItem.Size = new Size(177, 26);
            openMenuItem.Text = "Open...";
            openMenuItem.Click += OnOpenFileClick;
            // 
            // separator2
            // 
            separator2.Name = "separator2";
            separator2.Size = new Size(174, 6);
            // 
            // closeFileMenuItem
            // 
            closeFileMenuItem.Name = "closeFileMenuItem";
            closeFileMenuItem.Size = new Size(177, 26);
            closeFileMenuItem.Text = "Close File";
            closeFileMenuItem.Click += OnCloseFileClick;
            // 
            // separator3
            // 
            separator3.Name = "separator3";
            separator3.Size = new Size(174, 6);
            // 
            // quitSorexMenuItem
            // 
            quitSorexMenuItem.Name = "quitSorexMenuItem";
            quitSorexMenuItem.Size = new Size(177, 26);
            quitSorexMenuItem.Text = "Quit Sorex";
            quitSorexMenuItem.Click += OnQuitSorexClick;
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
            aboutSorexMenuItem.Size = new Size(174, 26);
            aboutSorexMenuItem.Text = "About Sorex";
            aboutSorexMenuItem.Click += OnAboutSorexClick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1006, 721);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            Controls.Add(mainMenu);
            MainMenuStrip = mainMenu;
            Name = "MainForm";
            Text = "Sorex";
            panelLeft.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            mainMenu.ResumeLayout(false);
            mainMenu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        protected void InitializeComponents()
        {
            openRecentMenuItem.DropDownItems.AddRange(user.Default.recentFiles.Cast<string>().Select(file => new ToolStripMenuItem(file, null, OnRecentFileClick)).ToArray());
        }

        private Panel panelLeft;
        private Panel panelRight;
        private FlowLayoutPanel tagsPanel;
        private Panel panelTop;
        private MenuStrip mainMenu;
        private ToolStripMenuItem fileMenuItem;
        private ToolStripMenuItem openRecentMenuItem;
        private ToolStripSeparator separator1;
        private ToolStripMenuItem newFileMenuItem;
        private ToolStripMenuItem openMenuItem;
        private ToolStripSeparator separator2;
        private ToolStripMenuItem closeFileMenuItem;
        private ToolStripSeparator separator3;
        private ToolStripMenuItem quitSorexMenuItem;
        private ToolStripMenuItem helpMenuItem;
        private ToolStripMenuItem aboutSorexMenuItem;
        private Button buttonNew;
        private CheckBox checkShowArchive;
        private TextBox textGlobalSearch;
        private ImageList images;
    }
}