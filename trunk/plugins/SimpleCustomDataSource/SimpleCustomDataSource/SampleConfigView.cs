namespace ExamplePlugin
{
	using System.Windows.Forms;
	using AccudemiaDataX.Core;

	public partial class SampleConfigView : UserControl, IConfigView
	{
		public SampleConfigView()
		{
			InitializeComponent();
		}

		public void OnLoad()
		{
			// You can use this method to load all the initial information, and load the settings saved
			// in the DataSource
		}

		public IDataSource DataSource { get; set; }
	}
}