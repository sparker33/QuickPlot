using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace QuickPlot
{
    public class PlottableData
	{
		// Private objects
		private List<PointD> _points = new List<PointD>();

		// Public objects
		public string Name { get; }
		public IEnumerable<PointD> Points()
        {
            int n = 0;
            while (n < _points.Count)
            {
                yield return _points[n++];
            }
        }
		public double XMin { get; private set; } = Double.MaxValue;
		public double XMax { get; private set; } = Double.MinValue;
		public double YMin { get; private set; } = Double.MaxValue;
		public double YMax { get; private set; } = Double.MinValue;
		public string XVarName { get; }
		public string YVarName { get; }

		// Class constructor method
		public PlottableData(PlotSettings settings)
        {
            Name = settings.DataFilePath.Trim().
				Remove(settings.DataFilePath.LastIndexOf(@".")).
				Remove(0, settings.DataFilePath.LastIndexOf(@"\") + 1);
            
            double x = 0.0d;
            double y = 0.0d;
            StreamReader inputsReader = new StreamReader(settings.DataFilePath);
			for (int i = 0; i < settings.HeaderLineCount; i++)
			{
				inputsReader.ReadLine();
			}
            string line = inputsReader.ReadLine();
            string[] values = line.Split(',');
            XVarName = values[settings.XColumnIndex];
            YVarName = values[settings.YColumnIndex];
            while ((!Double.TryParse(values[settings.XColumnIndex], out x)
                || !Double.TryParse(values[settings.YColumnIndex], out y))
                && !inputsReader.EndOfStream)
            {
                line = inputsReader.ReadLine();
                values = line.Split(',');
            }

            if (inputsReader.EndOfStream)
            {
                throw new Exception("No valid (numeric) values found in X and/or Y collumns.");
            }

            do
            {
                if (_points.Count > 0)
                {
                    if (x < _points.Last().X)
                    {
                        throw new Exception("X values are not in increasing order.");
                    }
                }
                _points.Add(new PointD(x, y));
                FindMaxMin(x, y);
                line = inputsReader.ReadLine();
                values = line.Split(',');
                if (!Double.TryParse(values[settings.XColumnIndex], out x))
                {
                    throw new Exception("Invalid (non-numeric or blank) value found in X collumn.");
                }
                if (!Double.TryParse(values[settings.YColumnIndex], out y))
                {
                    throw new Exception("Invalid (non-numeric or blank) value found in Y collumn.");
                }
            } while (!inputsReader.EndOfStream);

		    inputsReader.Close();
        }

        // Helper method to find and set max/min values
        private void FindMaxMin(double x, double y)
        {
            if (x < XMin)
            {
                XMin = x;
            }
            if (x > XMax)
            {
                XMax = x;
            }
            if (y < YMin)
            {
                YMin = y;
            }
            if (y > YMax)
            {
                YMax = y;
            }
        }

        // Method to scale all y-values to parameterized min/max range
        public void ScaleY(double minVal, double maxVal)
        {
            foreach (PointD point in _points)
            {
                point.Y = (point.Y - YMin) * (maxVal - minVal) / (YMax - YMin) + minVal;
            }
            YMin = minVal;
            YMax = maxVal;
        }
    }
}