namespace Media.Entity
{
    public class Media
    {
        public Media()
        {

        }

        public Media(Guid id, string title, string description, DateTime createDate, DateTime updateDate, string filename, string originalFilename, double sizeInKb)
        {
            Id = id;
            Title = title;
            Description = description;
            CreateDate = createDate;
            UpdateDate = updateDate;
            FileName = filename;
            OriginalFileName = originalFilename;
            SizeInKb = sizeInKb;
        }

        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string FileName { get; set; }
        public string OriginalFileName { get; set; }
        public double SizeInKb { get; set; }

        public static Media Create(Guid id, string title, string description, DateTime createDate, DateTime updateDate, string filename, string originalFilename, double sizeInKb)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.", nameof(title));
            if (title.Length > 255)
                throw new ArgumentException("Title cannot exceed 255 characters.", nameof(title));

            if (createDate == null || createDate == DateTime.MinValue)
                throw new ArgumentException("Create date is required.", nameof(createDate));

            if (updateDate == null || updateDate == DateTime.MinValue)
                throw new ArgumentException("Update date is required.", nameof(updateDate));

            if (string.IsNullOrEmpty(filename))
                throw new ArgumentException("Filename is required");

            if (string.IsNullOrEmpty(originalFilename))
                throw new ArgumentException("Original filename is required");

            if (sizeInKb <= 0)
                throw new ArgumentException("Size in KB must be greater than zero.", nameof(sizeInKb));

            return new Media(id, title, description, createDate, updateDate, filename, originalFilename, sizeInKb);
        }
    }
}
