﻿using System.ComponentModel;
using System.Windows.Forms.Integration;
using SorexUI.viewmodel;
using SorexMarkdownLibrary;

namespace SorexUI.view;

partial class MainForm : Form
{
    private MainViewModel vm;

    internal MainForm(MainViewModel vm)
    {
        InitializeComponent();
        InitializeComponents();
        this.vm = vm;
        vm.PropertyChanged += OnCurrentPathChanged;
    }

    private void OnCurrentPathChanged(object? sender, PropertyChangedEventArgs e)
    {
        tagsPanel.Controls.Clear();
        tagsPanel.Controls.AddRange(vm.GetTags().Select(tag =>
        {
            var btn = new Button { Text = tag, Size = new(170, 30), TextAlign = ContentAlignment.MiddleLeft };
            btn.Click += OnTagClick;
            return btn;
        }).ToArray());
    }

    private void OnTagClick(object? sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            var notes = vm.SearchByTag(btn.Text, checkShowArchive.Checked);
            contentPanel.Controls.Clear();
            contentPanel.Controls.AddRange(notes.Select(note =>
                new ElementHost { Child = new SorexMarkdown { Markdown = note.data } }
            ).ToArray());
        }
    }

    private void OnRecentFileClick(object sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem item)
            vm.OpenFile(item.Text ?? "");
    }

    private void OnNewFileClick(object sender, EventArgs e) => vm.NewFile();

    private void OnOpenFileClick(object sender, EventArgs e) => vm.OpenFile();

    private void OnCloseFileClick(object sender, EventArgs e) => vm.CloseFile();

    private void OnQuitSorexClick(object sender, EventArgs e)
    {
        vm.CloseFile();
        Application.Exit();
    }

    private void OnAboutSorexClick(object sender, EventArgs e)
    {
        MessageBox.Show("Sorex App"); // TODO FIXME message
    }
}
