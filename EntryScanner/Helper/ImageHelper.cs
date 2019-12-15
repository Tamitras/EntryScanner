using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryScanner.Helper
{
    public class ImageHelper
    {
        public Double CurrentCoeff { get; set; }
        public Boolean TemlateFound_Head { get; set; }

        public event EventHandler<MyEventArgs> TemplateFound;

        /// <summary>
        /// Asynchroner Task zum Analysieren des aktuellen Screenshots
        /// </summary>
        public List<Bitmap> StartAnalyseScreenshotAsync(Image image, Rectangle[] rectangles)
        {
            return AnalyseScreenshot(image, rectangles);
        }

        private Bitmap CropImage(Image image, Rectangle rect)
        {
            Rectangle cropRect = rect;
            Bitmap src = image as Bitmap;
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }

            return target;
        }

        /// <summary>
        /// Analysiert den aktuellen Screenshot und vergleicht diesen mit einem template
        /// </summary>
        private List<Bitmap> AnalyseScreenshot(Image image, Rectangle[] rectangles)
        {
            var foundFaces = new List<Bitmap>();
            try
            {
                foreach (var rect in rectangles)
                {
                    foundFaces.Add(CropImage(image, rect));
                }

                //TODO: Exception wrong input
                // Könnte sein, dass das zu suchende Template größer ist als die Quell-Datei (Croped Image)
                //foreach (var foundFace in foundFaces)
                //{
                //    Image<Bgr, byte> source = new Image<Bgr, byte>(foundFace);
                //    Image<Bgr, byte> template = new Image<Bgr, byte>(Properties.Resources.Marina_Face); // Image A
                //    var foundTemplate = FindTemplate(template, source);
                //}
            }
            catch (Exception ex)
            {
                // this.WriteToLog(ex.Message);
            }

            return foundFaces;
        }

        private bool FindTemplate(Image<Bgr, byte> template, Image<Bgr, byte> source)
        {
            Boolean ret = false;
            using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                CurrentCoeff = maxValues[0];
                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                if (maxValues[0] > 0.77)
                {
                    // This is a match. Do something with it, for example draw a rectangle around it.

                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }

            return ret;
        }


    }
}
