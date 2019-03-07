using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace QuickPlot
{
	public class DataPlotSettings : PlotSettings
	{
		// Private objects
		private DataPlotOptions plotOptions;

		// Public objects
		public Color PlotColor { get { return plotOptions.PlotColor; } }
		public Scaling PlotScaling { get { return plotOptions.PlotScaling; } }

		/// <summary>
		/// Default class constructor method. Not intended for external use
		/// </summary>
		protected DataPlotSettings()
		{
		}

		/// <summary>
		/// Class constructor taking path to the data source file for this plot
		/// </summary>
		/// <param name="dataFile"> String path to data file. </param>
		public DataPlotSettings(string dataFile) : base(dataFile)
		{
			plotOptions = new DataPlotOptions(Color.Blue, new Scaling("Shared"));
		}

		/// <summary>
		/// Opens a data plot options dialog
		/// </summary>
		public override void OpenOptionsDialog()
		{
			DataPlotOptionsForm optionsDialog = new DataPlotOptionsForm(plotOptions);
			optionsDialog.Show();
		}
	}
}
