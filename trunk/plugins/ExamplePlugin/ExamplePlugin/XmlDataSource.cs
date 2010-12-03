namespace ExamplePlugin
{
	using System.Collections.Generic;
	using AccudemiaDataX.Core;
	using AccudemiaDataX.Core.Model;
	using AccudemiaDataX.Core.Model.Base;

	public class XmlDataSource : IDataSource
	{
		public IEnumerable<IAccudemiaEntity> GetEntities()
		{
			return null;
		}

		public IList<IConfigView> GetConfigViews()
		{
			return new List<IConfigView>();
		}

		public bool IsConfigViewValid(IConfigView viewToValidate)
		{
			return false;
		}

		public void AcceptConfigViewChanges(IConfigView viewToAcceptChanges)
		{
		}

		public string Describe()
		{
			return "Not yet implemented";
		}

		public string DescribeResultContents()
		{
			return "Not yet implemented";
		}

		public IEnumerable<EntityMap> GetMappedEntities()
		{
			return new List<EntityMap>();
		}

		public string Name
		{
			get { return "XML Data Source"; }
		}

		public string Description
		{
			get { return "Reads the information from a XML file."; }
		}
	}
}