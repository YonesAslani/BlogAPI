using DBManager.Data;
using DBManager.DbModels;
using DBManager.IServices;
using DBManager.Pagging.PaggingClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Services
{
    public class PostService : IPostService
    {
        private readonly Context _db;
        public PostService(Context db)
        {
            _db = db;
        }
        public async Task<Post> AddPostAsync(Post post)
        {
            try
            {
                await _db.Posts.AddAsync(post);
                await _db.SaveChangesAsync();
                return post;
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task DeletePostAsync(int id)
        {
            _db.Posts.Remove(new Post { Id=id});
            await _db.SaveChangesAsync();
        }

        public async Task<PaggingPost> DoPaggingPostsAsync(int pagesize, int pageid, string title, string content, string author)
        {
            var posts = await GetAllPostsAsync();
            if (title != null)
            {
                posts=posts.Where(w=>w.Title.Contains(title)).ToList();
            }
            if (content != null)
            {
                posts=posts.Where(w=>w.Content.Contains(content)).ToList();
            }
            if (author != null)
            {
                posts = posts.Where(w => w.Author!=null && w.Author.Contains(author)).ToList();
            }
            int skip = (pageid - 1) * pagesize;
            var model = new PaggingPost()
            {
                Posts = posts.Skip(skip).Take(pagesize).ToList(),
                SearchedContent = content,
                SearchedAuthor = author,
                SearchedTitle = title
            };

            model.GeneratPagging(posts.AsQueryable(), pagesize, pageid);
            return model;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _db.Posts.ToListAsync();
        }

        public async Task<Post> GetPostAsync(int id)
        {
            var result = await _db.Posts.FindAsync(id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<bool> IsExistPostAsync(int id)
        {
            var result = await _db.Posts.AnyAsync(a => a.Id == id);
            return result;
        }

        public async Task<Post> UpdatePostAsync(Post post)
        {
            _db.Entry(post).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return post;
        }
    }
}
