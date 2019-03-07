namespace QuickPlot
{
	partial class InputsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.submitButton = new System.Windows.Forms.Button();
			this.plotTypeDropDownBox = new System.Windows.Forms.ComboBox();
			this.plotSettingsDataGridView = new System.Windows.Forms.DataGridView();
			this.DataName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.headerLineCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.xData = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.yData = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.Options = new System.Windows.Forms.DataGridViewButtonColumn();
			this.addPlotData = new System.Windows.Forms.Button();
			this.removePlotData = new System.Windows.Forms.Button();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			((System.ComponentModel.ISupportInitialize)(this.plotSettingsDataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// submitButton
			// 
			this.submitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.submitButton.Enabled = false;
			this.submitButton.Location = new System.Drawing.Point(479, 263);
			this.submitButton.Name = "submitButton";
			this.submitButton.Size = new System.Drawing.Size(75, 23);
			this.submitButton.TabIndex = 0;
			this.submitButton.Text = "Plot";
			this.submitButton.UseVisualStyleBackColor = true;
			this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
			// 
			// plotTypeDropDownBox
			// 
			this.plotTypeDropDownBox.AllowDrop = true;
			this.plotTypeDropDownBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.plotTypeDropDownBox.FormattingEnabled = true;
			this.plotTypeDropDownBox.Items.AddRange(new object[] {
            "Data Plot",
            "Frequency Content"});
			this.plotTypeDropDownBox.Location = new System.Drawing.Point(13, 13);
			this.plotTypeDropDownBox.Name = "plotTypeDropDownBox";
			this.plotTypeDropDownBox.Size = new System.Drawing.Size(162, 21);
			this.plotTypeDropDownBox.TabIndex = 1;
			// 
			// plotSettingsDataGridView
			// 
			this.plotSettingsDataGridView.AllowUserToAddRows = false;
			this.plotSettingsDataGridView.AllowUserToDeleteRows = false;
			this.plotSettingsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.plotSettingsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveBorder;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.plotSettingsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.plotSettingsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.plotSettingsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataName,
            this.headerLineCount,
            this.xData,
            this.yData,
            this.Options});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.plotSettingsDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
			this.plotSettingsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.plotSettingsDataGridView.GridColor = System.Drawing.SystemColors.ActiveBorder;
			this.plotSettingsDataGridView.Location = new System.Drawing.Point(13, 41);
			this.plotSettingsDataGridView.MultiSelect = false;
			this.plotSettingsDataGridView.Name = "plotSettingsDataGridView";
			this.plotSettingsDataGridView.RowHeadersVisible = false;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
			this.plotSettingsDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
			this.plotSettingsDataGridView.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
			this.plotSettingsDataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.plotSettingsDataGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
			this.plotSettingsDataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			this.plotSettingsDataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
			this.plotSettingsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.plotSettingsDataGridView.Size = new System.Drawing.Size(541, 216);
			this.plotSettingsDataGridView.TabIndex = 2;
			// 
			// DataName
			// 
			this.DataName.HeaderText = "Name";
			this.DataName.Name = "DataName";
			this.DataName.ReadOnly = true;
			// 
			// headerLineCount
			// 
			this.headerLineCount.HeaderText = "Header Lines";
			this.headerLineCount.Name = "headerLineCount";
			// 
			// xData
			// 
			this.xData.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
			this.xData.HeaderText = "X Data";
			this.xData.Name = "xData";
			this.xData.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// yData
			// 
			this.yData.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
			this.yData.HeaderText = "Y Data";
			this.yData.Name = "yData";
			this.yData.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// Options
			// 
			this.Options.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.Options.HeaderText = "Options";
			this.Options.Name = "Options";
			this.Options.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// addPlotData
			// 
			this.addPlotData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.addPlotData.Location = new System.Drawing.Point(12, 263);
			this.addPlotData.Name = "addPlotData";
			this.addPlotData.Size = new System.Drawing.Size(75, 23);
			this.addPlotData.TabIndex = 3;
			this.addPlotData.Text = "Add";
			this.addPlotData.UseVisualStyleBackColor = true;
			this.addPlotData.Click += new System.EventHandler(this.addPlotData_Click);
			// 
			// removePlotData
			// 
			this.removePlotData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.removePlotData.Enabled = false;
			this.removePlotData.Location = new System.Drawing.Point(93, 263);
			this.removePlotData.Name = "removePlotData";
			this.removePlotData.Size = new System.Drawing.Size(75, 23);
			this.removePlotData.TabIndex = 4;
			this.removePlotData.Text = "Remove";
			this.removePlotData.UseVisualStyleBackColor = true;
			this.removePlotData.Click += new System.EventHandler(this.removePlotData_Click);
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(175, 264);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(298, 21);
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar.TabIndex = 5;
			this.progressBar.Visible = false;
			// 
			// InputsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(566, 298);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.removePlotData);
			this.Controls.Add(this.addPlotData);
			this.Controls.Add(this.plotSettingsDataGridView);
			this.Controls.Add(this.plotTypeDropDownBox);
			this.Controls.Add(this.submitButton);
			this.Name = "InputsForm";
			this.Text = "QuickPlot";
			((System.ComponentModel.ISupportInitialize)(this.plotSettingsDataGridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button submitButton;
		private System.Windows.Forms.ComboBox plotTypeDropDownBox;
		private System.Windows.Forms.DataGridView plotSettingsDataGridView;
		private System.Windows.Forms.Button addPlotData;
		private System.Windows.Forms.Button removePlotData;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.DataGridViewTextBoxColumn DataName;
		private System.Windows.Forms.DataGridViewTextBoxColumn headerLineCount;
		private System.Windows.Forms.DataGridViewComboBoxColumn xData;
		private System.Windows.Forms.DataGridViewComboBoxColumn yData;
		private System.Windows.Forms.DataGridViewButtonColumn Options;
	}
}

