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
    public static class ServiceProvider
    {
        private static readonly CascadeClassifier cascadeClassifier = new CascadeClassifier(@"Cascades\haarcascade_frontalface_alt_tree.xml");

        private const string CamUrl = "http://192.168.0.20/image/jpeg.cgi";

        public static double ScaleFactor = 1.4;

        public static Bitmap CurrentImage { get; set; }
        public static ImageHelper ImageHelper { get; set; }

        public static event EventHandler<MyEventArgs> NewCurrentImage;
        public static event EventHandler<MyEventArgs> TemplateFound;
        public static void Initialize()
        {
            ImageHelper = new ImageHelper();
            ImageHelper.TemplateFound += ImageHelper_TemplateFound;
        }

        private static void ImageHelper_TemplateFound(object sender, MyEventArgs e)
        {
            TemplateFound(sender, new MyEventArgs() { imageAsBitmap = e.imageAsBitmap });
        }

        public static Bitmap GetScreenshot()
        {
            //Create a new bitmap.
            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                           Screen.PrimaryScreen.Bounds.Height,
                                           PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                        Screen.PrimaryScreen.Bounds.Y,
                                        0,
                                        0,
                                        Screen.PrimaryScreen.Bounds.Size,
                                        CopyPixelOperation.SourceCopy);

            // Save the screenshot to the specified path that the user has chosen.
            // bmpScreenshot.Save("Screenshot.png", ImageFormat.Png);

            return bmpScreenshot;
        }

        internal static Image FindFaces(Bitmap imageAsBitmap, out List<Bitmap> images)
        {
            Bitmap bitmap = imageAsBitmap;
            Bitmap bitmapCloned = (Bitmap)bitmap.Clone();
            Image<Bgr, byte> grayImage = new Image<Bgr, byte>(bitmap);

            // 1.4 is the best ScaleFactor
            //Rectangle[] rectangles = cascadeClassifier.DetectMultiScale(grayImage, 1.4, 0);
            Rectangle[] rectangles = cascadeClassifier.DetectMultiScale(grayImage, ServiceProvider.ScaleFactor, 0);

            foreach (var rectangle in rectangles)
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Pen pen = new Pen(Color.Red, 2))
                    {
                        graphics.DrawRectangle(pen, rectangle);
                    }
                }
            }

            // Crop Image from Rectangle and search Database for equal
            // CroptImage(Rectangles) --> Inform MainThrad and Form that someone is found

            images = ImageHelper.StartAnalyseScreenshotAsync(bitmapCloned, rectangles);

            return bitmap;
        }

        internal static void GetInitialImage()
        {
            //Load From URL
            //while (true)
            //{
            //    using (WebClient client = new WebClient())
            //    {
            //        client.DownloadDataCompleted += Client_DownloadDataCompleted;
            //        //byte[] bytes = client.DownloadData(new Uri(CamUrl));
            //        client.DownloadDataAsync(new Uri(CamUrl));
            //    }
            //    Thread.Sleep(100);
            //}

            CurrentImage = new Bitmap(Properties.Resources.suits_persons);

            NewCurrentImage(null, new MyEventArgs() { imageAsBitmap = CurrentImage });
        }

        private static void Client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            MemoryStream ms = new MemoryStream(e.Result);
            Image img = Image.FromStream(ms);
            CurrentImage = new Bitmap(img);

            NewCurrentImage(sender, new MyEventArgs() { imageAsBitmap = CurrentImage });
        }
    }
}
