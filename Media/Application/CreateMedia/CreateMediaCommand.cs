namespace Media.Application.CreateMedia
{
    public record CreateMediaCommand(string Title, string Description, string Filename, string OriginalFilename, double SizeInKb, double Width, double Height, IFormFile File);
}
