using Streetcode.DAL.Enums;

namespace Streetcode.BLL.DTO.Timeline;

public class UpdateTimelineItemDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public DateViewPattern DateViewPattern { get; set; }
    public int StreetcodeId { get; set; }
    public List<int> HistoricalContextIds { get; set; } = new();
}
