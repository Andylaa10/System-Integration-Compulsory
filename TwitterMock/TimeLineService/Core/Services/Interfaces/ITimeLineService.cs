using TimeLineService.Core.Entities;
using TimeLineService.Core.Services.DTO;

namespace TimeLineService.Core.Services.Interfaces;

public interface ITimeLineService
{
    public Task<IEnumerable<TimeLine>> GetTimeLines();
    
    public Task AddToTimeLine(AddToTimeLineDto dto);
    
    public Task DeleteFromTimeLine(int postId);
}