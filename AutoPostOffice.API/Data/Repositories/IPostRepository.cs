using AutoPostOffice.API.Data.Entities;
using System.Collections.Generic;

namespace AutoPostOffice.API.Data.Repositories
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAllPosts();
        Post GetPost(string postNumber);
    }
}
