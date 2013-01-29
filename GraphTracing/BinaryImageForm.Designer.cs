namespace GraphTracing
{
    partial class BinaryImageForm
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
            this.binaryImagePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.binaryImagePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // binaryImagePictureBox
            // 
            this.binaryImagePictureBox.Location = new System.Drawing.Point(13, 13);
            this.binaryImagePictureBox.Name = "binaryImagePictureBox";
            this.binaryImagePictureBox.Size = new System.Drawing.Size(1220, 504);
            this.binaryImagePictureBox.TabIndex = 0;
            this.binaryImagePictureBox.TabStop = false;
            // 
            // BinaryImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 538);
            this.Controls.Add(this.binaryImagePictureBox);
            this.Name = "BinaryImageForm";
            this.Text = "Binary Image";
            this.Load += new System.EventHandler(this.BinaryImageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.binaryImagePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox binaryImagePictureBox;
    }
}

