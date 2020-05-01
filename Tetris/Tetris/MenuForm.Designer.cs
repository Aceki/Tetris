using System.Windows.Forms;

namespace Tetris
{
    partial class MenuForm
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>ывфывasdasd
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MenuForm
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 400);
            this.DoubleBuffered = true;
            this.KeyPress += Control.KeyPressed;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MenuForm";
            this.ResumeLayout(false);

        }

        #endregion
    }
}