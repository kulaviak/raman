namespace Raman
{
  partial class RamanForm
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
      this.menuStrip = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.miOpenFiles = new System.Windows.Forms.ToolStripMenuItem();
      this.miExit = new System.Windows.Forms.ToolStripMenuItem();
      this.menuStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip
      // 
      this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.fileToolStripMenuItem});
      this.menuStrip.Location = new System.Drawing.Point(0, 0);
      this.menuStrip.Name = "menuStrip";
      this.menuStrip.Size = new System.Drawing.Size(800, 24);
      this.menuStrip.TabIndex = 0;
      this.menuStrip.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.miOpenFiles, this.miExit});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // miOpenFiles
      // 
      this.miOpenFiles.Name = "miOpenFiles";
      this.miOpenFiles.Size = new System.Drawing.Size(129, 22);
      this.miOpenFiles.Text = "Open Files";
      // 
      // miExit
      // 
      this.miExit.Name = "miExit";
      this.miExit.Size = new System.Drawing.Size(129, 22);
      this.miExit.Text = "Exit";
      this.miExit.Click += new System.EventHandler(this.miExit_Click);
      // 
      // RamanForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.menuStrip);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MainMenuStrip = this.menuStrip;
      this.Name = "RamanForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Raman";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RamanForm_FormClosing);
      this.menuStrip.ResumeLayout(false);
      this.menuStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem miOpenFiles;
    private System.Windows.Forms.ToolStripMenuItem miExit;

    #endregion
  }
}

