using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace QuickPlot
{
	public partial class InputsForm : Form
	{
		// Private objects
		private List<PlotSettings> plotList = new List<PlotSettings>();
		private BackgroundWorker solveWorker = new BackgroundWorker();
		private static string defaultDirectory = "C:\\";

		/// <summary>
		/// Initialize the form and set up async backgroundworker
		/// </summary>
		public InputsForm()
		{
			InitializeComponent();

			plotTypeDropDownBox.SelectedIndex = 0;
			plotTypeDropDownBox.SelectedIndexChanged += new EventHandler(plotTypeDropDownBox_SelectedIndexChanged);

			plotSettingsDataGridView.CellContentClick +=
				new DataGridViewCellEventHandler(dataGridView_CellContentClick);
			plotSettingsDataGridView.CellValueChanged +=
				new DataGridViewCellEventHandler(dataGridView_CellValueChanged);

			solveWorker.WorkerReportsProgress = true;
			solveWorker.DoWork += new DoWorkEventHandler(solveWorker_DoWork);
			solveWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(solveWorker_RunWorkerCompleted);
			solveWorker.ProgressChanged += new ProgressChangedEventHandler(solveWorker_ProgressChanged);
		}

		// Change Plot Settings grid setup when plot type is changed
		private void plotTypeDropDownBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			plotSettingsDataGridView.Rows.Clear();
			plotList.Clear();
			removePlotData.Enabled = false;
			submitButton.Enabled = false;
		}

		// Handle event for Settings Options buttons being clicked
		private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1)
			{
				return;
			}

			DataGridView senderGrid = (DataGridView)sender;
			if (senderGrid[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell && e.RowIndex >= 0)
			{
				plotList[e.RowIndex].OpenOptionsDialog();
			}
		}

		// Handle event for axis choice dropdown values changing
		private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView senderGrid = (DataGridView)sender;

			if (senderGrid[e.ColumnIndex, e.RowIndex] is DataGridViewComboBoxCell && e.RowIndex >= 0)
			{
				DataGridViewComboBoxCell sendingCell = (DataGridViewComboBoxCell)senderGrid[e.ColumnIndex, e.RowIndex];
				string selectedValue = (string)sendingCell.Value;
				if (e.ColumnIndex == 2)
				{
					plotList[e.RowIndex].XColumnIndex = sendingCell.Items.IndexOf(selectedValue);
				}
				else if (e.ColumnIndex == 3)
				{
					plotList[e.RowIndex].YColumnIndex = sendingCell.Items.IndexOf(selectedValue);
				}
				else
				{
					throw new Exception("Invalid operation requested for plotAxisDropDown_CellValueChanged");
				}
			}
			else if (e.ColumnIndex == 1 && e.RowIndex >= 0)
			{
				int hl = 0;
				Int32.TryParse(senderGrid[e.ColumnIndex, e.RowIndex].Value.ToString(), out hl);
				plotList[e.RowIndex].HeaderLineCount = hl;
				UpdateXYOptions(e.RowIndex);
			}
		}

		// Add line to param list button event handler
		private void addPlotData_Click(object sender, EventArgs e)
		{
			try
			{
				// Obtain source data file
				string dataSource = "";
				OpenFileDialog selectFileDialog = new OpenFileDialog();
				if (defaultDirectory == String.Empty)
				{
					selectFileDialog.InitialDirectory = "C:\\";
				}
				else
				{
					selectFileDialog.InitialDirectory = defaultDirectory;
				}
				selectFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
				selectFileDialog.FilterIndex = 1;
				if (selectFileDialog.ShowDialog() == DialogResult.OK)
				{
					dataSource = selectFileDialog.FileName;
					defaultDirectory = dataSource.Trim().Remove(dataSource.LastIndexOf(@"\"));
				}

				// Create the plot settings and update GUI
				if (plotTypeDropDownBox.SelectedIndex == 0)
				{
					plotList.Add(new DataPlotSettings(dataSource));
				}
				else if (plotTypeDropDownBox.SelectedIndex == 1)
				{
					if (plotSettingsDataGridView.Rows.Count != 0)
					{ throw new Exception("Frequency plotting will only use the first input data set."); }
					plotList.Add(new FreqPlotSettings(dataSource));
				}
				else
				{
					throw new Exception("Invalid plot type selection");
				}

				plotSettingsDataGridView.Rows.Add();
				plotSettingsDataGridView[0, plotList.Count - 1].Value =
					plotList[plotList.Count - 1].DataFilePath.Trim().
					Remove(plotList[plotList.Count - 1].DataFilePath.LastIndexOf(@".")).
					Remove(0, plotList[plotList.Count - 1].DataFilePath.LastIndexOf(@"\") + 1);

				DataGridViewButtonCell plotOptionsCell = (DataGridViewButtonCell)plotSettingsDataGridView[4, plotList.Count - 1];
				plotOptionsCell.Value = "Options...";

				plotSettingsDataGridView[1, plotList.Count - 1].Value = 0;

				if (!removePlotData.Enabled) { removePlotData.Enabled = true; }
				if (!submitButton.Enabled) { submitButton.Enabled = true; }
			}
			catch (System.ArgumentException)
			{
				return;
			}
			catch (System.Exception)
			{
				throw;
			}
		}

		// Update the values of the entered row's xOptions and yOptions cells
		private void UpdateXYOptions(int rowIndex)
		{
			DataGridViewComboBoxCell xOptionsCell = (DataGridViewComboBoxCell)plotSettingsDataGridView[2, rowIndex];
			xOptionsCell.Items.Clear();
			xOptionsCell.Items.AddRange(plotList[rowIndex].DataLabels.ToArray());
			xOptionsCell.Value = xOptionsCell.Items[0];

			DataGridViewComboBoxCell yOptionsCell = (DataGridViewComboBoxCell)plotSettingsDataGridView[3, rowIndex];
			yOptionsCell.Items.Clear();
			yOptionsCell.Items.AddRange(plotList[rowIndex].DataLabels.ToArray());
			yOptionsCell.Value = yOptionsCell.Items[1];
		}

		// Remove line from param list button event handler
		private void removePlotData_Click(object sender, EventArgs e)
		{
			if (plotSettingsDataGridView.CurrentRow != null)
			{
				plotList.RemoveAt(plotSettingsDataGridView.CurrentRow.Index);
				plotSettingsDataGridView.Rows.Remove(plotSettingsDataGridView.CurrentRow);
				if (plotSettingsDataGridView.Rows.Count == 0)
				{
					removePlotData.Enabled = false;
					submitButton.Enabled = false;
				}
			}
		}

		// Begin plotting button event handler
		private void submitButton_Click(object sender, EventArgs e)
		{
			PlotViewer viewer = new PlotViewer();
			viewer.Configure(plotList, plotTypeDropDownBox.SelectedIndex);

			progressBar.Value = 0;
			progressBar.Visible = true;
			submitButton.Enabled = false;
			plotTypeDropDownBox.Enabled = false;
			addPlotData.Enabled = false;
			removePlotData.Enabled = false;
			solveWorker.RunWorkerAsync(viewer);
		}

		// Async plotting DoWork function
		private void solveWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = sender as BackgroundWorker;

			e.Result = Process((PlotViewer)e.Argument, worker);
		}

		// Async plotting completed function
		private void solveWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				MessageBox.Show(e.Error.Message);
			}
			else
			{
				PlotViewer viewer = (PlotViewer)e.Result;
				viewer.Show();
			}
			progressBar.Visible = false;
			submitButton.Enabled = true;
			plotTypeDropDownBox.Enabled = true;
			addPlotData.Enabled = true;
			removePlotData.Enabled = true;
		}

		// Async plotting progress updater function
		private void solveWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar.Value = e.ProgressPercentage;
		}

		// Async plotting helper function
		public PlotViewer Process(PlotViewer viewer, BackgroundWorker worker)
		{
			viewer.CreatePlot(worker);
			return viewer;
		}
	}
}
