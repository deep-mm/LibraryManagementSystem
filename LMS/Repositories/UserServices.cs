/*
 * This contains implementation of all the functions performed to get details of a user or to add a new user
 * Author: Deep Mehta
 */
namespace LMS.DataAccessLayer.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using LMS.DataAccessLayer.DatabaseContext;
    using LMS.DataAccessLayer.Entities;
    using LMS.DataAccessLayer.Profiles;
    using LMS.DataAccessLayer.Repositories;
    using LMS.SharedFiles.DTOs;
    using Microsoft.EntityFrameworkCore;

    public class UserServices: IUserRepository
    {
        private ReadDBContext readDBContext;
        private IMapper mapper;

        public UserServices()
        {
            Initialize();
        }

        public void Initialize()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReadDBContext>();
            string conn = "Server=tcp:tone-app.database.windows.net,1433;Initial Catalog=LMS;Persist Security Info=False;User ID=demeht;Password=India@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(conn);

            ReadDBContext context = new ReadDBContext(optionsBuilder.Options);
            this.readDBContext = context;


            var config = new MapperConfiguration(cfg =>
                cfg.AddProfile(new AutoMapperProfiles())
            );

            this.mapper = config.CreateMapper();
        }

        public async Task Commit()
        {
            await readDBContext.SaveChangesAsync();
        }

        /*
         * Get a user by its userId: await GetUserById(userId)
         */
        public async Task<UserDTO> GetUserById(int userId)
        {
            User user = await readDBContext.users.FindAsync(userId);
            if (user != null)
            {
                return mapper.Map<UserDTO>(user);
            }
            return null;
        }

        /*
         * Get a user by its username: await GetUserByName(username)
         */
        public async Task<UserDTO> GetUserByName(string username)
        {
            User user = readDBContext.users.FirstOrDefault(u => u.userName == username);
            if (user != null)
            {
                return mapper.Map<UserDTO>(user);
            }
            return null;
        }
        /*
         * Add a new user to the database
         */
        public async Task<bool> AddNewUser(UserDTO userDTO)
        {
            User user = mapper.Map<User>(userDTO);
            await readDBContext.users.AddAsync(user);
            await Commit();
            return true;
        }

        /*
         * Get all the BookOrderDTOs of books that a user has checked out: awaut UserOrderHistory(userId)
         */
        public async Task<IEnumerable<BookOrdersDTO>> UserOrderHistory(int userId)
        {
            IEnumerable<UserBookAssociation> userBookAssociations = readDBContext.userBookAssociations.Where(u => u.userId == userId);
            List<BookOrdersDTO> bookOrdersDTOs = new List<BookOrdersDTO>();

            foreach (var uba in userBookAssociations)
            {
                BookLibraryAssociation bla = await readDBContext.bookLibraryAssociations.FindAsync(uba.BookLibraryAssociationId);
                if (bla != null)
                {
                    Book b = await readDBContext.books.FindAsync(bla.bookId);

                    BookOrdersDTO bookOrdersDTO = new BookOrdersDTO();
                    bookOrdersDTO.BookName = b.title;
                    bookOrdersDTO.DueDate = uba.DueDate;
                    bookOrdersDTO.LibraryId = bla.libraryId;
                    bookOrdersDTO.userBookAssociationId = b.bookId;

                    bookOrdersDTOs.Add(bookOrdersDTO);
                }
                else
                {
                    //Log the exception
                    throw new Exception();
                }
            }

            return bookOrdersDTOs;
        }
    }
}
