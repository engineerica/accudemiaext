namespace FullCustomDataSource.ViewsImpl
{
	partial class PlainTextMappings
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.gvMappedFields = new System.Windows.Forms.DataGridView();
			this.FileColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Sample = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Property = new System.Windows.Forms.DataGridViewComboBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.gvMappedFields)).BeginInit();
			this.SuspendLayout();
			// 
			// gvMappedFields
			// 
			this.gvMappedFields.AllowUserToAddRows = false;
			this.gvMappedFields.AllowUserToDeleteRows = false;
			this.gvMappedFields.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.gvMappedFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gvMappedFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			                                                                                   	this.FileColumn,
			                                                                                   	this.Sample,
			                                                                                   	this.Property});
			this.gvMappedFields.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gvMappedFields.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gvMappedFields.Location = new System.Drawing.Point(0, 0);
			this.gvMappedFields.MultiSelect = false;
			this.gvMappedFields.Name = "gvMappedFields";
			this.gvMappedFields.RowHeadersVisible = false;
			this.gvMappedFields.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
			this.gvMappedFields.Size = new System.Drawing.Size(458, 182);
			this.gvMappedFields.TabIndex = 0;
			// 
			// FileColumn
			// 
			this.FileColumn.HeaderText = "Your Header";
			this.FileColumn.Name = "FileColumn";
			this.FileColumn.ReadOnly = true;
			// 
			// Sample
			// 
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gray;
			this.Sample.DefaultCellStyle = dataGridViewCellStyle1;
			this.Sample.HeaderText = "Sample Data";
			this.Sample.Name = "Sample";
			this.Sample.ReadOnly = true;
			// 
			// Property
			// 
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.NullValue = "- select - ";
			this.Property.DefaultCellStyle = dataGridViewCellStyle2;
			this.Property.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
			this.Property.DropDownWidth = 2;
			this.Property.HeaderText = "Maps To";
			this.Property.MaxDropDownItems = 10;
			this.Property.Name = "Property";
			this.Property.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// PlainTextMappings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gvMappedFields);
			this.Name = "PlainTextMappings";
			this.Size = new System.Drawing.Size(458, 182);
			((System.ComponentModel.ISupportInitialize)(this.gvMappedFields)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView gvMappedFields;
		private System.Windows.Forms.DataGridViewTextBoxColumn FileColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn Sample;
		private System.Windows.Forms.DataGridViewComboBoxColumn Property;

	}
}