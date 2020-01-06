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
            this.panelFoundFaces = new System.Windows.Forms.FlowLayoutPanel();
            this.panelFoundPersons = new System.Windows.Forms.FlowLayoutPanel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.labelScaleFactor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOrginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
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
            this.pictureBoxOrginal.Location = new System.Drawing.Point(93, 12);
            this.pictureBoxOrginal.Name = "pictureBoxOrginal";
            this.pictureBoxOrginal.Size = new System.Drawing.Size(844, 538);
            this.pictureBoxOrginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOrginal.TabIndex = 1;
            this.pictureBoxOrginal.TabStop = false;
            // 
            // pictureBoxFound
            // 
            this.pictureBoxFound.Location = new System.Drawing.Point(943, 12);
            this.pictureBoxFound.Name = "pictureBoxFound";
            this.pictureBoxFound.Size = new System.Drawing.Size(844, 538);
            this.pictureBoxFound.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxFound.TabIndex = 2;
            this.pictureBoxFound.TabStop = false;
            // 
            // panelFoundFaces
            // 
            this.panelFoundFaces.Location = new System.Drawing.Point(93, 556);
            this.panelFoundFaces.Name = "panelFoundFaces";
            this.panelFoundFaces.Size = new System.Drawing.Size(1694, 500);
            this.panelFoundFaces.TabIndex = 5;
            // 
            // panelFoundPersons
            // 
            this.panelFoundPersons.Location = new System.Drawing.Point(1793, 12);
            this.panelFoundPersons.Name = "panelFoundPersons";
            this.panelFoundPersons.Size = new System.Drawing.Size(166, 580);
            this.panelFoundPersons.TabIndex = 6;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(25, 53);
            this.trackBar1.Maximum = 20;
            this.trackBar1.Minimum = 10;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 385);
            this.trackBar1.TabIndex = 7;
            this.trackBar1.Value = 10;
            this.trackBar1.ValueChanged += new System.EventHandler(this.TrackBar1_ValueChanged);
            // 
            // labelScaleFactor
            // 
            this.labelScaleFactor.AutoSize = true;
            this.labelScaleFactor.Location = new System.Drawing.Point(12, 441);
            this.labelScaleFactor.Name = "labelScaleFactor";
            this.labelScaleFactor.Size = new System.Drawing.Size(86, 13);
            this.labelScaleFactor.TabIndex = 8;
            this.labelScaleFactor.Text = "labelScaleFactor";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1969, 604);
            this.Controls.Add(this.labelScaleFactor);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.panelFoundPersons);
            this.Controls.Add(this.panelFoundFaces);
            this.Controls.Add(this.pictureBoxFound);
            this.Controls.Add(this.pictureBoxOrginal);
            this.Controls.Add(this.bntGetImage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOrginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bntGetImage;
        private System.Windows.Forms.PictureBox pictureBoxOrginal;
        private System.Windows.Forms.PictureBox pictureBoxFound;
        private System.Windows.Forms.FlowLayoutPanel panelFoundFaces;
        private System.Windows.Forms.FlowLayoutPanel panelFoundPersons;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label labelScaleFactor;
    }
}

