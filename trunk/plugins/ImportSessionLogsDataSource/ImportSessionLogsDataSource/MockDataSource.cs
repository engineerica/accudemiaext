namespace ImportSessionLogsDataSource
{
	using System;
	using System.Collections.Generic;
	using AccudemiaDataX.Core;
	using AccudemiaDataX.Core.Model;
	using AccudemiaDataX.Core.Model.Base;

	[Serializable]
	public class MockDataSource : IDataSource
	{
		private static IList<IConfigView> Views
		{
			get { return new List<IConfigView>(0); }
		}

		public IEnumerable<IAccudemiaEntity> GetEntities()
		{
			// This example shows how to create a sample group and add 2 students to it.
			// You can play with other entities located in the AccudemiaDataX.Core.Model namespace.
			
			var baseTime = new DateTime(2011, 2, 2, 15, 10, 22);

			var student1 = new Student
			         	{
			         		FirstName = "John",
			         		MiddleName = "",
			         		LastName = "Williams",
			         		PersonNumber = "444-44-4444"
			         	};

			var tutor = new Tutor
			            	{
			            		FirstName = "Matthew",
			            		MiddleName = "",
			            		LastName = "Perry",
			            		PersonNumber = "123-12-1234"
			            	};
			var instructor = new Instructor
			                 	{
			                 		FirstName = "Edgard",
			                 		MiddleName = "",
			                 		LastName = "Cox",
			                 		PersonNumber = "456-78-9875"
			                 	};

			var course1 = new Course { Name = "Math" };
			var course2 = new Course { Name = "Algebra" };
			var center = new Center {Name = "Writing Center"};
			var service1 = new Service {Name = "Tutoring", BelongsTo = center};
			var service2 = new Service { Name = "Mentoring", BelongsTo = center };

			var log1 = new SignInLog
			{
				Person = student1,
				Center = center,
				Comments = "Some sample comments",
				Instructor = instructor,
				Tutor = tutor,
				SignInTime = baseTime,
				SignOutTime = baseTime.AddMinutes(25),
			};

			var log2 = new SignInLog
			{
				Person = tutor,
				Center = center,
				SignInTime = baseTime.AddMilliseconds(-3),
				SignOutTime = baseTime.AddMinutes(50),
			};


			// Entities MUST be returned in a logical order to avoid foreign-key constrains errors.
			// The same entity can be returned multiple times because the engine will remove duplicates.
			yield return student1;
			yield return tutor;
			yield return instructor;
			yield return course1;
			yield return course2;
			yield return center;
			yield return service1;
			yield return service2;

			yield return log1;
			yield return log1.AddCourse(course1);
			yield return log1.AddService(service2);

			yield return log2;
			yield return log2.AddCourse(course1);
			yield return log2.AddCourse(course2);
			yield return log2.AddService(service1);
			yield return log2.AddService(service2);
		}

		public IList<IConfigView> GetConfigViews()
		{
			return Views;
		}

		public bool IsConfigViewValid(IConfigView viewToValidate)
		{
			return true;
		}

		public void AcceptConfigViewChanges(IConfigView viewToAcceptChanges)
		{
		}

		public string Describe()
		{
			return "Import Sample Session Logs";
		}

		public string DescribeResultContents()
		{
			return "Sample session logs";
		}

		public IEnumerable<EntityMap> GetMappedEntities()
		{
			// IMPORTANT!
			// You must specify which fields are you uploading. These are used to match and update
			// the records in the server. It's required because you might want to upload only some
			// fields and keep others unchanged. 
			// As this information is used to match the records on the server, writing it wrong
			// can lead into duplicated information.
			// If the record is not found in the server and must be created, all the fields
			// will be used because otherwise they would default to null.

			var student = new EntityMap(typeof (Student).Name, typeof (Student).FullName);
			student.Properties.Add("PersonNumber");
			student.Properties.Add("FirstName");
			student.Properties.Add("MiddleName");
			student.Properties.Add("LastName");
			yield return student;

            var course = new EntityMap(typeof(Course).Name, typeof(Course).FullName);
			course.Properties.Add("Name");
			yield return course;

			var center = new EntityMap(typeof(Center).Name, typeof(Center).FullName);
			center.Properties.Add("Name");
			yield return center;

			var instructor = new EntityMap(typeof(Instructor).Name, typeof(Instructor).FullName);
			instructor.Properties.Add("PersonNumber");
			instructor.Properties.Add("FirstName");
			instructor.Properties.Add("MiddleName");
			instructor.Properties.Add("LastName");
			yield return instructor;

			var tutor = new EntityMap(typeof(Tutor).Name, typeof(Tutor).FullName);
			tutor.Properties.Add("PersonNumber");
			tutor.Properties.Add("FirstName");
			tutor.Properties.Add("MiddleName");
			tutor.Properties.Add("LastName");
			yield return tutor;

			var service = new EntityMap(typeof(Service).Name, typeof(Service).FullName);
			service.Properties.Add("Name");
			service.Properties.Add("BelongsTo");
			yield return service;

			var signInLog = new EntityMap(typeof(SignInLog).Name, typeof(SignInLog).FullName);
			signInLog.Properties.Add("Person");
			signInLog.Properties.Add("SignInTime");
			signInLog.Properties.Add("SignOutTime");
			signInLog.Properties.Add("Comments");
			signInLog.Properties.Add("Course");
			signInLog.Properties.Add("Center");
			signInLog.Properties.Add("Instructor");
			signInLog.Properties.Add("Tutor");
			signInLog.Properties.Add("Services");
			yield return signInLog;
		}

		public string Name
		{
			get { return "Import Sample Session Logs"; }
		}

		public string Description
		{
			get { return "Sample Session Logs"; }
		}

		public override string ToString()
		{
			return Name;
		}
	}
}