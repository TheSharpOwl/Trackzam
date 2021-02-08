
namespace TrackzamClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ActiveWindowsLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ActiveWindowsLog
            // 
            this.ActiveWindowsLog.Dock = System.Windows.Forms.DockStyle.Top;
            this.ActiveWindowsLog.Location = new System.Drawing.Point(0, 0);
            this.ActiveWindowsLog.Multiline = true;
            this.ActiveWindowsLog.Name = "ActiveWindowsLog";
            this.ActiveWindowsLog.Size = new System.Drawing.Size(800, 200);
            this.ActiveWindowsLog.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ActiveWindowsLog);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ActiveWindowsLog;
    }
}

