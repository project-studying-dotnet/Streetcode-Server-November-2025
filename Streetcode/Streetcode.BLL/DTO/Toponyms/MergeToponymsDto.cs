namespace Streetcode.BLL.DTO.Toponyms
{
    public class MergeToponymsDto
    {
        public int TargetToponymId { get; set; }
        public List<int> SourceToponymIds { get; set; } = new();
    }
}
