using Microsoft.EntityFrameworkCore;
using TimeLineService.Core.Entities;
using TimeLineService.Core.Helper;
using TimeLineService.Core.Repositories.Interfaces;

namespace TimeLineService.Core.Repositories;

public class TimeLineRepository : ITimeLineRepository
{
    private readonly DatabaseContext _context;

    public TimeLineRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TimeLine>> GetTimeLines()
    {
        return await _context.TimeLines.ToListAsync();
    }

    public async Task AddToTimeLine(TimeLine timeLine)
    {
        await _context.TimeLines.AddAsync(timeLine);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFromTimeLine(int postId)
    {
        var postsToDelete = await _context.TimeLines.FirstOrDefaultAsync(p => p.PostId == postId);

        if (postsToDelete == null) throw new KeyNotFoundException($"No post with the id of {postId}");
        
        _context.TimeLines.Remove(postsToDelete);
        await _context.SaveChangesAsync();
    }
}