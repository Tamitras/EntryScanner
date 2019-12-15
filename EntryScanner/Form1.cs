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
        private Thread SetImageThread { get; set; }
        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            GetImageThread = new Thread(GetImage);
            ServiceProvider.Initialize();
            ServiceProvider.NewCurrentImage += ServiceProvider_NewCurrentImage;
            ServiceProvider.TemplateFound += ServiceProvider_TemplateFound;
        }

        private void ServiceProvider_TemplateFound(object sender, MyEventArgs e)
        {
            FoundPerson(e.imageAsBitmap);
        }

        private void FoundPerson(Bitmap imageAsBitmap)
        {
            InvokeIfRequired(this, (MethodInvoker)delegate ()
            {
                PictureBox picBox = new PictureBox();
                picBox.Image = (Image)imageAsBitmap;

                this.panelFoundPersons.Controls.Add(picBox);
            });
        }

        private void ServiceProvider_NewCurrentImage(object sender, MyEventArgs e)
        {
            SetImage(e.imageAsBitmap);
        }

        private void BntGetImage_Click(object sender, EventArgs e)
        {
            GetImageThread.Start();
        }

        private void GetImage()
        {
            ServiceProvider.GetInitialImage();
        }

        private void SetImage(Bitmap imageAsBitmap)
        {
            this.pictureBoxOrginal.Image = imageAsBitmap;
            this.pictureBoxFound.Image = imageAsBitmap;

            Bitmap clonedBitmap = (Bitmap)ServiceProvider.CurrentImage.Clone();

            this.pictureBoxFound.Image = ServiceProvider.FindFaces(clonedBitmap, out List<Bitmap> images);

            foreach (var image in images)
            {
                PictureBox picBox = new PictureBox();
                picBox.Image = (Image)image;
                picBox.Size = new Size(100, 100);

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

        private void InvokeIfRequired(Control target, Delegate methodToInvoke)
        {
            /* Mit Hilfe von InvokeRequired wird geprüft ob der Aufruf direkt an die UI gehen kann oder
             * ob ein Invokeing hier von Nöten ist
             */
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
