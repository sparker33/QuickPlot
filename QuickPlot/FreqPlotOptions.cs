using System;
using System.Drawing;

namespace QuickPlot
{
    public class FreqPlotOptions
    {
		// Properties
		public Wave WaveType { get; set; }

		// Class Constructor method
		public FreqPlotOptions(Wave waveSelection)
        {
            WaveType = waveSelection;
        }
    }
}