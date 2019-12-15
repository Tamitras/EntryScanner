using System;
using System.Drawing;

namespace EntryScanner
{
    public class MyEventArgs : EventArgs
    {
        public Bitmap imageAsBitmap { get; set; }
    }
}
