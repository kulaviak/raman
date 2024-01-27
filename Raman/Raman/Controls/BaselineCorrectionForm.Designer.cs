﻿namespace Raman.Controls;

partial class BaselineCorrectionForm
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
        this.btnUndoBaselineCorrection = new System.Windows.Forms.Button();
        this.btnDoBaselineCorrection = new System.Windows.Forms.Button();
        this.btnExportPoints = new System.Windows.Forms.Button();
        this.btnImportPoints = new System.Windows.Forms.Button();
        this.btnReset = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.btnClose = new System.Windows.Forms.Button();
        this.btnExportCorrectedCharts = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // btnUndoBaselineCorrection
        // 
        this.btnUndoBaselineCorrection.Location = new System.Drawing.Point(7, 217);
        this.btnUndoBaselineCorrection.Name = "btnUndoBaselineCorrection";
        this.btnUndoBaselineCorrection.Size = new System.Drawing.Size(142, 23);
        this.btnUndoBaselineCorrection.TabIndex = 8;
        this.btnUndoBaselineCorrection.Text = "Undo Correction";
        this.btnUndoBaselineCorrection.UseVisualStyleBackColor = true;
        this.btnUndoBaselineCorrection.Click += new System.EventHandler(this.btnUndoBaselineCorrection_Click);
        // 
        // btnDoBaselineCorrection
        // 
        this.btnDoBaselineCorrection.Location = new System.Drawing.Point(7, 188);
        this.btnDoBaselineCorrection.Name = "btnDoBaselineCorrection";
        this.btnDoBaselineCorrection.Size = new System.Drawing.Size(142, 23);
        this.btnDoBaselineCorrection.TabIndex = 7;
        this.btnDoBaselineCorrection.Text = "Correct Baseline";
        this.btnDoBaselineCorrection.UseVisualStyleBackColor = true;
        this.btnDoBaselineCorrection.Click += new System.EventHandler(this.btnDoBaselineCorrection_Click);
        // 
        // btnExportPoints
        // 
        this.btnExportPoints.Location = new System.Drawing.Point(7, 109);
        this.btnExportPoints.Name = "btnExportPoints";
        this.btnExportPoints.Size = new System.Drawing.Size(142, 23);
        this.btnExportPoints.TabIndex = 6;
        this.btnExportPoints.Text = "Export Points";
        this.btnExportPoints.UseVisualStyleBackColor = true;
        this.btnExportPoints.Click += new System.EventHandler(this.btnExportPoints_Click);
        // 
        // btnImportPoints
        // 
        this.btnImportPoints.Location = new System.Drawing.Point(7, 80);
        this.btnImportPoints.Name = "btnImportPoints";
        this.btnImportPoints.Size = new System.Drawing.Size(142, 23);
        this.btnImportPoints.TabIndex = 5;
        this.btnImportPoints.Text = "Import Points";
        this.btnImportPoints.UseVisualStyleBackColor = true;
        this.btnImportPoints.Click += new System.EventHandler(this.btnImportPoints_Click);
        // 
        // btnReset
        // 
        this.btnReset.Location = new System.Drawing.Point(7, 138);
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
        this.label1.Text = "Click on spectrum where baseline points should be. Draw at least 4 points to see " + "baseline.";
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(35, 295);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(75, 23);
        this.btnClose.TabIndex = 11;
        this.btnClose.Text = "Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // btnExportCorrectedCharts
        // 
        this.btnExportCorrectedCharts.Location = new System.Drawing.Point(7, 246);
        this.btnExportCorrectedCharts.Name = "btnExportCorrectedCharts";
        this.btnExportCorrectedCharts.Size = new System.Drawing.Size(142, 23);
        this.btnExportCorrectedCharts.TabIndex = 12;
        this.btnExportCorrectedCharts.Text = "Export Corrected Charts";
        this.btnExportCorrectedCharts.UseVisualStyleBackColor = true;
        this.btnExportCorrectedCharts.Click += new System.EventHandler(this.btnExportCorrectedCharts_Click);
        // 
        // BaselineCorrectionForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(169, 450);
        this.Controls.Add(this.btnExportCorrectedCharts);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.btnReset);
        this.Controls.Add(this.btnUndoBaselineCorrection);
        this.Controls.Add(this.btnDoBaselineCorrection);
        this.Controls.Add(this.btnExportPoints);
        this.Controls.Add(this.btnImportPoints);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        this.Name = "BaselineCorrectionForm";
        this.Text = "Baseline Correction";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BaselineCorrectionForm_FormClosing);
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.Button btnExportCorrectedCharts;

    private System.Windows.Forms.Button btnClose;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.Button btnReset;

    #endregion

    private System.Windows.Forms.Button btnUndoBaselineCorrection;
    private System.Windows.Forms.Button btnDoBaselineCorrection;
    private System.Windows.Forms.Button btnExportPoints;
    private System.Windows.Forms.Button btnImportPoints;
}