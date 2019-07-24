using System;
using Xunit;

namespace GradeBook.Tests
{
	public class TypeTest
	{
		[Fact]
		public void ValueTypesCanAlsoPassByReference()
		{
			var x = GetInt();
			SetInt(ref x);

			Assert.Equal(41, x);
		}

		private void SetInt(ref int z)
		{
			z = 41;
		}
		public void ValueTypesAlsoPassByValue()
		{
			var x = GetInt();
			SetInt(x);

			Assert.Equal(3, x);
		}

		private void SetInt(int z)
		{
			z = 41;
		}

		private int GetInt()
		{
			return 3;
		}

		[Fact]
		public void CsharpCanPassByReference()
		{
			var book1 = GetBook("Book 1");
			GetBookSetName(out book1, "New Name");

			Assert.Equal("New Name", book1.Name);
		}

		void GetBookSetName(out InMemoryBook book, string newName)
		{
			book = new InMemoryBook(newName);
		}

		[Fact]
		public void CsharpIsPassByValue()
		{
			var book1 = GetBook("Book 1");
			GetBookSetName(book1, "New Name");

			Assert.Equal("Book 1", book1.Name);
		}

		void GetBookSetName(InMemoryBook book, string newName)
		{
			book = new InMemoryBook(newName);
		}

		[Fact]
		public void CanSetNameFromReference()
		{
			var book1 = GetBook("Book 1");
			SetName(book1, "New Name");

			Assert.Equal("New Name", book1.Name);
		}
		void SetName(InMemoryBook book, string newName)
		{
			book.Name = newName;
		}

		[Fact]
		public void StringsBehaveLikeValueTypes()
		{
			string name = "Scott";
			string upperName = MakeUppercase(name);

			Assert.Equal("Scott", name);
			Assert.Equal("SCOTT", upperName);
		}

		private string MakeUppercase(string arg)
		{
			return arg.ToUpper();
		}

		[Fact]
		public void GetBookReturnsDiffObjs()
		{
			var book1 = GetBook("Book 1");
			var book2 = GetBook("Book 2");

			Assert.Equal("Book 1", book1.Name);
			Assert.Equal("Book 2", book2.Name);
			Assert.NotSame(book1, book2);
		}

		[Fact]
		public void TwoVarsCanReferenceSameObj()
		{
			var book1 = GetBook("Book 1");
			var book2 = book1;

			Assert.Same(book1, book2);
			Assert.True(Object.ReferenceEquals(book1, book2));
		}

		InMemoryBook GetBook(string name)
		{
			return new InMemoryBook(name);
		}
	}
}
