using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.Data.Repository
{
    public class Repository : IRepository
    {
        private AppDbcontext _ctx;

        public Repository(AppDbcontext ctx) => _ctx = ctx;

        public  void AddPost(Post post)
        {
            _ctx.Posts.Add(post);
            
        }

        public List<Post> GetAllPost()
        {
            return _ctx.Posts.ToList();
        }

        public Post GetPost(int id)
        {
            return _ctx.Posts.FirstOrDefault(p =>p.Id==id);
        }

        public void RemovePost(int id)
        {
            _ctx.Posts.Remove(GetPost(id));
        }

        public void updatePost(Post post)
        {
            _ctx.Posts.Update(post);
        }

       public  async Task<bool> SaveChangesAsync()
        {
            if(await _ctx.SaveChangesAsync() >0)
            {
                return true;
            }
            return false;
            throw new NotImplementedException();
        }
    }
}
