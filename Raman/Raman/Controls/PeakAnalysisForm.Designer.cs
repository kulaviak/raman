namespace Raman.Controls;

partial class PeakAnalysisForm
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
        this.btnReset = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.btnClose = new System.Windows.Forms.Button();
        this.btnExportPeaks = new System.Windows.Forms.Button();
        this.cbAddToAllCharts = new System.Windows.Forms.CheckBox();
        this.SuspendLayout();
        // 
        // btnReset
        // 
        this.btnReset.Location = new System.Drawing.Point(7, 91);
        this.btnReset.Name = "btnReset";
        this.btnReset.Size = new System.Drawing.Size(142, 23);
        this.btnReset.TabIndex = 9;
        this.btnReset.Text = "Reset Points";
        this.btnReset.UseVisualStyleBackColor = true;
        this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
        // 
        // label1
        // 
        this.label1.Location = new System.Drawing.Point(7, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(137, 58);
        this.label1.TabIndex = 10;
        this.label1.Text = "Click on spectrum where peak start and then at point where peak ends.";
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(38, 149);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(75, 23);
        this.btnClose.TabIndex = 11;
        this.btnClose.Text = "Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // btnExportPeaks
        // 
        this.btnExportPeaks.Location = new System.Drawing.Point(7, 120);
        this.btnExportPeaks.Name = "btnExportPeaks";
        this.btnExportPeaks.Size = new System.Drawing.Size(142, 23);
        this.btnExportPeaks.TabIndex = 12;
        this.btnExportPeaks.Text = "Export Peaks";
        this.btnExportPeaks.UseVisualStyleBackColor = true;
        this.btnExportPeaks.Click += new System.EventHandler(this.btnExportPeaks_Click);
        // 
        // cbAddToAllCharts
        // 
        this.cbAddToAllCharts.Location = new System.Drawing.Point(7, 61);
        this.cbAddToAllCharts.Name = "cbAddToAllCharts";
        this.cbAddToAllCharts.Size = new System.Drawing.Size(148, 24);
        this.cbAddToAllCharts.TabIndex = 13;
        this.cbAddToAllCharts.Text = "Add Peak To All Spectra";
        this.cbAddToAllCharts.UseVisualStyleBackColor = true;
        this.cbAddToAllCharts.CheckedChanged += new System.EventHandler(this.cbAddToAllCharts_CheckedChanged);
        // 
        // PeakAnalysisForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(169, 450);
        this.Controls.Add(this.cbAddToAllCharts);
        this.Controls.Add(this.btnExportPeaks);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.btnReset);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        this.Name = "PeakAnalysisForm";
        this.Text = "Peak Analysis";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PeakAnalysisForm_FormClosing);
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.CheckBox cbAddToAllCharts;

    private System.Windows.Forms.Button btnExportPeaks;

    private System.Windows.Forms.Button btnClose;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.Button btnReset;

    #endregion
}