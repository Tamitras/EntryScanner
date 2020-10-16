using System;
using System.Drawing;

namespace EntryScanner
{
    public class MyEventArgs : EventArgs
    {
        public Bitmap imageAsBitmap { get; set; }
    }

    public class EventArgsStream : EventArgs
    {
        public bool BrokeImageStream { get; set; }
    }
}
