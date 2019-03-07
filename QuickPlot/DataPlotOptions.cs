using System;
using System.Drawing;

namespace QuickPlot
{
    public class DataPlotOptions
    {
		// Properties
		public Color PlotColor { get; set; }
		public Scaling PlotScaling { get; set; }
		public string PlotColorString
        {
            get{return PlotColor.ToString().
                Remove(PlotColor.ToString().Length - 1).
                Remove(0, 7);}
        }

		// Class Constructor method
        public DataPlotOptions(Color color, Scaling scaling)
        {
            PlotColor = color;
            PlotScaling = scaling;
        }
    }
}