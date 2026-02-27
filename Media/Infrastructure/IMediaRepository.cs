using Media.Application.GetAllMedia;
using Media.Entity;

namespace Media.Infrastructure
{
    public interface IMediaRepository
    {
        Task AddAsync(Media.Entity.Media media);
        Task UpdateAsync(Media.Entity.Media media);
        Task<List<Media.Entity.Media>> GetAllAsync(GetAllMediaQuery criteria);
        Task<Media.Entity.Media?> GetByIdAsync(Guid id);
        Task DeleteAsync(Media.Entity.Media media);
    }
}
