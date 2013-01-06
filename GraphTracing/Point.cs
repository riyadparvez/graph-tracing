using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GraphTracing
{
   public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }


        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
