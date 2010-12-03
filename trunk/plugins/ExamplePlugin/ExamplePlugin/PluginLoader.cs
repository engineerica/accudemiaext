namespace ExamplePlugin
{
	using AccudemiaDataX.Core;

	public class PluginLoader : Plugin
	{
		public override void Load()
		{
			// Add here all the data sources you want.
			Bind<IDataSource>().To<MockDataSource>();
		}
	}
}