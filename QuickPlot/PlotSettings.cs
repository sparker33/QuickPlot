using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QuickPlot
{
	public class PlotSettings
	{
		// Private objects
		protected List<string> dataLabels;
		protected int headerLineCount = 0;

		// Public objects
		public IEnumerable<string> DataLabels
		{
			get { return dataLabels; }
		}
		public string DataFilePath { get; protected set; }
		public int HeaderLineCount { get => headerLineCount; set { headerLineCount = value; UpdtaeDataLabels(); } }
		public int XColumnIndex { get; set; } = 0;
		public int YColumnIndex { get; set; } = 1;

		/// <summary>
		/// Default class constructor method. Not intended for external use
		/// </summary>
		protected PlotSettings()
		{
		}

		/// <summary>
		/// Class constructor taking path to the data source file for this plot
		/// </summary>
		/// <param name="dataFile"> String path to data file. </param>
		public PlotSettings(string dataFile)
		{
			DataFilePath = dataFile;
			UpdtaeDataLabels();
		}

		/// <summary>
		/// Helper function to set the 
		/// </summary>
		protected void UpdtaeDataLabels()
		{
			StreamReader inputsReader = new StreamReader(DataFilePath);
			for (int i = 0; i < headerLineCount; i++)
			{
				inputsReader.ReadLine();
			}
			dataLabels = new List<string>(inputsReader.ReadLine().Split(','));
			inputsReader.Close();
		}

		/// <summary>
		/// Method to open this PlotSettings' options dialogue
		/// </summary>
		public virtual void OpenOptionsDialog()
		{
			System.Windows.Forms.MessageBox.Show("No settings available for this plot type.");
		}
	}
}
