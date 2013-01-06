using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GraphTracing
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.Write("Enter whole path of image:");
            string path = Console.ReadLine();
            new Tracer(Image.FromFile(path) as Bitmap).Trace();
   
        }
    }
}
