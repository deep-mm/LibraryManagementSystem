using AutoMapper;
using LMS.APILayer.Controllers;
using LMS.BusinessLogic.Services;
using LMS.DataAccessLayer.DatabaseContext;
using LMS.DataAccessLayer.Repositories;
using LMS.SharedFiles.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
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
        private Mock<IConfiguration> configuration;
        private Mock<IBooksBusinessLogic> booksBusinessLogic;
        private BooksController booksController;


        [TestInitialize]
        public void BeforeEveryTest()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.booksBusinessLogic = this.mockRepository.Create<IBooksBusinessLogic>();
            this.configuration = this.mockRepository.Create<IConfiguration>();
            this.booksController = new BooksController(booksBusinessLogic.Object, configuration.Object);
        }

        [TestCleanup]
        public void CleanUpEnd()
        {
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public async Task GetAllBooksSuccessFlow()
        {
            booksBusinessLogic.Setup(x => x.GetBookByName(It.IsAny<string>(),It.IsAny<int>())).Returns(ExecuteBooksTaskSuccess());

            var result = await booksController.GetAllBooks($"{It.IsAny<int>()}");

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }


        private async Task<IEnumerable<BookDTO>> ExecuteBooksTaskSuccess()
        {
            return await Task.FromResult(new List<BookDTO>() { new BookDTO(), new BookDTO()});
        }

        [TestMethod]
        public async Task GetAllBooksFailureFlow()
        {
            booksBusinessLogic.Setup(x => x.GetBookByName(It.IsAny<string>(), It.IsAny<int>())).Returns(ExecuteBooksTaskFailure());
         
            IActionResult result = await booksController.GetAllBooks($"{It.IsAny<int>()}");

            var badResult = result as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, badResult.StatusCode);
        }


        private async Task<IEnumerable<BookDTO>> ExecuteBooksTaskFailure()
        {
            IEnumerable<BookDTO> books = null;
            return await Task.FromResult(books);
        }

        [TestMethod]
        public async Task GetBookByNameSuccessFlow()
        {
            booksBusinessLogic.Setup(x => x.GetBookByName(It.IsAny<string>(), It.IsAny<int>())).Returns(ExecuteBooksTaskSuccess());

            IActionResult result = await booksController.GetBookByName(It.IsAny<string>(), $"{It.IsAny<int>()}");

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task GetBookByNameFailureFlow()
        {
            booksBusinessLogic.Setup(x => x.GetBookByName(It.IsAny<string>(), It.IsAny<int>())).Returns(ExecuteBooksTaskFailure());

            IActionResult result = await booksController.GetBookByName(It.IsAny<string>(), $"{It.IsAny<int>()}");

            var badRequest = result as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [TestMethod]
        public async Task AddBookSuccessFlow()
        {
            //booksBusinessLogic.Setup(x => x.AddNewBook(It.IsAny<BookDTO>(), It.IsAny<int>())).Returns(ExecuteBooksTaskFailure());

            IActionResult result = await booksController.GetBookByName(It.IsAny<string>(), $"{It.IsAny<int>()}");

            var badRequest = result as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }
    }
}
