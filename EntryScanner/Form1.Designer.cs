namespace EntryScanner
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.bntGetImage = new System.Windows.Forms.Button();
            this.pictureBoxOrginal = new System.Windows.Forms.PictureBox();
            this.pictureBoxFound = new System.Windows.Forms.PictureBox();
            this.btnFaceRecognation = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOrginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFound)).BeginInit();
            this.SuspendLayout();
            // 
            // bntGetImage
            // 
            this.bntGetImage.Location = new System.Drawing.Point(12, 12);
            this.bntGetImage.Name = "bntGetImage";
            this.bntGetImage.Size = new System.Drawing.Size(75, 23);
            this.bntGetImage.TabIndex = 0;
            this.bntGetImage.Text = "Start";
            this.bntGetImage.UseVisualStyleBackColor = true;
            this.bntGetImage.Click += new System.EventHandler(this.BntGetImage_Click);
            // 
            // pictureBoxOrginal
            // 
            this.pictureBoxOrginal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxOrginal.Location = new System.Drawing.Point(93, 12);
            this.pictureBoxOrginal.Name = "pictureBoxOrginal";
            this.pictureBoxOrginal.Size = new System.Drawing.Size(532, 391);
            this.pictureBoxOrginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOrginal.TabIndex = 1;
            this.pictureBoxOrginal.TabStop = false;
            // 
            // pictureBoxFound
            // 
            this.pictureBoxFound.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxFound.Location = new System.Drawing.Point(996, 12);
            this.pictureBoxFound.Name = "pictureBoxFound";
            this.pictureBoxFound.Size = new System.Drawing.Size(545, 391);
            this.pictureBoxFound.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxFound.TabIndex = 2;
            this.pictureBoxFound.TabStop = false;
            // 
            // btnFaceRecognation
            // 
            this.btnFaceRecognation.Location = new System.Drawing.Point(12, 41);
            this.btnFaceRecognation.Name = "btnFaceRecognation";
            this.btnFaceRecognation.Size = new System.Drawing.Size(75, 23);
            this.btnFaceRecognation.TabIndex = 3;
            this.btnFaceRecognation.Text = "FaceRecog";
            this.btnFaceRecognation.UseVisualStyleBackColor = true;
            this.btnFaceRecognation.Click += new System.EventHandler(this.btnFaceRecognation_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1553, 415);
            this.Controls.Add(this.btnFaceRecognation);
            this.Controls.Add(this.pictureBoxFound);
            this.Controls.Add(this.pictureBoxOrginal);
            this.Controls.Add(this.bntGetImage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOrginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFound)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bntGetImage;
        private System.Windows.Forms.PictureBox pictureBoxOrginal;
        private System.Windows.Forms.PictureBox pictureBoxFound;
        private System.Windows.Forms.Button btnFaceRecognation;
    }
}

