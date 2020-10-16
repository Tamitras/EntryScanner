using DlibDotNet;
using FaceRecognitionDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntryScanner
{
    public partial class Form1 : Form
    {
        private static FaceRecognition faceRecognition;
        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            CamProvider.Initialize();
            CamProvider.NewCapturedImageFromCam += ServiceProvider_NewCurrentImage;

            this.bntGetImage.BackColor = Color.Red;
        }

        private void ServiceProvider_NewCurrentImage(object sender, MyEventArgs e)
        {
            // Zeigt immer das neuste Bild der Webcam an
            ShowWebCamImage(e.imageAsBitmap);
        }

        private void BntGetImage_Click(object sender, EventArgs e)
        {
            this.bntGetImage.BackColor = Color.Green;
            this.bntGetImage.Enabled = false;

            new Task(() =>
            {
                CamProvider.StatusChanged += (s, args) =>
                {
                    if (args.BrokeImageStream)
                    {
                        this.bntGetImage.BackColor = Color.Red;
                    }
                };

                CamProvider.AnalyzeCamStream();

            }).Start();
        }

        private void StartFaceDetection()
        {
            try
            {
                new Task(() =>
                {
                    using (var win = new ImageWindow())
                    {
                        // Load face detection and pose estimation models.
                        using (var detector = Dlib.GetFrontalFaceDetector())
                        using (var poseModel = ShapePredictor.Deserialize(@"D:\Development\Git\EntryScanner\EntryScanner\ShapePredictor\shape_predictor_68_face_landmarks.dat"))
                        {
                            // Grab and process frames until the main window is closed by the user.
                            while (!win.IsClosed())
                            {
                                // Grab a frame
                                var temp = CamProvider.CurrentMat;
                                var temp2 = CamProvider.CurrentImage;

                                // Turn OpenCV's Mat into something dlib can deal with.  Note that this just
                                // wraps the Mat object, it doesn't copy anything.  So cimg is only valid as
                                // long as temp is valid.  Also don't do anything to temp that would cause it
                                // to reallocate the memory which stores the image as that will make cimg
                                // contain dangling pointers.  This basically means you shouldn't modify temp
                                // while using cimg.
                                var array = new byte[temp.Width * temp.Height * temp.ElementSize];

                                    // Detect faces 
                                    var faces = CamProvider.FindFaces(CamProvider.CurrentImage);
                                    // Find the pose of each face.
                                    var shapes = new List<FullObjectDetection>();
                                    for (var i = 0; i < faces.Length; ++i)
                                    {
                                        var det = poseModel.Detect(cimg, faces[i]);
                                        shapes.Add(det);
                                    }

                                    // Display it all on the screen
                                    win.ClearOverlay();
                                    win.SetImage(CamProvider.CurrentImage.);
                                    var lines = Dlib.RenderFaceDetections(shapes);
                                    win.AddOverlay(lines);

                                    foreach (var line in lines)
                                        line.Dispose();
                            }
                        }
                    }
                }).Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void ShowWebCamImage(Bitmap imageAsBitmap)
        {
            this.pictureBoxOrginal.Image = imageAsBitmap;
            this.pictureBoxFound.Image = imageAsBitmap;
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

        private void btnFaceRecognation_Click(object sender, EventArgs e)
        {
            this.StartFaceDetection();
        }
    }
}
