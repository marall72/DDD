using Media.Infrastructure;
using Shared.Model;

namespace Media.Application.GetAllMedia
{
    public class GetAllMediaHandler
    {
        private readonly IMediaRepository _repo;

        public GetAllMediaHandler(IMediaRepository repo)
        {
            _repo = repo;
        }

        //TODO: add paging and sorting
        public async Task<List<Media.Entity.Media>> Handle(GetAllMediaQuery query)
        {
            return await _repo.GetAllAsync(query);
        }
    }
}
