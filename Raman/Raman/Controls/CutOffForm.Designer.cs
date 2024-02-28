namespace Raman.Controls;

partial class CutOffForm
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
        this.label1 = new System.Windows.Forms.Label();
        this.btnClose = new System.Windows.Forms.Button();
        this.btnUndoCutOff = new System.Windows.Forms.Button();
        this.btnCutOff = new System.Windows.Forms.Button();
        this.btnResetPoints = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.Location = new System.Drawing.Point(7, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(137, 42);
        this.label1.TabIndex = 10;
        this.label1.Text = "Select start and end point for cut off.";
        // 
        // btnClose
        // 
        this.btnClose.Location = new System.Drawing.Point(38, 140);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(75, 23);
        this.btnClose.TabIndex = 11;
        this.btnClose.Text = "Close";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // btnUndoCutOff
        // 
        this.btnUndoCutOff.Location = new System.Drawing.Point(7, 102);
        this.btnUndoCutOff.Name = "btnUndoCutOff";
        this.btnUndoCutOff.Size = new System.Drawing.Size(142, 23);
        this.btnUndoCutOff.TabIndex = 13;
        this.btnUndoCutOff.Text = "Undo Cut Off";
        this.btnUndoCutOff.UseVisualStyleBackColor = true;
        this.btnUndoCutOff.Click += new System.EventHandler(this.btnUndoCutOff_Click);
        // 
        // btnCutOff
        // 
        this.btnCutOff.Location = new System.Drawing.Point(7, 73);
        this.btnCutOff.Name = "btnCutOff";
        this.btnCutOff.Size = new System.Drawing.Size(142, 23);
        this.btnCutOff.TabIndex = 14;
        this.btnCutOff.Text = "Cut Off";
        this.btnCutOff.UseVisualStyleBackColor = true;
        this.btnCutOff.Click += new System.EventHandler(this.btnCutOff_Click);
        // 
        // btnResetPoints
        // 
        this.btnResetPoints.Location = new System.Drawing.Point(7, 45);
        this.btnResetPoints.Name = "btnResetPoints";
        this.btnResetPoints.Size = new System.Drawing.Size(142, 23);
        this.btnResetPoints.TabIndex = 15;
        this.btnResetPoints.Text = "Reset Points";
        this.btnResetPoints.UseVisualStyleBackColor = true;
        this.btnResetPoints.Click += new System.EventHandler(this.btnResetPoints_Click);
        // 
        // CutOffForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(169, 450);
        this.Controls.Add(this.btnResetPoints);
        this.Controls.Add(this.btnCutOff);
        this.Controls.Add(this.btnUndoCutOff);
        this.Controls.Add(this.btnClose);
        this.Controls.Add(this.label1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        this.Name = "CutOffForm";
        this.Text = "Cut Off";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CutOffForm_FormClosing);
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.Button btnResetPoints;

    private System.Windows.Forms.Button btnUndoCutOff;

    private System.Windows.Forms.Button btnCutOff;

    private System.Windows.Forms.Button btnClose;

    private System.Windows.Forms.Label label1;

    #endregion
}