using Media.Infrastructure;
using Shared.Model;

namespace Media.Application.DeleteMedia
{
    public class DeleteMediaHandler
    {
        private readonly IMediaRepository _repo;

        public DeleteMediaHandler(IMediaRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(DeleteMediaCommand cmd)
        {
            var media = await _repo.GetByIdAsync(cmd.Id);
            if (media == null)
                return Result.Fail("Media not found");

            await _repo.DeleteAsync(media);

            return Result.Ok();
        }
    }
}
