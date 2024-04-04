using TimeLineService.Core.Entities;

namespace TimeLineService.Core.Repositories.Interfaces;

public interface ITimeLineRepository
{
    /// <summary>
    /// Retrieves timeline from db
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<TimeLine>> GetTimeLines();

    /// <summary>
    /// Add to post to timeline
    /// </summary>
    /// <param name="timeLine"></param>
    /// <returns></returns>
    public Task AddToTimeLine(TimeLine timeLine);

    /// <summary>
    /// Delete post from timeline
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public Task DeleteFromTimeLine(int postId);
}