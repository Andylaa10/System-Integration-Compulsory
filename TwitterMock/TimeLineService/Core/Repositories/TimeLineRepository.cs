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
        var postsToDelete = await _context.TimeLines.Where(t => t.PostId == postId).ToListAsync();
        _context.TimeLines.RemoveRange(postsToDelete);
        await _context.SaveChangesAsync();
    }
}