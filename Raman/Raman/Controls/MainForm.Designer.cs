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
            this._canvasPanel = new Raman.Drawing.CanvasPanel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnOpenFiles = new System.Windows.Forms.ToolStripButton();
            this.btnZoomWindow = new System.Windows.Forms.ToolStripButton();
            this.zoomToOriginalSize = new System.Windows.Forms.ToolStripButton();
            this.miTools = new System.Windows.Forms.ToolStripMenuItem();
            this.miBaselineCorrection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this._canvasPanel.SuspendLayout();
            this.toolStrip2.SuspendLayout();
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
            // _canvasPanel
            // 
            this._canvasPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._canvasPanel.BackColor = System.Drawing.SystemColors.Control;
            this._canvasPanel.Controls.Add(this.toolStrip2);
            this._canvasPanel.IsZooming = false;
            this._canvasPanel.Location = new System.Drawing.Point(0, 27);
            this._canvasPanel.Name = "_canvasPanel";
            this._canvasPanel.Size = new System.Drawing.Size(800, 428);
            this._canvasPanel.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnOpenFiles,
            this.btnZoomWindow,
            this.zoomToOriginalSize});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(800, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 22);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnOpenFiles
            // 
            this.btnOpenFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenFiles.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFiles.Image")));
            this.btnOpenFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenFiles.Name = "btnOpenFiles";
            this.btnOpenFiles.Size = new System.Drawing.Size(23, 22);
            this.btnOpenFiles.Text = "Open Files";
            this.btnOpenFiles.Click += new System.EventHandler(this.btnOpenFiles_Click);
            // 
            // btnZoomWindow
            // 
            this.btnZoomWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomWindow.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomWindow.Image")));
            this.btnZoomWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomWindow.Name = "btnZoomWindow";
            this.btnZoomWindow.Size = new System.Drawing.Size(23, 22);
            this.btnZoomWindow.Text = "Zoom Area";
            this.btnZoomWindow.Click += new System.EventHandler(this.btnZoomWindow_Click);
            // 
            // zoomToOriginalSize
            // 
            this.zoomToOriginalSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomToOriginalSize.Image = ((System.Drawing.Image)(resources.GetObject("zoomToOriginalSize.Image")));
            this.zoomToOriginalSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomToOriginalSize.Name = "zoomToOriginalSize";
            this.zoomToOriginalSize.Size = new System.Drawing.Size(23, 22);
            this.zoomToOriginalSize.Text = "Zoom To Original Size";
            this.zoomToOriginalSize.Click += new System.EventHandler(this.zoomToOriginalSize_Click);
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
            this.miBaselineCorrection.Size = new System.Drawing.Size(180, 22);
            this.miBaselineCorrection.Text = "Baseline Correction";
            this.miBaselineCorrection.Click += new System.EventHandler(this.miBaselineCorrection_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._canvasPanel);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Raman";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RamanForm_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this._canvasPanel.ResumeLayout(false);
            this._canvasPanel.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem miOpenFiles;
    private System.Windows.Forms.ToolStripMenuItem miExit;

        #endregion

        private Raman.Drawing.CanvasPanel _canvasPanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripButton btnOpenFiles;
        private System.Windows.Forms.ToolStripButton btnZoomWindow;
        private System.Windows.Forms.ToolStripButton zoomToOriginalSize;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miZoomWindow;
        private System.Windows.Forms.ToolStripMenuItem miZoomToOriginalSize;
        private System.Windows.Forms.ToolStripMenuItem miRefresh;
        private System.Windows.Forms.ToolStripMenuItem miTools;
        private System.Windows.Forms.ToolStripMenuItem miBaselineCorrection;
    }
}

