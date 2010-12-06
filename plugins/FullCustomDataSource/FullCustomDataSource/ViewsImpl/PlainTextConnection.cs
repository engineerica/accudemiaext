namespace FullCustomDataSource.ViewsImpl
{
	using System;
	using System.Windows.Forms;
	using AccudemiaDataX.Core;

	public partial class PlainTextConnection : UserControl, IConfigView
	{
		public PlainTextConnection()
		{
			InitializeComponent();
		}

		public IDataSource DataSource { get; set; }

		public void OnLoad()
		{
			// Load the options for import
			if (cbFieldDelimiter.Items.Count <= 0)
			{
				cbFieldDelimiter.Items.Add(new ComboItem(',', "Comma"));
				cbFieldDelimiter.Items.Add(new ComboItem('\t', "Tab"));
				cbFieldDelimiter.Items.Add(new ComboItem(';', "Semicolon"));
				cbFieldDelimiter.Items.Add(new ComboItem(' ', "Space"));
				cbFieldDelimiter.SelectedIndex = 0;

				cbTextQualifier.Items.Add(new ComboItem('"', "\""));
				cbTextQualifier.Items.Add(new ComboItem('\'', "\'"));
				cbTextQualifier.SelectedIndex = 0;
			}

			var ds = DataSource as PlainTextDataSource;
			if (ds != null)
			{
				txtFileName.Text = ds.SourceFile;
				foreach (ComboItem item in cbFieldDelimiter.Items)
				{
					if (item.Key == ds.Delimiter)
					{
						cbFieldDelimiter.SelectedItem = item;
					}
				}
				foreach (ComboItem item in cbTextQualifier.Items)
				{
					if (item.Key == ds.TextQualifier)
					{
						cbTextQualifier.SelectedItem = item;
					}
				}
			}
		}

		public string FileName
		{
			get { return txtFileName.Text; }
		}

		public char TextQualifier
		{
			get { return ((ComboItem) cbTextQualifier.SelectedItem).Key; }
		}

		public char Delimiter
		{
			get { return ((ComboItem) cbFieldDelimiter.SelectedItem).Key; }
		}

		private void btnSearchFile_Click(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog();
			dialog.Filter = "Comma Separated Files (*.csv)|*.csv|All Files (*.*)|*.*";
			dialog.Title = "Choose File to Import";
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				txtFileName.Text = dialog.FileName;
			}
		}

		private class ComboItem
		{
			public ComboItem(char key, string value)
			{
				Key = key;
				Value = value;
			}

			public char Key { get; set; }
			public string Value { get; set; }

			public override string ToString()
			{
				return Value;
			}
		}
	}
}