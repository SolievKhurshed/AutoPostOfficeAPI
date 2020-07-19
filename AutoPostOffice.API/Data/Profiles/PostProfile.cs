using AutoMapper;
using AutoPostOffice.API.Data.Entities;
using AutoPostOffice.API.Models;

namespace AutoPostOffice.API.Data.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostModel>();
        }
    }
}
