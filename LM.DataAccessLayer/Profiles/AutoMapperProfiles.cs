/*
 * Defining the AutoMapper Profiles
 * Author: Deep Mehta
 */
namespace LMS.DataAccessLayer.Profiles
{
    using AutoMapper;
    using LMS.DataAccessLayer.Entities;
    using LMS.SharedFiles.DTOs;
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>();
            CreateMap<Library, LibraryDTO>();
            CreateMap<Location, LibraryDTO>();
            CreateMap<Location, LocationDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<PostDTO, Post>();
            CreateMap<Post, PostDTO>();
        }
    }
}
