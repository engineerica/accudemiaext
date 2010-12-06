namespace FullCustomDataSource
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;
	using System.Windows.Forms;
	using AccudemiaDataX.Core;
	using AccudemiaDataX.Core.DataSources.CSVParser;
	using AccudemiaDataX.Core.Model;
	using AccudemiaDataX.Core.Model.Base;
	using AccudemiaDataX.Utilities;
	using log4net;
	using InvalidDataException=System.IO.InvalidDataException;
	using PlainTextConnection=FullCustomDataSource.ViewsImpl.PlainTextConnection;
	using PlainTextMappings=FullCustomDataSource.ViewsImpl.PlainTextMappings;

	[Serializable]
	public class PlainTextDataSource : IDataSource
	{
		private static readonly ILog log = LogManager.GetLogger(typeof (PlainTextDataSource));

		[NonSerialized]
		private List<IConfigView> views;

		[NonSerialized]
		private Semester defaultSemester;
		public Semester DefaultSemester
		{
			get
			{
				if (defaultSemester == null)
				{
					defaultSemester = new Semester {Name = "Current"};
				}
				return defaultSemester;
			}
		}

		public PlainTextDataSource()
		{
			if (MappedFields == null)
			{
				MappedFields = new List<FieldMap>();
			}
		}

		#region Properties

		public string Name
		{
			get { return "CSV files (Custom Source)"; }
		}

		public string Description
		{
			get { return "Not available."; }
		}

		[NonSerialized]
		private Stream source;
		public Stream Source
		{
			get
			{
				if (source != null)
				{
					return source;
				}
				return new FileStream(SourceFile, FileMode.Open, FileAccess.Read);
			}
			set { source = value; }
		}

		public string SourceFile { get; set; }

		private char delimiter = ',';
		/// <summary>
		/// Gets or sets the delimiter character separating each field (default is ',').
		/// </summary>
		/// <value>The delimiter.</value>
		public char Delimiter
		{
			get { return delimiter; }
			set { delimiter = value; }
		}

		private char textQualifier = '"';

		/// <summary>
		/// Gets or sets the quotation character wrapping every field (default is ").
		/// </summary>
		/// <value>The text qualifier.</value>
		public char TextQualifier
		{
			get { return textQualifier; }
			set { textQualifier = value; }
		}

		public List<FieldMap> MappedFields { get; set; }

		#endregion

		#region Public Methods

		public PlainTextDataSource Map(FieldMap map)
		{
			MappedFields.Add(map);
			return this;
		}

		public IDictionary<string, string> GetFileFields()
		{
			var fields = new Dictionary<string, string>();

			using (var sreader = new StreamReader(Source))
			{
				sreader.BaseStream.Seek(0, SeekOrigin.Begin);
				using (var reader = new CsvReader(sreader, true,
				                                  Delimiter, TextQualifier, CsvReader.DefaultEscape,
				                                  CsvReader.DefaultComment, true))
				{
					reader.DefaultParseErrorAction = ParseErrorAction.ThrowException;
					reader.MissingFieldAction = MissingFieldAction.ReplaceByEmpty;

					// Add the headers 
					var headers = reader.GetFieldHeaders();
					foreach (var header in headers)
					{
						fields.Add(header, "");
					}

					int rowIndex = 0;
					while (reader.ReadNextRecord())
					{
						foreach (var header in headers)
						{
							var currentValue = fields[header];
							if (currentValue.Length != 0)
							{
								currentValue += ", ";
							}
							fields[header] = currentValue + reader[header];
						}

						rowIndex++;
						if (rowIndex > 2)
							break;
					}
				}
			}

			return fields;
		}

		public IEnumerable<IAccudemiaEntity> GetEntities()
		{
			using (var sreader = new StreamReader(Source))
			{
				sreader.BaseStream.Seek(0, SeekOrigin.Begin);

				// Start reading the CSV
				using (var reader = new CsvReader(sreader, true,
				                                  Delimiter, TextQualifier, CsvReader.DefaultEscape,
				                                  CsvReader.DefaultComment, true))
				{
					reader.DefaultParseErrorAction = ParseErrorAction.ThrowException;
					reader.MissingFieldAction = MissingFieldAction.ReplaceByEmpty;

					// Flag to return the semester only once
					bool semesterWasReturned = false;
					Semester semester = null;

					// Reset the default semester, because it might be changed since the last time the
					// import ran
					defaultSemester = null;

					while (reader.ReadNextRecord())
					{
						Student student = null;
						Tutor tutor = null;
						Course course = null;
						Clazz clazz = null;
						Instructor instructor = null;
						StudentEnrollment studentEnrollment = null;
						InstructorEnrollment instructorEnrollment = null;
						TutorEnrollment tutorEnrollment = null;
						Center center = null;
						CourseCenterAssignment courseCenterAssignment = null;
						PersonsGroup personsGroup = null;
						PersonsGroupMember personsGroupMember = null;

						// Create entities mapped
						if (IsEntityMapped(typeof (Student)))
						{
							student = CreateEntity<Student>(reader);
							// Set the middle name, because as this is a not commonly used field
							// it might be null but it's required to have something in order to enforce
							// unique constraint
							student.MiddleName = student.MiddleName ?? "";
						}

						if (IsEntityMapped(typeof (Instructor)))
						{
							instructor = CreateEntity<Instructor>(reader);

							// Set the middle name, because as this is a not commonly used field
							// it might be null but it's required to have something in order to enforce
							// unique constraint
							instructor.MiddleName = instructor.MiddleName ?? "";
						}

						if (IsEntityMapped(typeof (Tutor)))
						{
							tutor = CreateEntity<Tutor>(reader);

							// Set the middle name, because as this is a not commonly used field
							// it might be null but it's required to have something in order to enforce
							// unique constraint
							tutor.MiddleName = tutor.MiddleName ?? "";
						}

						if (IsEntityMapped(typeof (Semester)))
						{
							semester = CreateEntity<Semester>(reader);
							if (semester.Name == null)
							{
								semester.Name = "Default Term";
							}
						}

						if (IsEntityMapped(typeof (Course)))
						{
							course = CreateEntity<Course>(reader);
						}

						if (IsEntityMapped(typeof (Center)))
						{
							center = CreateEntity<Center>(reader);
						}

						if (IsEntityMapped(typeof (PersonsGroup)))
						{
							personsGroup = CreateEntity<PersonsGroup>(reader);
						}

						if (IsEntityMapped(typeof (Clazz)))
						{
							clazz = CreateEntity<Clazz>(reader);

							if (semester == null)
							{
								semester = DefaultSemester;
							}

							clazz.Semester = semester;
							if (course == null)
							{
								throw new InvalidDataException(
									"Unable to create the classes, you must specify the subject area to create classes.");
							}

							clazz.Course = course;
							course.HasScheduledClasses = true;
						}
						else if (student != null && course != null)
						{
							if (semester == null) semester = DefaultSemester;

							clazz = new Clazz(course, semester);
							FillEntityData(reader, clazz);

							if (clazz.ShortName == null)
							{
								clazz.ShortName = course + " - " + semester;
							}
						}

						// Make relations between entities created
						if (student != null && clazz != null)
						{
							studentEnrollment = student.AddEnrollment(clazz);
						}

						if (instructor != null && clazz != null)
						{
							instructorEnrollment = instructor.AddEnrollment(clazz);
						}

						if (tutor != null && course != null)
						{
							if (semester == null) semester = DefaultSemester;
							tutorEnrollment = tutor.AddEnrollment(course, semester);
						}

						if (center != null && course != null)
						{
							courseCenterAssignment = center.AddCourse(course);
						}

						if (student != null && personsGroup != null)
						{
							personsGroupMember = personsGroup.AddUser(student);
						}

						// Start returning all loaded entities
						if (!semesterWasReturned && semester != null)
						{
							// Return the default semester only once, all the other semester must be always returned
							if (DefaultSemester.Equals(semester))
							{
								semesterWasReturned = true;
							}

							yield return LogEntityFoundAndReturn(semester);
						}

						if (course != null) yield return LogEntityFoundAndReturn(course);
						if (clazz != null) yield return LogEntityFoundAndReturn(clazz);
						if (student != null) yield return LogEntityFoundAndReturn(student);
						if (instructor != null) yield return LogEntityFoundAndReturn(instructor);
						if (tutor != null) yield return LogEntityFoundAndReturn(tutor);
						if (center != null) yield return LogEntityFoundAndReturn(center);
						if (studentEnrollment != null) yield return LogEntityFoundAndReturn(studentEnrollment);
						if (instructorEnrollment != null) yield return LogEntityFoundAndReturn(instructorEnrollment);
						if (tutorEnrollment != null) yield return LogEntityFoundAndReturn(tutorEnrollment);
						if (personsGroup != null) yield return LogEntityFoundAndReturn(personsGroup);
						if (personsGroupMember != null) yield return LogEntityFoundAndReturn(personsGroupMember);
						if (courseCenterAssignment != null) yield return LogEntityFoundAndReturn(courseCenterAssignment);
					}
				}
			}
		}

		private static T LogEntityFoundAndReturn<T>(T entityFound) where T : IAccudemiaEntity
		{
			if(log.IsDebugEnabled)
			{
				log.DebugFormat("Found {0}: {1}", typeof (T).Name, entityFound);
			}
			return entityFound;
		}

		public string Describe()
		{
			return "Plain text file: " + (SourceFile ?? "<not specified>");
		}

		public string DescribeResultContents()
		{
			var text = new StringBuilder(200);

			MappedFields.Sort((f1, f2) => string.CompareOrdinal(f1.EntityType.FullName, f2.EntityType.FullName));
			Type lastEntityType = null;
			foreach (var map in MappedFields)
			{
				if (map.EntityType != lastEntityType)
				{
					lastEntityType = map.EntityType;

					// Skip the enter on the first line
					if (text.Length > 0)
					{
						text.AppendLine();
					}
					text.Append(ModelHelper.GetFriendlyEntityName(map.EntityType) + ": ");
				}
				else
				{
					text.Append(", ");
				}

				var modelProps = (ModelPropertyAttribute[]) ReflectionHelper.GetPropertyInfo(map.EntityType, map.PropertyPath)
				                                            	.GetCustomAttributes(typeof (ModelPropertyAttribute), true);

				if (modelProps.Length > 0)
				{
					text.Append(modelProps[0].DisplayName);
				}
			}

			return text.ToString();
		}

		public IEnumerable<EntityMap> GetMappedEntities()
		{
			MappedFields.Sort((f1, f2) => string.CompareOrdinal(f1.EntityType.FullName, f2.EntityType.FullName));

			Type lastEntityType = null;
			EntityMap entityMap = null;
			foreach (var map in MappedFields)
			{
				if (map.EntityType != lastEntityType || entityMap == null)
				{
					if (entityMap != null)
					{
						yield return entityMap;
					}

					lastEntityType = map.EntityType;
					entityMap = new EntityMap(map.EntityType.Name, map.EntityType.FullName);
				}
				
				entityMap.Properties.Add(map.PropertyPath);
			}

			// Return the last created entity map
			if (entityMap != null)
			{
				yield return entityMap;
			}
		}

		#endregion

		#region Private Methods

		private bool IsEntityMapped(Type entityType)
		{
			foreach (var map in MappedFields)
			{
				if (map.EntityType == entityType)
				{
					return true;
				}
			}
			return false;
		}

		private T CreateEntity<T>(CsvReader reader) where T : IAccudemiaEntity, new()
		{
			var entity = new T();
			FillEntityData(reader, entity);
			return entity;
		}

		private void FillEntityData<T>(CsvReader reader, T entityToFill) where T : IAccudemiaEntity, new()
		{
			foreach (var fieldMap in MappedFields)
			{
				if (fieldMap.EntityType == typeof(T))
				{
					// Parse the value to the correct type
					fieldMap.SetValue(entityToFill, reader[fieldMap.HeaderText]);
				}
			}
		}

		#endregion

		#region View and Config Members

		private IList<IConfigView> Views
		{
			get
			{
				if (views == null || views.Count == 0)
				{
					views = new List<IConfigView>(2) {new PlainTextConnection(), new PlainTextMappings()};
				}
				return views;
			}
		}

		public IList<IConfigView> GetConfigViews()
		{
			return Views;
		}

		public bool IsConfigViewValid(IConfigView viewToValidate)
		{
			var connectionView = viewToValidate as PlainTextConnection;
			var mappingsView = viewToValidate as PlainTextMappings;
			if (connectionView != null)
			{
				bool fileNameIsEmpty = string.IsNullOrEmpty(connectionView.FileName);
				if (fileNameIsEmpty)
				{
					MessageBox.Show("Please select a CSV file to import from.", "Select a File", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}

				bool fileExists = File.Exists(connectionView.FileName);
				if (!fileExists)
				{
					MessageBox.Show("The selected file does not exist.", "File not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}

				string extension = Path.GetExtension(connectionView.FileName);
				if (!extension.EndsWith("csv", StringComparison.InvariantCultureIgnoreCase))
				{
					string errorMessage = string.Format("Please select a valid CSV file to import from. {0} files are not supported by this data provider.", extension.ToUpperInvariant());
					MessageBox.Show(errorMessage, "File not Valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				return true;
			}
			if (mappingsView != null)
			{
				var etor = mappingsView.MappedFields.GetEnumerator();
				bool hasFieldsMapped = etor.MoveNext();
				if (!hasFieldsMapped)
				{
					MessageBox.Show("Please map at least one field.", "Map", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				return hasFieldsMapped;
			}
			return true;
		}

		public void AcceptConfigViewChanges(IConfigView viewToAcceptChanges)
		{
			var connectionView = viewToAcceptChanges as PlainTextConnection;
			var mappingsView = viewToAcceptChanges as PlainTextMappings;

			if (connectionView != null)
			{
				SourceFile = connectionView.FileName;
				TextQualifier = connectionView.TextQualifier;
				Delimiter = connectionView.Delimiter;
			}
			if (mappingsView != null)
			{
				MappedFields.Clear();
				foreach (var fieldMap in mappingsView.MappedFields)
				{
					Map(fieldMap);
				}
			}
		}

		#endregion

		#region Formatting Members

		public override string ToString()
		{
			return Name;
		}

		#endregion

		[Serializable]
		public class FieldMap
		{
			public FieldMap(string headerText, Type entityType, string propertyPath)
			{
				this.headerText = headerText;
				this.entityType = entityType;
				this.propertyPath = propertyPath;

				SetFormatter();
			}

			private void SetFormatter()
			{
				var prop = ReflectionHelper.GetPropertyInfo(EntityType, PropertyPath);
				var attribs = (ModelPropertyAttribute[])prop.GetCustomAttributes(typeof(ModelPropertyAttribute), true);

				if (attribs.Length > 0 && attribs[0].DefaultFormatterType != null)
				{
					formatter = FastActivator.CreateInstance<IDataFormatter>(attribs[0].DefaultFormatterType);
				}

				if (formatter == null)
				{
					// Guess the best data formatter
					foreach (var fter in ServiceLocator.GetAll<IDataFormatter>())
					{
						if (fter.TargetType == prop.PropertyType)
						{
							formatter = fter;
							break;
						}
					}

					// if the formatter is still not valid, error
					if (Formatter == null)
					{
						var errorText = string.Format("There's no formatter valid for property '{0}.{1}', of type '{2}'",
						                              prop.ReflectedType, prop.Name, prop.PropertyType.FullName);
						throw new NotSupportedException(errorText);
					}
				}
			}

			private readonly string headerText;
			public string HeaderText
			{
				get { return headerText; }
			}

			private readonly Type entityType;
			public Type EntityType
			{
				get { return entityType; }
			}

			private readonly string propertyPath;
			public string PropertyPath
			{
				get { return propertyPath; }
			}

			private IDataFormatter formatter;
			public IDataFormatter Formatter
			{
				get { return formatter; }
			}

			public void SetValue(IAccudemiaEntity entity, string rawValue)
			{
				if (entity == null)
				{
					throw new ArgumentNullException("entity");
				}
				ReflectionHelper.SetPropertyValue(entity, PropertyPath, Formatter.Format(rawValue));
			}
		}
	}
}