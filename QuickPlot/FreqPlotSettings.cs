using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot
{
	public class FreqPlotSettings : PlotSettings
	{
		// Private objects
		private FreqPlotOptions plotOptions;

		// Public objects
		public Wave WaveOption { get { return plotOptions.WaveType; } }

		/// <summary>
		/// Default class constructor method. Not intended for external use
		/// </summary>
		protected FreqPlotSettings()
		{
		}

		/// <summary>
		/// Class constructor taking path to the data source file for this plot
		/// </summary>
		/// <param name="dataFile"> String path to data file. </param>
		public FreqPlotSettings(string dataFile) : base(dataFile)
		{
			plotOptions = new FreqPlotOptions(new Wave("Haar"));
		}

		/// <summary>
		/// Opens a frequency plot options dialog
		/// </summary>
		public override void OpenOptionsDialog()
		{
			FreqPlotOptionsForm optionsDialog = new FreqPlotOptionsForm(plotOptions);
			optionsDialog.Show();
		}
	}
}
