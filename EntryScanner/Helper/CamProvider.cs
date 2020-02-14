using Emgu.CV;
using Emgu.CV.Structure;
using EntryScanner.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EntryScanner
{
    public static class CamProvider
    {
        private static readonly CascadeClassifier cascadeClassifier = new CascadeClassifier(@"Cascades\haarcascade_frontalface_alt_tree.xml");

        private const string CamRTSPUrl = "rtsp://admin:123456@192.168.0.26:554/live/ch0";

        public static Bitmap CurrentImage { get; set; }
        public static ImageHelper ImageHelper { get; set; }

        public static event EventHandler<MyEventArgs> NewCapturedImageFromCam;
        public static event EventHandler<MyEventArgs> TemplateFound;

        public static List<Person> Persons = new List<Person>();

        public static void Initialize()
        {
            Persons = new List<Person>();
            ImageHelper = new ImageHelper();
            ImageHelper.TemplateFound += ImageHelper_TemplateFound;
        }

        private static void ImageHelper_TemplateFound(object sender, MyEventArgs e)
        {
            TemplateFound(sender, new MyEventArgs() { imageAsBitmap = e.imageAsBitmap });
        }

        internal static Image FindFaces(Bitmap imageAsBitmap, out List<Bitmap> images)
        {
            Bitmap bitmap = imageAsBitmap;
            Bitmap bitmapCloned = (Bitmap)bitmap.Clone();
            Image<Bgr, byte> grayImage = new Image<Bgr, byte>(bitmap);

            // 1.4 is the best ScaleFactor
            Rectangle[] rectangles = cascadeClassifier.DetectMultiScale(grayImage, 1.4, 0);

            //// Durchlaufe alle gefundenen Gesichter und Umrahme sie mit einem Roten Rechteck
            //foreach (var rectangle in rectangles)
            //{
            //    using (Graphics graphics = Graphics.FromImage(bitmap))
            //    {
            //        using (Pen pen = new Pen(Color.Red, 2))
            //        {
            //            graphics.DrawRectangle(pen, rectangle);
            //        }
            //    }
            //}

            // Crop Image from Rectangle and search Database for equal
            // CroptImage(Rectangles) --> Inform MainThrad and Form that someone is found

            images = ImageHelper.StartAnalyseScreenshotAsync(bitmapCloned, rectangles);
            CheckEqualFaces(images);

            return bitmap;
        }

        private static bool CheckEqualFaces(List<Bitmap> foundFaces)
        {
            bool ret = false;
            bool newPersonFound = true;

            foreach (var foundFace in foundFaces)
            {
                // wenn Liste leer, dann füge das erste Gesicht hinzu
                if (Persons.Count == 0)
                {
                    Person firstPerson = new Person();
                    firstPerson.Faces.Add(foundFace);
                    firstPerson.LastSeen = DateTime.Now;
                    Persons.Add(firstPerson);
                }

                foreach (var person in Persons)
                {
                    newPersonFound = true;

                    var imageaccordance = GetImageAccordance(foundFace, person.Faces);

                    if (imageaccordance)
                    {
                        newPersonFound = false;
                        if (person.Faces.Count <= 5)
                        {
                            person.Faces.Add(foundFace);
                            TemplateFound(null, new MyEventArgs() { imageAsBitmap = foundFace });
                            break; // Breche hier ab, da Bild ähnlich, es wird mit dem nächsten Bild weitergemacht
                        }
                    }

                    

                }

                if(newPersonFound)
                {
                    // Eine neue unbekannte Person wurde gefunden
                    Person newPerson = new Person();
                    newPerson.Faces.Add(foundFace);
                    newPerson.LastSeen = DateTime.Now;
                    Persons.Add(newPerson);

                    TemplateFound(null, new MyEventArgs() { imageAsBitmap = foundFace });
                    ret = true;
                    newPersonFound = false;
                    return ret;
                }

            }
            return ret;
        }

        public static bool GetImageAccordance(Bitmap foundFace, List<Bitmap> faces)
        {
            Image<Bgr, byte> source = null;
            Image<Bgr, byte> template = null;

            double average = 0.0;

            foreach (var face in faces)
            {
                if (face.Height > foundFace.Height)
                {
                    source = new Image<Bgr, byte>(foundFace);
                    template = new Image<Bgr, byte>(face);
                }
                else
                {
                    source = new Image<Bgr, byte>(face);
                    template = new Image<Bgr, byte>(foundFace);
                }


                // Wenn die Bilder zu min. 88% übereinstimmen
                average  += ImageHelper.GetAccordanceCoeff(source, template);
            }

            average = average / faces.Count;

            // Übereinstimmung gefunden, bei allen faces
            if(average > 85)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        async internal static void AnalyzeCamStream()
        {
            VideoCapture myCapture = new VideoCapture(CamRTSPUrl);
            myCapture.QueryFrame();
            Mat m = new Mat();

            while (true)
            {
                m = myCapture.QueryFrame();
                if (!m.IsEmpty)
                {
                    CurrentImage = m.Bitmap;
                    NewCapturedImageFromCam(null, new MyEventArgs() { imageAsBitmap = m.Bitmap });

                    double fps = myCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
                    await Task.Delay(1000 / Convert.ToInt32(fps));
                }
            }


            ////Load From URL via RTSP
            //while (true)
            //{
            //    try
            //    {
            //        Mat m = new Mat();
            //        myCapture.Read(m);

            //        if (!m.IsEmpty)
            //        {
            //            CurrentImage = m.Bitmap;
            //            NewCapturedImageFromCam(null, new MyEventArgs() { imageAsBitmap = m.Bitmap });

            //            double fps = myCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
            //            await Task.Delay(1000 / Convert.ToInt32(fps));
            //        }
            //        else
            //        {
            //            myCapture.Dispose();
            //            myCapture = new VideoCapture(CamRTSPUrl);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"Message: {ex.Message}");
            //    }
            //}


        }
    }
}
