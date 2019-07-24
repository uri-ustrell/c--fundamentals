using System;
using Xunit;

namespace GradeBook.Tests
{
	public class BookTest
	{
		[Fact]
		public void BookCalculatesStats()
		{
			//arrange
			var book = new InMemoryBook("");
			book.AddGrade(89.1);
			book.AddGrade(90.5);
			book.AddGrade(77.3);

			//act
			var resultTest = book.GetStatistics();

			//assert
			Assert.Equal(77.3, resultTest.Low, 1);
			Assert.Equal(90.5, resultTest.High, 1);
			Assert.Equal(85.6, resultTest.Average, 1);
			Assert.Equal('B', resultTest.Letter);
		}
	}
}
