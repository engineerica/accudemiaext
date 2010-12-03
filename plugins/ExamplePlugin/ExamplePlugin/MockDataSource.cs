namespace ExamplePlugin
{
	using System;
	using System.Collections.Generic;
	using AccudemiaDataX.Core;
	using AccudemiaDataX.Core.Model;
	using AccudemiaDataX.Core.Model.Base;

	[Serializable]
	public class MockDataSource : IDataSource
	{
		[NonSerialized] 
		private IList<IConfigView> views;

		private IList<IConfigView> Views
		{
			get
			{
				if (views == null || views.Count == 0)
				{
					views = new List<IConfigView>(1) { new SampleConfigView() };
				}
				return views;
			}
		}

		public IEnumerable<IAccudemiaEntity> GetEntities()
		{
			// This example shows how to create a sample group and add 2 students to it.
			// You can play with other entities located in the AccudemiaDataX.Core.Model namespace.

			var group = new PersonsGroup {Name = "At risk students"};
			var s1 = new Student
			         	{
			         		FirstName = "John",
			         		MiddleName = "",
			         		LastName = "Williams",
			         		PersonNumber = "123123123",
			         		Active = true
			         	};
			var s2 = new Student
			         	{
			         		FirstName = "Albert",
			         		MiddleName = "",
			         		LastName = "Johansen",
			         		PersonNumber = "456456456",
			         		Active = true
			         	};

			var member1 = new PersonsGroupMember(s1, group);
			var member2 = new PersonsGroupMember(s2, group);

			// Entities MUST be returned in a logical order to avoid foreign-key constrains errors.
			// The same entity can be returned multiple times because the engine will remove duplicates.
			yield return s1;
			yield return s2;
			yield return group;
			yield return member1;
			yield return member2;
		}

		public IList<IConfigView> GetConfigViews()
		{
			return Views;
		}

		public bool IsConfigViewValid(IConfigView viewToValidate)
		{
			// You can cast to SampleConfigView and see all the settings written there
			// and validate the file names, settings, etc.
			return true;
		}

		public void AcceptConfigViewChanges(IConfigView viewToAcceptChanges)
		{
			// Save the changes from the view. This class (the one implementing IDataSource) will be
			// serialized, so you can store anything you want here.
		}

		public string Describe()
		{
			return "Sample Mock Data Source";
		}

		public string DescribeResultContents()
		{
			// You can use the data source to describe the contents that will be returned.
			return "Not yet implemented";
		}

		public IEnumerable<EntityMap> GetMappedEntities()
		{
			return new List<EntityMap>();
		}

		public string Name
		{
			get { return "Mock Data Source"; }
		}

		public string Description
		{
			get { return "Reads the information from..."; }
		}

		public override string ToString()
		{
			return Name;
		}
	}
}