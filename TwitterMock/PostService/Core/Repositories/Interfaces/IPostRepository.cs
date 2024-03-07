using PostService.Core.Entities;

namespace PostService.Core.Repositories.Interfaces;

public interface IPostRepository
{
    
    public Task<IEnumerable<Post>> GetPosts();

    public Task AddPost(Post post);
    
    public Task UpdatePost(int postId, Post post);
    
    public Task DeletePost(int postId);

    public Task<bool> DoesPostExists(int postId);
}