using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuickPlot
{
	public partial class PlotViewer : Form
	{
		// Private objects
		private int plotMode;
		private double xAxisMin = Double.MaxValue;
		private double xAxisMax = Double.MinValue;
		private double yAxisMin = Double.MaxValue;
		private double yAxisMax = Double.MinValue;
		private List<PlottableData> plotData;
		private List<Color> plotColors;
		private List<List<PointD>> freqTransVals;

		// Public objects
		//reserved

		// Auto-generated Initializer method
		public PlotViewer()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Method to set up this form and GUI elements based on the supplied PlotSettings
		/// </summary>
		/// <param name="plotSettings"> Input PlotSettings. </param>
		/// <param name="plotMode"> Plotting mode. </param>
		public void Configure(List<PlotSettings> plotSettings, int plotMode)
		{
			this.plotMode = plotMode;

			// Read in general 2D data
			plotData = new List<PlottableData>();
			plotColors = new List<Color>();
			foreach (PlotSettings settings in plotSettings)
			{
				plotData.Add(new PlottableData(settings));
				if (plotData.Last().XMin < xAxisMin)
				{
					xAxisMin = plotData.Last().XMin;
				}
				if (plotData.Last().XMax > xAxisMax)
				{
					xAxisMax = plotData.Last().XMax;
				}
				if (plotData.Last().YMin < yAxisMin)
				{
					yAxisMin = plotData.Last().YMin;
				}
				if (plotData.Last().YMax > yAxisMax)
				{
					yAxisMax = plotData.Last().YMax;
				}
			}

			// Perform additional mode-specific behavior
			if (plotMode == 0)
			{
				IEnumerator<PlotSettings> settingsEnumerator = plotSettings.GetEnumerator();
				DataPlotSetup(settingsEnumerator);
			}
			else if (plotMode == 1)
			{
				FreqPlotSettings freqSettings = (FreqPlotSettings)plotSettings[0];
				FreqPlotSetup(freqSettings);
			}
		}

		/// <summary>
		/// Method to finalize plot.
		/// </summary>
		/// <param name="worker"> Background worker used to conduct plot setup. </param>
		public void CreatePlot(BackgroundWorker worker)
		{
			InitializeChartArea();

			switch (plotMode)
			{
				case 0: // DataPlot
					IEnumerator<PlottableData> dataEnumerator = plotData.GetEnumerator();
					for (int i = 0; dataEnumerator.MoveNext(); i++)
					{
						chart.Series.Add(new Series());
						chart.Series[i].ChartType = SeriesChartType.Line;
						chart.Series[i].Color = plotColors[i];
						foreach (PointD point in dataEnumerator.Current.Points())
						{
							chart.Series[i].Points.AddXY(point.X, point.Y);
						}
					}
					chart.Invalidate();
					break;
				case 1: // Freqplot
					Series freqDataSeries = new Series();
					freqDataSeries.ChartType = SeriesChartType.Point;
					chart.ChartAreas[0].AxisY.Minimum = Math.Round(0f, 0);
					chart.ChartAreas[0].AxisY.Maximum = Math.Round((float)freqTransVals.Count, 0);
					CreateMarkers(chart);
					double yIndex = 0.5d;
					int colorIndex = 0;
					foreach (List<PointD> pointList in freqTransVals)
					{
						foreach (PointD point in pointList)
						{
							freqDataSeries.Points.AddXY(point.X, yIndex);
							freqDataSeries.Points.Last().MarkerImage = "MarkerImage" + colorIndex.ToString();
							colorIndex++;
						}
						yIndex += 1.0d;
					}
					chart.Series.Add(freqDataSeries);
					chart.Invalidate();
					break;
				default:
					throw new Exception("Invalid Plot mode in PlotViewer.CreatePlot");
			}
		}

		/// <summary>
		/// Helper method to set up data plots
		/// </summary>
		/// <param name="settingsEnumerator"> Collection of settings for data to be plotted. </param>
		private void DataPlotSetup(IEnumerator<PlotSettings> settingsEnumerator)
		{
			foreach (PlottableData data in plotData)
			{
				settingsEnumerator.MoveNext();
				DataPlotSettings settings = (DataPlotSettings)settingsEnumerator.Current;
				plotColors.Add(settings.PlotColor);
				if (settings.PlotScaling.ScaleMode == Scaling.ScaleOptions[0])
				{
					data.ScaleY(yAxisMin, yAxisMax);
				}
			}
		}

		/// <summary>
		/// Helper method to set up frequency plots (does frequency transform)
		/// </summary>
		/// <param name="freqSettings"> Settings for frequency transform. </param>
		private void FreqPlotSetup(FreqPlotSettings freqSettings)
		{
			double xAxisRange = xAxisMax - xAxisMin;
			freqTransVals = new List<List<PointD>>();
			List<double> transformTempRowIn = new List<double>();
			IList<double> transformTempRowOut = new List<double>();

			foreach (PointD point in plotData[0].Points())
			{
				transformTempRowIn.Add(point.Y);
			}
			while (true)
			{
				transformTempRowIn = new List<double>(freqSettings.WaveOption
					.Transform(transformTempRowIn, out transformTempRowOut));
				if (transformTempRowIn.Count == 0) { break; }
				freqTransVals.Add(new List<PointD>());
				double xPosition = xAxisMin + xAxisRange / (2.0d * transformTempRowOut.Count);
				foreach (double freqVal in transformTempRowOut)
				{
					freqTransVals.Last().Add(new PointD(xPosition, freqVal));
					xPosition += xAxisRange / transformTempRowOut.Count;
				}
			}
		}

		/// <summary>
		/// Method to set up the chart and display area.
		/// </summary>
		private void InitializeChartArea()
		{
			this.SuspendLayout();
			chart.Resize += new EventHandler(chart_Resize);
			chart.ChartAreas.Clear();
			ChartArea chartArea1 = new ChartArea();

			// Set up chart axis title properties
			Font axisFont = new Font("Arial", 12f);
			chartArea1.AxisX.TitleFont = axisFont;
			chartArea1.AxisY.TitleFont = axisFont;
			chartArea1.AxisX.Title = plotData[0].XVarName;
			if (plotMode == 0) { chartArea1.AxisY.Title = plotData[0].YVarName; }
			else if (plotMode == 1) { chartArea1.AxisY.Title = "Scale"; }

			// Set up chart axis bounds
			double xAxisPadding = 0.00d * (xAxisMax - xAxisMin);
			chartArea1.AxisX.Minimum = Math.Round(xAxisMin - xAxisPadding, 3);
			chartArea1.AxisX.Maximum = Math.Round(xAxisMax + xAxisPadding, 3);
			if (plotMode == 0) // FreqPlots use integer y-axis (scale/frequency levels)
			{
				double yAxisPadding = 0.00d * (yAxisMax - yAxisMin);
				chartArea1.AxisY.Minimum = Math.Round(yAxisMin - yAxisPadding, 3);
				chartArea1.AxisY.Maximum = Math.Round(yAxisMax + yAxisPadding, 3);
			}

			// Add newly created ChartArea to _chart
			// Clear chart Series and Legends
			chart.ChartAreas.Add(chartArea1);
			chart.Series.Clear();
			chart.Legends.Clear();

			// Perform general display setup finalization
			this.ResumeLayout();
		}

		/// <summary>
		/// Method to resize FreqPlot marker sizes when chart is resized
		/// </summary>
		/// <param name="sender"> Event-sending chart. </param>
		/// <param name="e"> Parameters for chart resizing. </param>
		private void chart_Resize(object sender, EventArgs e)
		{
			if (plotMode == 1)
			{
				Chart sendingChart = (Chart)sender;
				if (sendingChart.ChartAreas.Count != 0)
				{
					CreateMarkers(sendingChart);
				}
			}
		}

		/// <summary>
		/// Helper method to create FreqPlot markers (bmps)
		/// </summary>
		/// <param name="sendingChart"> Chart requiring new markers. </param>
		private void CreateMarkers(Chart sendingChart)
		{
			if (sendingChart.ChartAreas.Count == 0)
			{
				throw new Exception("CreateMarkers was called before chart area was initialized.");
			}

			// Determine correct Inner Plot Area size
			// If chart size has not yet been set up, initialize default values
			// default values are correct for _viewerWidth = 1050 and _viewerHeight = 800
			int plottableWidth;
			int plottableHeight;
			if (sendingChart.ChartAreas[0].InnerPlotPosition.Width == 0 ||
				sendingChart.ChartAreas[0].InnerPlotPosition.Height == 0 ||
				sendingChart.ChartAreas[0].Position.Width == 0 ||
				sendingChart.ChartAreas[0].Position.Height == 0)
			{
				plottableWidth = (int)(90.0f * (94.0f * sendingChart.ClientSize.Width / 100.0f) / 100.0f);
				plottableHeight = (int)(90.0f * (94.0f * sendingChart.ClientSize.Height / 100.0f) / 100.0f);
			}
			else
			{
				plottableWidth = (int)(sendingChart.ChartAreas[0].InnerPlotPosition.Width
					* (sendingChart.ChartAreas[0].Position.Width
					* sendingChart.ClientSize.Width / 100.0f) / 100.0f);
				plottableHeight = (int)(sendingChart.ChartAreas[0].InnerPlotPosition.Height
					* (sendingChart.ChartAreas[0].Position.Height
					* sendingChart.ClientSize.Height / 100.0f) / 100.0f);
			}

			// Determine color scaling parameters and generate ColorScaler
			double minFreqMag = Double.MaxValue;
			double maxFreqMag = Double.MinValue;
			foreach (List<PointD> pointList in freqTransVals)
			{
				foreach (PointD point in pointList)
				{
					if (point.Y < minFreqMag)
					{
						minFreqMag = point.Y;
					}
					if (point.Y > maxFreqMag)
					{
						maxFreqMag = point.Y;
					}
				}
			}
			ColorScaler scaler = new ColorScaler(minFreqMag, maxFreqMag);

			// Initialize marker images
			int markerWidth;
			int markerHeight = (int)Math.Max(1.0d, plottableHeight / freqTransVals.Count);
			int colorIndex = 0;
			foreach (NamedImage image in sendingChart.Images) image.Dispose();
			sendingChart.Images.Clear();
			foreach (List<PointD> freqValsList in freqTransVals)
			{
				markerWidth = (int)Math.Max(1.0d, plottableWidth / (double)freqValsList.Count);
				foreach (PointD freqVal in freqValsList)
				{
					Bitmap marker = new Bitmap(markerWidth, markerHeight);
					using (Graphics G = Graphics.FromImage(marker)) G.Clear(scaler.GetScaledColor(freqVal.Y));
					sendingChart.Images.Add(new NamedImage("MarkerImage" + colorIndex.ToString(), marker));
					colorIndex++;
				}
			}
		}
	}
}
