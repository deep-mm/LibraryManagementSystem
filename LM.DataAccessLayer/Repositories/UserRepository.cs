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
        private static string className = "UserRespository";

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
                throw new Exception(className+ "/Commit(): Error occured while commiting to database");
            }
        }

        /*
         * Get a user by its userId: await GetUserById(userId)
         */
        public async Task<UserDTO> GetUserById(int userId)
        {
            if (userId != 0)
            {
                User user = await readDBContext.users.FindAsync(userId);
                if (user != null)
                {
                    return mapper.Map<UserDTO>(user);
                }
                else
                {
                    throw new NullReferenceException(className+ $"/GetUserById(): The user for id: {userId} was not found");
                }
            }
            else
            {
                throw new ArgumentNullException(className+ "/GetUserById(): The userId parameter received is null");
            }
        }

        /*
         * Get a user by its username: await GetUserByName(username)
         */
        public async Task<UserDTO> GetUserByName(string username)
        {
            if (username != null)
            {
                User user = readDBContext.users.FirstOrDefault(u => u.userName == username);
                if (user != null)
                {
                    return mapper.Map<UserDTO>(user);
                }
                else
                {
                    throw new NullReferenceException(className+ $"/GetUserByName(): The user for name {username} was not found");
                }
            }
            else
            {
                throw new ArgumentNullException(className + "/GetUserByName():The username parameter received is null");
            }
        }
        /*
         * Add a new user to the database
         */
        public async Task<bool> AddNewUser(UserDTO userDTO)
        {
            if (userDTO != null)
            {
                User user = mapper.Map<User>(userDTO);
                await readDBContext.users.AddAsync(user);
                await Commit();
                return true;
            }
            else
            {
                throw new ArgumentNullException(className+ "/AddNewUser(): The userDTO object parameter received is null");
            }
        }

        /*
         * Get all the BookOrderDTOs of books that a user has checked out: awaut UserOrderHistory(userId)
         */
        public async Task<IEnumerable<BookOrdersDTO>> UserOrderHistory(int userId)
        {
            if (userId != 0)
            {
                IEnumerable<UserBookAssociation> userBookAssociations = readDBContext.userBookAssociations.Where(u => u.userId == userId);

                if (userBookAssociations != null)
                {
                    List<BookOrdersDTO> bookOrdersDTOs = new List<BookOrdersDTO>();

                    foreach (var uba in userBookAssociations)
                    {
                        BookLibraryAssociation bla = await readDBContext.bookLibraryAssociations.FindAsync(uba.BookLibraryAssociationId);
                        if (bla != null)
                        {
                            Book b = await readDBContext.books.FindAsync(bla.bookId);

                            if (b != null)
                            {
                                BookOrdersDTO bookOrdersDTO = new BookOrdersDTO();
                                bookOrdersDTO.BookName = b.title;
                                bookOrdersDTO.DueDate = uba.DueDate;
                                bookOrdersDTO.LibraryId = bla.libraryId;
                                bookOrdersDTO.userBookAssociationId = b.bookId;

                                bookOrdersDTOs.Add(bookOrdersDTO);
                            }
                            else
                            {
                                throw new NullReferenceException(className+ $"/UserOrderHistory(): The book for id: {bla.bookId} was not found");
                            }
                        }
                        else
                        {
                            throw new NullReferenceException(className + $"/UserOrderHistory(): The bookLibraryAssociation for id: {uba.BookLibraryAssociationId} was not found");
                        }
                    }

                    return bookOrdersDTOs;
                }
                else
                {
                    throw new NullReferenceException(className + $"/UserOrderHistory(): The userBookAssociations for userId: {userId} was not found");
                }
            }
            else
            {
                throw new ArgumentNullException(className + "/UserOrderHistory(): The userId parameter received is null");
            }

        }
    }
}
