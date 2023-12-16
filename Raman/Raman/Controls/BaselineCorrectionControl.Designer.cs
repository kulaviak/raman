namespace Raman.Controls
{
    partial class BaselineCorrectionControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnImportPoints = new System.Windows.Forms.Button();
            this.btnExportPoints = new System.Windows.Forms.Button();
            this.btnDoBaselineCorrection = new System.Windows.Forms.Button();
            this.btnUndoBaselineCorrection = new System.Windows.Forms.Button();
            this.lblPoints = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnImportPoints
            // 
            this.btnImportPoints.Location = new System.Drawing.Point(58, 26);
            this.btnImportPoints.Name = "btnImportPoints";
            this.btnImportPoints.Size = new System.Drawing.Size(142, 23);
            this.btnImportPoints.TabIndex = 0;
            this.btnImportPoints.Text = "Import Points";
            this.btnImportPoints.UseVisualStyleBackColor = true;
            // 
            // btnExportPoints
            // 
            this.btnExportPoints.Location = new System.Drawing.Point(58, 55);
            this.btnExportPoints.Name = "btnExportPoints";
            this.btnExportPoints.Size = new System.Drawing.Size(142, 23);
            this.btnExportPoints.TabIndex = 1;
            this.btnExportPoints.Text = "Export Points";
            this.btnExportPoints.UseVisualStyleBackColor = true;
            // 
            // btnDoBaselineCorrection
            // 
            this.btnDoBaselineCorrection.Location = new System.Drawing.Point(58, 246);
            this.btnDoBaselineCorrection.Name = "btnDoBaselineCorrection";
            this.btnDoBaselineCorrection.Size = new System.Drawing.Size(142, 23);
            this.btnDoBaselineCorrection.TabIndex = 2;
            this.btnDoBaselineCorrection.Text = "Do Baseline Correction";
            this.btnDoBaselineCorrection.UseVisualStyleBackColor = true;
            // 
            // btnUndoBaselineCorrection
            // 
            this.btnUndoBaselineCorrection.Location = new System.Drawing.Point(58, 275);
            this.btnUndoBaselineCorrection.Name = "btnUndoBaselineCorrection";
            this.btnUndoBaselineCorrection.Size = new System.Drawing.Size(142, 23);
            this.btnUndoBaselineCorrection.TabIndex = 3;
            this.btnUndoBaselineCorrection.Text = "Undo Baseline Correction";
            this.btnUndoBaselineCorrection.UseVisualStyleBackColor = true;
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.Location = new System.Drawing.Point(60, 87);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(36, 13);
            this.lblPoints.TabIndex = 4;
            this.lblPoints.Text = "Points";
            // 
            // BaselineCorrectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblPoints);
            this.Controls.Add(this.btnUndoBaselineCorrection);
            this.Controls.Add(this.btnDoBaselineCorrection);
            this.Controls.Add(this.btnExportPoints);
            this.Controls.Add(this.btnImportPoints);
            this.Name = "BaselineCorrectionControl";
            this.Size = new System.Drawing.Size(324, 510);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImportPoints;
        private System.Windows.Forms.Button btnExportPoints;
        private System.Windows.Forms.Button btnDoBaselineCorrection;
        private System.Windows.Forms.Button btnUndoBaselineCorrection;
        private System.Windows.Forms.Label lblPoints;
    }
}
