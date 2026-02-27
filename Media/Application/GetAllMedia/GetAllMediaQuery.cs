using Shared.Model;

namespace Media.Application.GetAllMedia
{
    public record GetAllMediaQuery : BaseFilterCriteria
    {
        public FilterField<Guid[]>? Ids { get; set; }
        public FilterField<string>? Title { get; set; }
        public FilterField<string>? Description { get; set; }
        public FilterField<DateTime>? CreateDate { get; set; }
        public FilterField<DateTime>? UpdateDate { get; set; }
        public FilterField<string>? FileName { get; set; }
        public FilterField<string>? OriginalFileName { get; set; }
        public FilterField<double>? SizeInKb { get; set; }
        public FilterField<double>? Width { get; set; }
        public FilterField<double>? Height { get; set; }
    }
}
