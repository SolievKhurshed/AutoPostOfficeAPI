using AutoMapper;
using AutoPostOffice.API.Data.Entities;
using AutoPostOffice.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
