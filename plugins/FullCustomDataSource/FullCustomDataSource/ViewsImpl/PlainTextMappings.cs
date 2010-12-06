namespace FullCustomDataSource.ViewsImpl
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Reflection;
	using System.Windows.Forms;
	using AccudemiaDataX.Core;
	using AccudemiaDataX.Core.DataSources.CSVParser;
	using AccudemiaDataX.Core.Model;
	using AccudemiaDataX.Core.Model.Base;
	using log4net;
	using PlainTextDataSource=FullCustomDataSource.PlainTextDataSource;

	public partial class PlainTextMappings : UserControl, IConfigView
	{
		private static readonly ILog log = LogManager.GetLogger(typeof (PlainTextMappings));

		public PlainTextMappings()
		{
			InitializeComponent();
		}

		public IDataSource DataSource { get; set; }

		public void OnLoad()
		{
			var ds = DataSource as PlainTextDataSource;
			if (ds == null)
				return;

			IDictionary<string, string> fields;
			try
			{
				fields = ds.GetFileFields();
			}
			catch (DuplicateHeaderException ex)
			{
				MessageBox.Show("Invalid file format. The header '" + ex.HeaderName + "' is duplicated. Please go back and select another file, or fix it and try again.", "Accudemia Data Exchange",
				                MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			catch (Exception ex)
			{
				log.Error("Unable to get file headers.", ex);
				MessageBox.Show("Unable to read the file. Please check the details and verify the file is correctly formated.\nDetails:\n" + ex.Message, "Accudemia Data Exchange",
				                MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			gvMappedFields.Rows.Clear();
			foreach (var field in fields)
			{
				var row = new DataGridViewRow();

				var cellCsvField = new DataGridViewTextBoxCell { Value = field.Key };
				var cellSamples = new DataGridViewTextBoxCell { Value = field.Value };

				var cellMapsTo = new DataGridViewComboBoxCell();
				cellMapsTo.ValueMember = "Value";
				cellMapsTo.DisplayMember = "DisplayText";
				cellMapsTo.Items.AddRange(GetModelFields());

				row.Cells.Add(cellCsvField);
				row.Cells.Add(cellSamples);
				row.Cells.Add(cellMapsTo);

				// Load the saved mappings
				foreach (var savedMap in ds.MappedFields)
				{
					if (savedMap.HeaderText == field.Key)
					{
						// Now that the field is mapped, select the item in the combo
						//cellMapsTo.Value = savedMap;
						foreach (MapInfo mapInCombo in cellMapsTo.Items)
						{
							if (mapInCombo.Equals(savedMap))
							{
								cellMapsTo.Value = mapInCombo;
							}
						}
					}
				}

				
				gvMappedFields.Rows.Add(row);
			}
		}

		private static object[] modelFieldsCache;
		private static object[] GetModelFields()
		{
			if (modelFieldsCache != null)
				return modelFieldsCache;

			var results = new List<MapInfo>(40);

			results.Add(new MapInfo(null, null, null) {DisplayText = "- select -"});

			var allTypes = typeof(Student).Assembly.GetTypes();
			foreach (var type in allTypes)
			{
				// Get the entity information
				var entityDescription = type.GetCustomAttributes(typeof (ModelEntityAttribute), false);
				if (entityDescription == null || entityDescription.Length == 0)
				{
					continue;
				}
				
				string entityName = ((ModelEntityAttribute) entityDescription[0]).FriendlyName;
				
				// Get the properties information
				foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
				{
					var propertyDesc = property.GetCustomAttributes(typeof(ModelPropertyAttribute), true);
					if (propertyDesc.Length == 0)
						continue;

					var browsableAtt = property.GetCustomAttributes(typeof(BrowsableAttribute), true);
					if (browsableAtt.Length == 1 && !((BrowsableAttribute)browsableAtt[0]).Browsable)
						continue;

					if (property.PropertyType.IsByRef ||
					    typeof(IAccudemiaEntity).IsAssignableFrom(property.PropertyType))
					{
						continue;
					}

					var propAttrib = ((ModelPropertyAttribute)propertyDesc[0]);

					var displayText = entityName + "'s " + propAttrib.DisplayName;
					results.Add(new MapInfo(type, property.Name, propAttrib.DefaultFormatterType) {DisplayText = displayText});
				}
			}

			modelFieldsCache = results.ToArray();
			return modelFieldsCache;
		}

		public IEnumerable<PlainTextDataSource.FieldMap> MappedFields
		{
			get
			{
				foreach (DataGridViewRow row in gvMappedFields.Rows)
				{
					var headerText = row.Cells["FileColumn"].Value as string;
					if (string.IsNullOrEmpty(headerText))
					{
						continue;
					}

					var propertyInfo = (MapInfo) row.Cells["Property"].Value;
					if (propertyInfo != null && propertyInfo.EntityType != null)
					{
						yield return new PlainTextDataSource.FieldMap(headerText, propertyInfo.EntityType, propertyInfo.PropertyName);
					}
				}
			}
		}

		private class MapInfo : IEquatable<MapInfo>
		{
			public MapInfo(Type entityType, string propertyName, Type formatter)
			{
				EntityType = entityType;
				PropertyName = propertyName;
				Formatter = formatter;
			}

			public Type EntityType { get; set; }
			public string PropertyName { get; set; }
			public Type Formatter { get; set; }

			// Properties to show it as a combo item
			public MapInfo Value { get { return this; } }
			public string DisplayText { get; set; }

			//mapInCombo.EntityType == savedMap.EntityType &&
			//                    mapInCombo.PropertyName == savedMap.PropertyPath

			#region Equals & GetHashCode

			public bool Equals(MapInfo other)
			{
				if (other == null)
				{
					return false;
				}
				if (ReferenceEquals(this, other))
				{
					return true;
				}
				return Equals(other.EntityType, EntityType) && Equals(other.PropertyName, PropertyName);
			}

			public override bool Equals(object obj)
			{
				return Equals(obj as MapInfo);
			}

			public bool Equals(PlainTextDataSource.FieldMap obj)
			{
				Type formatterType = null;
				if (obj.Formatter != null)
				{
					formatterType = obj.Formatter.GetType();
				}
				var info = new MapInfo(obj.EntityType, obj.PropertyPath, formatterType);
				return Equals(info);
			}

			public override int GetHashCode()
			{
				unchecked
				{
					if (PropertyName == null || EntityType == null)
						return 0;
					return (EntityType.GetHashCode()*397) ^ PropertyName.GetHashCode();
				}
			}

			#endregion

			public override string ToString()
			{
				return DisplayText;
			}
		}
	}
}