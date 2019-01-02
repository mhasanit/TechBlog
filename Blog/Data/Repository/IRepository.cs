using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blog.Data.Repository
{
    public interface  IRepository
    {
        Post  GetPost(int id);
        List<Post> GetAllPost();
        void AddPost(Post post);
        void RemovePost(int id);
        void updatePost(Post post);
        Task <bool> SaveChangesAsync();

    }
}
