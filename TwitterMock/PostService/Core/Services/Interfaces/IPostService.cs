using PostService.Core.Entities;
using PostService.Core.Services.DTOs;

namespace PostService.Core.Services.Interfaces
{
    public interface IPostService
    {
        public Task<IEnumerable<Post>> GetPosts();
        
        public Task<Post> GetPostById(int postId);
        
        public Task<Post> AddPost(AddPostDTO comment);
        
        public Task UpdatePost(int postId, UpdatePostDTO post);
        
        public Task DeletePost(int postId);
    }
}