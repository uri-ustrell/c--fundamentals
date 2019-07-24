using System;
using System.Collections.Generic;

namespace GradeBook
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("enter name of grade book student");
			var name = Console.ReadLine();
			IBook book = new DiskBook(name);
			book.GradeAdded += OnGradeAdded;
			EnterGrades(name, book);

			var stats = book.GetStatistics();

			System.Console.WriteLine($"For the book named: {book.Name}");
			Console.WriteLine($"The average grade is {stats.Average:N1}");
			Console.WriteLine($"The highest grade {stats.High:N1}");
			Console.WriteLine($"The lowest {stats.Low:N1}");
			Console.WriteLine($"The letter grade is {stats.Letter}");
		}

		private static void EnterGrades(string name, IBook book)
		{
			for (int i = 0; i < 50; i++)
			{
				Console.WriteLine($"Enter grade for {name}'s Grade Book\n(or enter 'q' to see stats)");
				var input = Console.ReadLine();
				if (input == "q")
				{
					return;
				}

				try
				{
					var grade = double.Parse(input);
					book.AddGrade(grade);
				}
				catch (ArgumentException ex)
				{
					Console.WriteLine(ex.Message);
				}
				catch (FormatException ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}

		static void OnGradeAdded(object sender, EventArgs e)
		{
			Console.WriteLine("****A grade was added****");
		}
	}
}
