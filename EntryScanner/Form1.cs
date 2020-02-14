using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntryScanner
{
    public partial class Form1 : Form
    {
        private Thread GetImageThread { get; set; }
        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            GetImageThread = new Thread(GetImage);
            CamProvider.Initialize();
            CamProvider.NewCapturedImageFromCam += ServiceProvider_NewCurrentImage;
            CamProvider.TemplateFound += CamProvider_TemplateFound;
        }

        private void CamProvider_TemplateFound(object sender, MyEventArgs e)
        {
            RefreshPersons(e.imageAsBitmap);
        }

        private void RefreshPersons(Bitmap imageAsBitmap)
        {
            try
            {
                InvokeIfRequired(this, (MethodInvoker)delegate ()
                {
                    this.panelFoundPersons.Controls.Clear();
                    foreach (var person in CamProvider.Persons)
                    {
                        PictureBox picBox = new PictureBox();

                        // Get first Face
                        var face = person.Faces.FirstOrDefault();
                        if (face != null)
                        {
                            picBox.Image = (Image)face;
                            picBox.Size = new Size(120, 120);
                            picBox.SizeMode = PictureBoxSizeMode.StretchImage;

                            this.panelFoundPersons.Controls.Add(picBox);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ServiceProvider_NewCurrentImage(object sender, MyEventArgs e)
        {
            // Zeigt immer das neuste Bild der Webcam an
            ShowWebCamImage(e.imageAsBitmap);
            ShowFoundFaces();

        }

        private void BntGetImage_Click(object sender, EventArgs e)
        {
            if (GetImageThread.IsAlive)
            {

            }
            else
            {
                GetImageThread.Start();
            }
        }

        private void GetImage()
        {
            CamProvider.AnalyzeCamStream();
        }

        private void ShowWebCamImage(Bitmap imageAsBitmap)
        {
            this.pictureBoxOrginal.Image = imageAsBitmap;
            this.pictureBoxFound.Image = imageAsBitmap;
        }

        private void ShowFoundFaces()
        {
            Bitmap clonedBitmap = (Bitmap)CamProvider.CurrentImage.Clone();

            this.pictureBoxFound.Image = CamProvider.FindFaces(clonedBitmap, out List<Bitmap> images);

            ShowFoundFaces(images);
        }

        private void ShowFoundFaces(List<Bitmap> images)
        {
            foreach (var image in images)
            {
                PictureBox picBox = new PictureBox();
                picBox.Image = (Image)image;
                picBox.Size = new Size(120, 120);
                picBox.SizeMode = PictureBoxSizeMode.StretchImage;

                InvokeIfRequired(this, (MethodInvoker)delegate ()
                {
                    this.panelFoundFaces.Controls.Add(picBox);

                    if (this.panelFoundFaces.Controls.Count > 20)
                    {
                        this.panelFoundFaces.Controls.Clear();
                    }
                });
            }
        }

        /// <summary>
        /// Mit Hilfe von InvokeRequired wird geprüft ob der Aufruf direkt an die UI gehen kann oder
        /// ob ein Invokeing hier von Nöten ist
        /// </summary>
        /// <param name="target"></param>
        /// <param name="methodToInvoke"></param>
        private void InvokeIfRequired(Control target, Delegate methodToInvoke)
        {
            if (target.InvokeRequired)
            {
                // Das Control muss per Invoke geändert werden, weil der aufruf aus einem Backgroundthread kommt
                target.Invoke(methodToInvoke);
            }
            else
            {
                // Die Änderung an der UI kann direkt aufgerufen werden.
                methodToInvoke.DynamicInvoke();
            }
        }
    }
}
