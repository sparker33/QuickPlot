using System;

namespace QuickPlot
{
    public class PointD
	{
		// Private objects
		//reserved

		// Public objects
		public double X { get; set; }
		public double Y { get; set; }

		// Class Constructor method (with initial values)
		public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }

		// Class Constructor method (empty)
        public PointD()
        {
            X = 0;
            Y = 0;
        }
    }
}