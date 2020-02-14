using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryScanner.Helper
{
    public class Person
    {
        public List<Bitmap> Faces { get; set; }

        public DateTime LastSeen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Person()
        {
            Initialize();
        }

        private void Initialize()
        {
            Faces = new List<Bitmap>();
        }
    }
}
