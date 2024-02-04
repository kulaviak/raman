using System.ComponentModel;

namespace Raman.Controls;

partial class ErrorForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        this.btnOk = new System.Windows.Forms.Button();
        this.btnDetails = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // btnOk
        // 
        this.btnOk.Location = new System.Drawing.Point(497, 142);
        this.btnOk.Name = "btnOk";
        this.btnOk.Size = new System.Drawing.Size(75, 23);
        this.btnOk.TabIndex = 1;
        this.btnOk.Text = "OK";
        this.btnOk.UseVisualStyleBackColor = true;
        this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
        // 
        // btnDetails
        // 
        this.btnDetails.Location = new System.Drawing.Point(345, 142);
        this.btnDetails.Name = "btnDetails";
        this.btnDetails.Size = new System.Drawing.Size(146, 23);
        this.btnDetails.TabIndex = 2;
        this.btnDetails.Text = "Copy Details To Clipboard";
        this.btnDetails.UseVisualStyleBackColor = true;
        this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
        // 
        // ErrorForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.SystemColors.Control;
        this.ClientSize = new System.Drawing.Size(584, 175);
        this.Controls.Add(this.btnDetails);
        this.Controls.Add(this.btnOk);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.Location = new System.Drawing.Point(15, 15);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "ErrorForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.Button btnDetails;

    private System.Windows.Forms.Button btnOk;

    #endregion
}