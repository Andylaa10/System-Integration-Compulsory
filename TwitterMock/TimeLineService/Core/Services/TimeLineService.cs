using AutoMapper;
using TimeLineService.Core.Entities;
using TimeLineService.Core.Repositories.Interfaces;
using TimeLineService.Core.Services.DTO;
using TimeLineService.Core.Services.Interfaces;

namespace TimeLineService.Core.Services;

public class TimeLineService : ITimeLineService
{
    private readonly ITimeLineRepository _timeLineRepository;
    private readonly IMapper _mapper;

    public TimeLineService(ITimeLineRepository timeLineRepository, IMapper mapper)
    {
        _timeLineRepository = timeLineRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TimeLine>> GetTimeLines()
    {
        return await _timeLineRepository.GetTimeLines();
    }

    public async Task AddToTimeLine(AddToTimeLineDto dto)
    {
        if (dto.PostId <= 0) throw new ArgumentException("Post id cannot be equal or less than 0");

        await _timeLineRepository.AddToTimeLine(_mapper.Map<TimeLine>(dto));
    }

    public async Task DeleteFromTimeLine(int postId)
    {
        if (postId <= 0) throw new ArgumentException("Post id cannot be equal or less than 0");
        await _timeLineRepository.DeleteFromTimeLine(postId);
    }
}