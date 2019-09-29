using LMS.DataAccessLayer.Repositories;
using System;
using Xunit;

namespace LMS.UnitTest
{
    public class BookRepositoryTests
    {
        [Fact]
        [Theory]
        [InlineData("")]
        public void AddNewBookSuccess(string name)
        {
            //var sut = new BookRepository();

            //r result = sut.exexute();
            //Assert.Equal(result.A);
        }
    }
}
