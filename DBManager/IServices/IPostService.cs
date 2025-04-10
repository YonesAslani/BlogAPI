using DBManager.DbModels;
using DBManager.Pagging.PaggingClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.IServices
{
    public interface IPostService
    {
        Task<Post> AddPostAsync(Post post);

        Task<Post> GetPostAsync(int id);

        Task<List<Post>> GetAllPostsAsync();

        Task<Post> UpdatePostAsync(Post post);

        Task<bool> IsExistPostAsync(int id);

        Task DeletePostAsync(int id);

        Task<PaggingPost> DoPaggingPostsAsync(int pagesize, int pageid, string title, string content, string author);
    }
}
