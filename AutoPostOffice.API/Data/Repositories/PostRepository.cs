using AutoPostOffice.API.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AutoPostOffice.API.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private static IEnumerable<Post> Posts { get; } = new List<Post>
        {
            new Post { Number = "POST-11111", Address = "Проспект мира 10", Status = true},
            new Post { Number = "POST-22222", Address = "Нагатинская 1", Status = true},
            new Post { Number = "POST-33333", Address = "Фабричная 15", Status = false},
            new Post { Number = "POST-44444", Address = "Авиамоторная 18", Status = true},
            new Post { Number = "POST-55555", Address = "Варшавское шоссе 33", Status = false}
        };

        public Post GetPost(string postNumber)
        {
            return Posts.Where(c => c.Number == postNumber).FirstOrDefault();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return Posts;
        }
    }
}
