namespace FullCustomDataSource
{
	using AccudemiaDataX.Core;
	using Ninject.Modules;

	public class PluginLoader : NinjectModule
	{
		public override void Load()
		{
			Bind<IDataSource>().To<PlainTextDataSource>();
		}
	}
}