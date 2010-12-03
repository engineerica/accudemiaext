namespace ExamplePlugin
{
	using System;
	using System.Windows.Forms;
	using AccudemiaDataX.Core;

	public class PluginLoader : Plugin
	{
		public override void Load()
		{
			MessageBox.Show("Hi, I'm loading the plugins...");

			// Add here all the data sources you want.
			Bind<IDataSource>().To<MockDataSource>();
		}
	}
}