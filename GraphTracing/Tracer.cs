using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics.Contracts;

namespace GraphTracing
{
    class Tracer
    {
        private int[,] grayArray;
        private int[,] binaryArray;
        private double[,] weightedArray;
        private int[,] componentArray;

        //private int[,] ;

        private int height;
        private int width;
        private ComponentLabeling componentLabeling;
        private Dictionary<int, List<Point>> connectedComponents;

        private Bitmap original;
        public Bitmap BinaryImage { get; set; }
        public int ComponentCount { get; set; }


        public Tracer(Bitmap original) 
        {
            Contract.Requires<ArgumentNullException>(original != null);
            
            this.original = original;
            height = original.Height;
            width = original.Width;

            grayArray = new int[width, height];
            binaryArray = new int[width, height];
            componentArray = new int[width, height];
            weightedArray = new double[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    componentArray[i, j] = 0;
                }
            }
        }


        public Bitmap MakeGrayscale()
        {
            Bitmap newBitmap = new Bitmap(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color originalColor = original.GetPixel(i, j);

                    int grayScale = (int)((originalColor.R * .1) + (originalColor.G * .59)
                                        + (originalColor.B * .31));

                    grayArray[i, j] = grayScale;

                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    newBitmap.SetPixel(i, j, newColor);
                }
            }

            return newBitmap;
        }


        public Bitmap MakeBinaryImage(int [,] arr)
        {
            Contract.Requires<ArgumentNullException>(arr != null);

            Bitmap newBitmap = new Bitmap(width, height);
            Color newColor;

            for(int i=0; i<width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (arr[i, j] == 1)
                    {
                        newColor = Color.FromArgb(0, 0, 0);
                    }
                    else 
                    {
                        newColor = Color.FromArgb(255, 255, 255);
                    }

                    newBitmap.SetPixel(i, j, newColor);
                }
            }

            return newBitmap;
        }

        public void MakeBinary()
        {
            for(int i=0; i< width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (grayArray[i, j] < 100)
                    {
                        binaryArray[i, j] = 1;
                    }
                    else
                    {
                        binaryArray[i, j] = 0;
                    }
                }
            }
        }


        private double Filter(int indexX, int indexY, int number)
        {
            Contract.Requires<ArgumentOutOfRangeException>(indexX >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(indexY >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(number >= 0);

            int lowerX = indexX - number;
            int lowerY = indexY - number;
            int higherX = indexX + number;
            int higherY = indexY + number;

            if(lowerX<0)
            {
                lowerX = 0;
            }
            if(lowerY<0)
            {
                lowerY = 0;
            }
            if(higherX>=width)
            {
                higherX = width - 1;
            }
            if(higherY>=height)
            {
                higherY = height - 1;
            }

            int count = 0;
            int loopCount = 0;

            for(int i=lowerX; i<=higherX; i++)
            {
                for (int j = 0; j <= higherY; j++)
                {
                    count += binaryArray[i, j];
                    loopCount++;
                }
            }

            return count/(double)loopCount;
        }


        private void FilterBinaryArray()
        {
            for(int i=0; i<width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    weightedArray[i, j] = Filter(i, j, 3);
                }
            }
        }



        private void MakeWeightedArray()
        {
            for(int i=0; i<width; i++)
            {
                for (int j = 0; j < height; j++ )
                {

                }
            }
        }


        private List<Point> SlimLine(IEnumerable<Point> line) 
        {
            Contract.Requires<ArgumentNullException>(line != null);
            Contract.Ensures(Contract.Result<List<Point>>() != null);

            var q = line.ToLookup(p => p.X);
            List<Point> points = new List<Point>();
            foreach(var ps in q)
            {
                int prevX = 0;
                int prevY = 0;
                double sum = 0;
                int count = 0;
                var ordered = ps.OrderBy(p => p.X).ThenBy(p => p.Y);
                foreach (var o in ordered)
                {
                    if(o.X != prevX)
                    {
                        points.Add(new Point(o.X, (int)Math.Round(sum / count)));
                        sum = o.Y;
                        count = 1;      
                    }
                    else if (Math.Abs(o.Y - prevY) == 1)
                    {
                        sum += o.Y;
                        count++;
                    }
                    else
                    {
                        points.Add(new Point(o.X, (int)Math.Round(sum / count)));
                        sum = o.Y;
                        count = 1;
                    }
                    prevX = o.X;
                    prevY = o.Y;
                }
            }

            var query = from p in line
                        group p by p.X into d
                        select new
                        {
                            X = d.Key,
                            Y = d.Average(p=>p.Y)
                        };

/*            List<Point> points = new List<Point>();

            foreach(var element in query)
            {
                points.Add(new Point(element.X, (int)element.Y));
            }
            */
            return points;
        }


        private void Print() 
        {

            Console.WriteLine("Feature Count: {0}", ComponentCount);
            Console.WriteLine("----------------------------------------------\n");
            Console.WriteLine("Features");
            Console.WriteLine("----------------------------------------------\n");

            foreach (var feature in connectedComponents.Values)
            {
                List<Point> tempList = SlimLine(feature);

                foreach (Point p in tempList)
                {
                    //double key = (p.X / (double)width) * 100;
                    //double value = 100 - (p.Y / (double)height) * 100;

                    //Console.WriteLine("{0}, {1}", key, value);

                    Console.WriteLine(p.X + " " + p.Y);
                }

                Console.WriteLine("---------------------------------------\n\n");
            }
            Console.ReadLine();
        }


        public void Trace()
        {
            MakeGrayscale();
            MakeBinary();
            ComponentLabeling componentLabeling = new ComponentLabeling(binaryArray, width, height);
            connectedComponents = componentLabeling.Find();
            ComponentCount = connectedComponents.Count;

            Print();
        }

    }
}
