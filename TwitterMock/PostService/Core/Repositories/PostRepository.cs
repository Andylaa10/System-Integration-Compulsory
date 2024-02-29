using Microsoft.EntityFrameworkCore;
using PostService.Core.Entities;
using PostService.Core.Helper;
using PostService.Core.Repositories.Interfaces;

namespace PostService.Core.Repositories;

public class PostRepository : IPostRepository
{
    private readonly DatabaseContext _context;

    public PostRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Post>> GetPosts()
    {
        return await _context.Posts.ToListAsync();
    }

    public async Task AddPost(Post post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePost(int postId, Post updatedPost)
    {
        var postToUpdate = await _context.Posts.FirstOrDefaultAsync(p => p.Id == updatedPost.Id);
        
        if (postId != updatedPost.Id)
        {
            throw new ArgumentException("The ids do not match");
        }
        
        postToUpdate.Content = updatedPost.Content;
        postToUpdate.UpdatedAt = DateTime.UtcNow;
        _context.Posts.Update(postToUpdate);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePost(int postId)
    {
        var postToDelete = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        _context.Posts.Remove(postToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesPostExists(int postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        if (post != null || post != default)
        {
            return true;
        }

        return false;
    }
    
}