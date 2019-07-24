using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
	public delegate void GradeAddedDelegate(object sender, EventArgs args);

	public class NamedObject
	{
		public string Name { get; protected set; }
		public NamedObject(string name)
		{
			Name = name;
		}
	}

	public interface IBook
	{
		void AddGrade(double grade);
		Statistics GetStatistics();
		string Name { get; }
		event GradeAddedDelegate GradeAdded;
	}
	public abstract class Book : NamedObject, IBook
	{
		public Book(string name) : base(name)
		{
		}

		public abstract event GradeAddedDelegate GradeAdded;

		public abstract void AddGrade(double grade);

		public abstract Statistics GetStatistics();
	}

	public class InMemoryBook : Book
	{
		public override event GradeAddedDelegate GradeAdded;
		private List<double> grades;

		public InMemoryBook(string name) : base(name)
		{
			Name = name;
			grades = new List<double>();
		}

		public void AddGrade(char letter)
		{
			switch (letter)
			{
				case 'A':
					AddGrade(90);
					break;
				case 'B':
					AddGrade(80);
					break;
				case 'C':
					AddGrade(70);
					break;
				default:
					break;
			}
		}
		public override void AddGrade(double grade)
		{
			if (grade <= 100 && grade >= 0)
			{
				grades.Add(grade);
				if (GradeAdded != null)
				{
					GradeAdded(this, new EventArgs());
				}
			}
			else
			{
				throw new ArgumentException($"Invalid {nameof(grade)}!");
			}
		}

		public override Statistics GetStatistics()
		{
			return new Statistics(grades);
		}
	}

	public class DiskBook : Book
	{
		public override event GradeAddedDelegate GradeAdded;
		private List<double> grades;

		public DiskBook(string name) : base(name)
		{
			grades = new List<double>();
		}

		public override void AddGrade(double grade)
		{
			if (grade <= 100 && grade >= 0)
			{
				try
				{
					using (var writer = File.AppendText($"./bin/docs/books/{Name}.txt"))
					{
						writer.WriteLine(grade);
						if (GradeAdded != null)
						{
							GradeAdded(this, new EventArgs());
						}
					}
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
			else
			{
				throw new ArgumentException($"Invalid {nameof(grade)}!");
			}
		}

		public override Statistics GetStatistics()
		{
			//populate grades from file
			using (var reader = File.OpenText($"./bin/docs/books/{Name}.txt"))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					var lineGrade = double.Parse(line);
					grades.Add(lineGrade);
				}
			}
			var stats = new Statistics(grades);
			System.Console.WriteLine(stats);
			return stats;
		}
	}
}
