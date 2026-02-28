using Media.Infrastructure;
using Shared.Model;

namespace Media.Application.CreateMedia
{
    public class CreateMediaHandler
    {
        private readonly IMediaRepository _repo;

        public CreateMediaHandler(IMediaRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<Guid>> Handle(CreateMediaCommand cmd)
        {
            if (cmd.File == null)
                return Result<Guid>.Fail("File is required.");

            if (string.IsNullOrEmpty(cmd.Title))
                return Result<Guid>.Fail("Title is required.");

            var id = Guid.NewGuid();
            var fileName = DateTime.Now.Ticks + Path.GetExtension(cmd.File.FileName);

            var customer = Media.Entity.Media.Create(id, cmd.Title, cmd.Description, DateTime.Now, DateTime.Now, fileName, cmd.File.FileName, cmd.File.Length / 1000);
            await _repo.AddAsync(customer);

            return Result<Guid>.Ok(customer.Id);
        }
    }
}
