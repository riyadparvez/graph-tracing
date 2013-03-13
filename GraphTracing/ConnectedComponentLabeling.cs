using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics.Contracts;

namespace GraphTracing
{
    public class ComponentLabeling
    {
        readonly int[,] componentArray;
        readonly int width;
        readonly int height;
        readonly int [,] binaryArray;


        public ComponentLabeling(int [,] input, int width, int height)
        {
            Contract.Requires<ArgumentNullException>(input != null);

            binaryArray = input;
            this.width = width;
            this.height = height;
            componentArray = new int[width, height];
        }


        public Dictionary<int, List<Point>> Find()
        {
            Point currentPoint;
            Label currentLabel = new Label(0);
            int labelCount = 0;
            Dictionary<int, int> neighboringLabels;
            Dictionary<int, Label> allLabels = new Dictionary<int, Label>();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    currentPoint = new Point(j, i);

                    if (binaryArray[j, i] != 0)
                    {
                        neighboringLabels = GetNeighboringLabels(currentPoint);

                        if (neighboringLabels.Count == 0)
                        {
                            ++labelCount;
                            currentLabel.ID = labelCount;
                            allLabels.Add(currentLabel.ID, new Label(currentLabel.ID));
                        }
                        else
                        {
                            foreach (int label in neighboringLabels.Keys)
                            {
                                currentLabel.ID = label;//set currentLabel to the first label found in neighboring cells
                                break;
                            }
                            MergeLabels(currentLabel.ID, neighboringLabels, allLabels);
                        }
                        componentArray[j, i] = currentLabel.ID;
                    }
                }
            }


            Dictionary<int, List<Point>> Patterns = AggregatePatterns(allLabels);

            return Patterns;
        }


        private bool InRange(int i, int j)
        {
            if (i < 0)
            {
                return false;
            }
            if (j < 0)
            {
                return false;
            }
            if (i >= width)
            {
                return false;
            }
            if (j >= height)
            {
                return false;
            }

            return true;
        }


        Dictionary<int, int> GetNeighboringLabels(Point p)
        {
            Dictionary<int, int> neighboringLabels = new Dictionary<int, int>();
            int x = p.Y;
            int y = p.X;

            /*for (int i = x - 1; i < height; i++)
            {
                if (CheckWithinBoundaries(i, x + 2))
                {
                    for (int j = y - 1; j < width; j++)
                    {
                        if (CheckWithinBoundaries(j, y + 2))
                        {
                            if (componentArray[j, i] != 0)
                            {
                                if (!neighboringLabels.ContainsKey(componentArray[j, i]))
                                {
                                    neighboringLabels.Add(componentArray[j, i], 0);
                                }
                            }
                        }
                    }
                }
            }*/

            if (InRange(p.X + 1, p.Y) && componentArray[p.X + 1, p.Y] != 0)
            {
                if (!neighboringLabels.ContainsKey(componentArray[p.X + 1, p.Y]))
                {
                    neighboringLabels.Add(componentArray[p.X + 1, p.Y], 0);
                }
            }
            if (InRange(p.X - 1, p.Y) && componentArray[p.X - 1, p.Y] != 0)
            {
                if (!neighboringLabels.ContainsKey(componentArray[p.X - 1, p.Y]))
                {
                    neighboringLabels.Add(componentArray[p.X - 1, p.Y], 0);
                }
            }
            if (InRange(p.X + 1, p.Y + 1) && componentArray[p.X + 1, p.Y + 1] != 0)
            {
                if (!neighboringLabels.ContainsKey(componentArray[p.X + 1, p.Y + 1]))
                {
                    neighboringLabels.Add(componentArray[p.X + 1, p.Y + 1], 0);
                }
            }
            if (InRange(p.X - 1, p.Y + 1) && componentArray[p.X - 1, p.Y + 1] != 0)
            {
                if (!neighboringLabels.ContainsKey(componentArray[p.X - 1, p.Y + 1]))
                {
                    neighboringLabels.Add(componentArray[p.X - 1, p.Y + 1], 0);
                }
            }
            if (InRange(p.X - 1, p.Y - 1) && componentArray[p.X - 1, p.Y - 1] != 0)
            {
                if (!neighboringLabels.ContainsKey(componentArray[p.X - 1, p.Y - 1]))
                {
                    neighboringLabels.Add(componentArray[p.X - 1, p.Y - 1], 0);
                }
            }
            if (InRange(p.X + 1, p.Y - 1) && componentArray[p.X + 1, p.Y - 1] != 0)
            {
                if (!neighboringLabels.ContainsKey(componentArray[p.X + 1, p.Y - 1]))
                {
                    neighboringLabels.Add(componentArray[p.X + 1, p.Y - 1], 0);
                }
            }
            if (InRange(p.X, p.Y + 1) && componentArray[p.X, p.Y + 1] != 0)
            {
                if (!neighboringLabels.ContainsKey(componentArray[p.X, p.Y + 1]))
                {
                    neighboringLabels.Add(componentArray[p.X, p.Y + 1], 0);
                }
            }
            if (InRange(p.X, p.Y - 1) && componentArray[p.X, p.Y - 1] != 0)
            {
                if (!neighboringLabels.ContainsKey(componentArray[p.X, p.Y - 1]))
                {
                    neighboringLabels.Add(componentArray[p.X, p.Y - 1], 0);
                }
            }


            return neighboringLabels;
        }


        bool CheckWithinBoundaries(int j, int y)
        {
            return j > -1 && j < y;
        }


        void MergeLabels(int currentLabel, Dictionary<int, int> neighboringLabels, Dictionary<int, Label> labels)
        {
            Label root = labels[currentLabel].GetRoot();
            Label neighbor;

            foreach (int key in neighboringLabels.Keys)
            {
                if (key != currentLabel)
                {
                    neighbor = labels[key];

                    if (neighbor.GetRoot() != root)
                    {
                        neighbor.Root = root;
                    }
                }
            }
        }


        Dictionary<int, List<Point>> AggregatePatterns(Dictionary<int, Label> allLabels)
        {
            int patternNumber;
            List<Point> shape;
            Dictionary<int, List<Point>> Patterns = new Dictionary<int, List<Point>>();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    patternNumber = componentArray[i, j];

                    if (patternNumber != 0)
                    {
                        patternNumber = allLabels[patternNumber].GetRoot().ID;

                        if (!Patterns.ContainsKey(patternNumber))
                        {
                            shape = new List<Point>();
                            shape.Add(new Point(i, j));
                            Patterns.Add(patternNumber, shape);
                        }
                        else
                        {
                            shape = Patterns[patternNumber];
                            shape.Add(new Point(i, j));
                            Patterns[patternNumber] = shape;
                        }
                    }
                }
            }
            return Patterns;
        }
    }
}