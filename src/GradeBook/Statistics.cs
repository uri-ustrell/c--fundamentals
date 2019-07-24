using System;
using System.Collections.Generic;

namespace GradeBook
{
	public class Statistics
	{
		private double high;
		private double low;
		public double Average { get; set; }
		public double High
		{
			get { return high; }
			set { high = Math.Max(value, high); }
		}
		public double Low
		{
			get { return low; }
			set { low = Math.Min(value, low); }
		}
		public char Letter { get; set; }
		public Statistics(List<double> grades)
		{
			Average = 0.0;
			High = double.MinValue;
			Low = double.MaxValue;

			Calculate(grades);
		}

		private void Calculate(List<double> grades)
		{
			double sum = 0.0;
			foreach (double grade in grades)
			{
				High = grade;
				Low = grade;
				sum += grade;
			}

			Average = sum / grades.Count;

			switch (Average)
			{
				case var d when d >= 90.0:
					Letter = 'A';
					break;
				case var d when d >= 80.0:
					Letter = 'B';
					break;
				case var d when d >= 70.0:
					Letter = 'C';
					break;
				case var d when d >= 60.0:
					Letter = 'D';
					break;
				case var d when d >= 50.0:
					Letter = 'E';
					break;
				case var d when d < 50.0:
					Letter = 'F';
					break;
				default:
					break;
			}

		}
	}
}