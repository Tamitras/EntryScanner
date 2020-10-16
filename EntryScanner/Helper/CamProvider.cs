using Emgu.CV;
using Emgu.CV.Structure;
using EntryScanner.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace EntryScanner
{
    public static class CamProvider
    {
        private static readonly CascadeClassifier cascadeClassifier = new CascadeClassifier(@"Cascades\haarcascade_frontalface_alt_tree.xml");

        private const string CamRTSPUrl = "rtsp://admin:123456@192.168.0.75:554/live/ch0";

        public static Bitmap CurrentImage { get; set; }
        public static Mat CurrentMat { get; set; }

        public static event EventHandler<MyEventArgs> NewCapturedImageFromCam;
        public static event EventHandler<EventArgsStream> StatusChanged;

        public static List<Person> Persons = new List<Person>();

        public static void Initialize()
        {
            Persons = new List<Person>();
        }

        public static Rectangle[] FindFaces(Bitmap imageAsBitmap)
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

            return rectangles;
        }

        public async static void AnalyzeCamStream()
        {
            VideoCapture myCapture = new VideoCapture(CamRTSPUrl);
            Mat m = new Mat();
            var myEventargs = new MyEventArgs()
            {
                imageAsBitmap = null
            };

            try
            {
                while (true)
                {
                    m = myCapture.QueryFrame();
                    if (m!= null && !m.IsEmpty)
                    {
                        CurrentMat = m;
                        CurrentImage = m.Bitmap;
                        myEventargs.imageAsBitmap = m.Bitmap;
                        NewCapturedImageFromCam(null, myEventargs);

                        //double fps = myCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
                        //await Task.Delay(1000 / Convert.ToInt32(fps));
                    }
                    else
                    {
                        myCapture = new VideoCapture(CamRTSPUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                StatusChanged(typeof(CamProvider), new EventArgsStream() { BrokeImageStream = true });
                Console.WriteLine($"Exception: {ex}");
            }
        }
    }
}
