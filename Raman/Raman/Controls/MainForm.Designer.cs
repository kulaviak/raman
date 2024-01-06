using Raman.Drawing;

namespace Raman
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miZoomWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.miZoomToOriginalSize = new System.Windows.Forms.ToolStripMenuItem();
            this.miRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.miTools = new System.Windows.Forms.ToolStripMenuItem();
            this.miBaselineCorrection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.canvasPanel = new Raman.Drawing.CanvasPanel();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenFiles = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomToWindow = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomToOriginalSize = new System.Windows.Forms.ToolStripButton();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.miTools});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpenFiles,
            this.miExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // miOpenFiles
            // 
            this.miOpenFiles.Name = "miOpenFiles";
            this.miOpenFiles.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
            this.miOpenFiles.Size = new System.Drawing.Size(169, 22);
            this.miOpenFiles.Text = "Open Files";
            this.miOpenFiles.Click += new System.EventHandler(this.miOpenFiles_Click);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(169, 22);
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miZoomWindow,
            this.miZoomToOriginalSize,
            this.miRefresh});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // miZoomWindow
            // 
            this.miZoomWindow.Name = "miZoomWindow";
            this.miZoomWindow.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.miZoomWindow.Size = new System.Drawing.Size(231, 22);
            this.miZoomWindow.Text = "Zoom Window";
            this.miZoomWindow.Click += new System.EventHandler(this.miZoomWindow_Click);
            // 
            // miZoomToOriginalSize
            // 
            this.miZoomToOriginalSize.Name = "miZoomToOriginalSize";
            this.miZoomToOriginalSize.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.miZoomToOriginalSize.Size = new System.Drawing.Size(231, 22);
            this.miZoomToOriginalSize.Text = "Zoom To Original Size";
            this.miZoomToOriginalSize.Click += new System.EventHandler(this.miZoomToOriginalSize_Click);
            // 
            // miRefresh
            // 
            this.miRefresh.Name = "miRefresh";
            this.miRefresh.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.miRefresh.Size = new System.Drawing.Size(231, 22);
            this.miRefresh.Text = "Refresh";
            this.miRefresh.Click += new System.EventHandler(this.miRefresh_Click);
            // 
            // miTools
            // 
            this.miTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miBaselineCorrection});
            this.miTools.Name = "miTools";
            this.miTools.Size = new System.Drawing.Size(46, 20);
            this.miTools.Text = "Tools";
            // 
            // miBaselineCorrection
            // 
            this.miBaselineCorrection.Name = "miBaselineCorrection";
            this.miBaselineCorrection.Size = new System.Drawing.Size(176, 22);
            this.miBaselineCorrection.Text = "Baseline Correction";
            this.miBaselineCorrection.Click += new System.EventHandler(this.miBaselineCorrection_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh,
            this.tsbOpenFiles,
            this.tsbZoomToWindow,
            this.tsbZoomToOriginalSize});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(800, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip2";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.canvasPanel);
            this.splitContainer1.Size = new System.Drawing.Size(800, 401);
            this.splitContainer1.SplitterDistance = 590;
            this.splitContainer1.TabIndex = 2;
            // 
            // canvasPanel
            // 
            this.canvasPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasPanel.IsZooming = false;
            this.canvasPanel.Location = new System.Drawing.Point(0, 0);
            this.canvasPanel.Name = "canvasPanel";
            this.canvasPanel.Size = new System.Drawing.Size(590, 401);
            this.canvasPanel.TabIndex = 0;
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbRefresh.Text = "Refresh";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tsbOpenFiles
            // 
            this.tsbOpenFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpenFiles.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenFiles.Image")));
            this.tsbOpenFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenFiles.Name = "tsbOpenFiles";
            this.tsbOpenFiles.Size = new System.Drawing.Size(23, 22);
            this.tsbOpenFiles.Text = "Open Spectrum Files";
            this.tsbOpenFiles.Click += new System.EventHandler(this.tsbOpenFiles_Click);
            // 
            // tsbZoomToWindow
            // 
            this.tsbZoomToWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomToWindow.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomToWindow.Image")));
            this.tsbZoomToWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomToWindow.Name = "tsbZoomToWindow";
            this.tsbZoomToWindow.Size = new System.Drawing.Size(23, 22);
            this.tsbZoomToWindow.Text = "Zoom To Window";
            this.tsbZoomToWindow.Click += new System.EventHandler(this.tsbZoomToWindow_Click);
            // 
            // tsbZoomToOriginalSize
            // 
            this.tsbZoomToOriginalSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomToOriginalSize.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomToOriginalSize.Image")));
            this.tsbZoomToOriginalSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomToOriginalSize.Name = "tsbZoomToOriginalSize";
            this.tsbZoomToOriginalSize.Size = new System.Drawing.Size(23, 22);
            this.tsbZoomToOriginalSize.Text = "toolStripButton2";
            this.tsbZoomToOriginalSize.Click += new System.EventHandler(this.tsbZoomToOriginalSize_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Raman";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RamanForm_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem miOpenFiles;
    private System.Windows.Forms.ToolStripMenuItem miExit;

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miZoomWindow;
        private System.Windows.Forms.ToolStripMenuItem miZoomToOriginalSize;
        private System.Windows.Forms.ToolStripMenuItem miRefresh;
        private System.Windows.Forms.ToolStripMenuItem miTools;
        private System.Windows.Forms.ToolStripMenuItem miBaselineCorrection;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private CanvasPanel canvasPanel;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripButton tsbOpenFiles;
        private System.Windows.Forms.ToolStripButton tsbZoomToWindow;
        private System.Windows.Forms.ToolStripButton tsbZoomToOriginalSize;
    }
}

