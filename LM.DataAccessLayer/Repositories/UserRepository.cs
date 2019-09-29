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
    using LMS.SharedFiles.DTOs;

    public class UserRepository: IUserRepository
    {
        private ReadDBContext readDBContext;
        private IMapper mapper;

        public UserRepository(ReadDBContext readDBContext, IMapper mapper)
        {
            this.readDBContext = readDBContext;
            this.mapper = mapper;
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public async Task Commit()
        {
            try
            {
                await readDBContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                
            }
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
