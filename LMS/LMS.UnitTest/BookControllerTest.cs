using AutoMapper;
using LMS.APILayer.Controllers;
using LMS.BusinessLogic.Services;
using LMS.DataAccessLayer.DatabaseContext;
using LMS.SharedFiles.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace LMS.UnitTest
{
    [TestClass]
    public class BooksTest
    {
        private MockRepository mockRepository;

        private Mock<ReadDBContext> readDBContext;
        private Mock<IMapper> mapper;
        private Mock<IConfiguration> configuration;

        private Mock<BooksBusinessLogic> booksBusinessLogic;


        [TestInitialize]
        public void BeforeEveryTest()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.readDBContext = this.mockRepository.Create<ReadDBContext>();
            this.booksBusinessLogic = this.mockRepository.Create<BooksBusinessLogic>();
            this.mapper = this.mockRepository.Create<IMapper>();
            this.configuration = this.mockRepository.Create<IConfiguration>();
        }

        [TestMethod]
        public void GetAllBooksSuccessFlow()
        {
            booksBusinessLogic.Setup(x => x.GetBookByName("")).Returns(ExecuteBooksTask());
            BooksController booksController = new BooksController(readDBContext.Object, mapper.Object, configuration.Object);

            var result = booksController.GetAllBooks();

            //var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);
        }


        private async Task<IEnumerable<BookDTO>> ExecuteBooksTask()
        {
            return await Task.FromResult(new List<BookDTO>() { new BookDTO(), new BookDTO()});
            //create controller object
            //call the function
            //Assert.is
        }
    }
}
