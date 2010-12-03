namespace ExamplePlugin
{
	using System;
	using System.Windows.Forms;
	using AccudemiaDataX.Core;

	public class PluginLoader : Plugin
	{
		public override void Load()
		{
			MessageBox.Show("Loading plugin...");
			Bind<IDataSource>().To<XmlDataSource>();
		}
	}
}