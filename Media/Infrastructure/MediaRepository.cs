using Media.Application.GetAllMedia;
using Media.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Media.Infrastructure
{
    public class MediaRepository : IMediaRepository
    {
        private readonly AppDbContext _context;

        public MediaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Media.Entity.Media media)
        {
            //TODO: save file on disk logic

            await _context.Media.AddAsync(media);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Media.Entity.Media media)
        {
            //TODO: replace file logic

            var existingMedia = await _context.Media.FirstOrDefaultAsync(x => x.Id == media.Id);
            if (existingMedia == null)
            {
                throw new Exception("User does not exist");
            }

            existingMedia.Title = media.Title;
            existingMedia.Description = media.Description;
            existingMedia.UpdateDate = media.UpdateDate;

            if (!string.IsNullOrEmpty(media.OriginalFileName))
                existingMedia.OriginalFileName = media.OriginalFileName;

            if (media.SizeInKb > 0)
                existingMedia.SizeInKb = media.SizeInKb;

            if (media.Width > 0)
                existingMedia.Width = media.Width;

            if (media.Height > 0)
                existingMedia.Height = media.Height;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Media.Entity.Media>> GetAllAsync(GetAllMediaQuery criteria)
        {
            var result = _context.Media.AsQueryable();

            #region Ids
            if (criteria.Ids != null && criteria.Ids.Value != null && criteria.Ids.Value.Any())
                switch (criteria.Ids.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(x => criteria.Ids.Value.Contains(x.Id));
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(x => !criteria.Ids.Value.Contains(x.Id));
                        break;
                    default:
                        break;
                }
            #endregion


            #region Firstname

            if (criteria.Title != null && !string.IsNullOrEmpty(criteria.Title.Value))
                switch (criteria.Title.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(x => x.Title == criteria.Title.Value);
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(x => x.Title != criteria.Title.Value);
                        break;
                    case Shared.Model.FilterOperator.Contains:
                        result = result.Where(x => x.Title.Contains(criteria.Title.Value));
                        break;
                    case Shared.Model.FilterOperator.StartsWith:
                        result = result.Where(x => x.Title.StartsWith(criteria.Title.Value));
                        break;
                    case Shared.Model.FilterOperator.EndsWith:
                        result = result.Where(x => x.Title.EndsWith(criteria.Title.Value));
                        break;
                    default:
                        break;
                }

            #endregion

            #region Lastname
            if (criteria.Description != null && !string.IsNullOrEmpty(criteria.Description.Value))
                switch (criteria.Description.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(x => x.Description == criteria.Description.Value);
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(x => x.Description != criteria.Description.Value);
                        break;
                    case Shared.Model.FilterOperator.Contains:
                        result = result.Where(x => x.Description.Contains(criteria.Description.Value));
                        break;
                    case Shared.Model.FilterOperator.StartsWith:
                        result = result.Where(x => x.Description.StartsWith(criteria.Description.Value));
                        break;
                    case Shared.Model.FilterOperator.EndsWith:
                        result = result.Where(x => x.Description.EndsWith(criteria.Description.Value));
                        break;
                    default:
                        break;
                }
            #endregion

            #region CreateDate
            if (criteria.CreateDate != null && criteria.CreateDate.Value != DateTime.MinValue)
                switch (criteria.CreateDate.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(x => x.CreateDate.Date == criteria.CreateDate.Value.Date);
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(x => x.CreateDate.Date != criteria.CreateDate.Value.Date);
                        break;
                    case Shared.Model.FilterOperator.GreaterOrEqual:
                        result = result.Where(x => x.CreateDate.Date >= criteria.CreateDate.Value.Date);
                        break;
                    case Shared.Model.FilterOperator.GreaterThan:
                        result = result.Where(x => x.CreateDate.Date > criteria.CreateDate.Value.Date);
                        break;
                    case Shared.Model.FilterOperator.LessOrEqual:
                        result = result.Where(x => x.CreateDate.Date <= criteria.CreateDate.Value.Date);
                        break;
                    case Shared.Model.FilterOperator.LessThan:
                        result = result.Where(x => x.CreateDate.Date < criteria.CreateDate.Value.Date);
                        break;
                    default:
                        break;
                }
            #endregion

            #region UpdateDate
            if (criteria.UpdateDate != null && criteria.UpdateDate.Value != DateTime.MinValue)
                switch (criteria.UpdateDate.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(x => x.UpdateDate.Date == criteria.UpdateDate.Value.Date);
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(x => x.UpdateDate.Date != criteria.UpdateDate.Value.Date);
                        break;
                    case Shared.Model.FilterOperator.GreaterOrEqual:
                        result = result.Where(x => x.UpdateDate.Date >= criteria.UpdateDate.Value.Date);
                        break;
                    case Shared.Model.FilterOperator.GreaterThan:
                        result = result.Where(x => x.UpdateDate.Date > criteria.UpdateDate.Value.Date);
                        break;
                    case Shared.Model.FilterOperator.LessOrEqual:
                        result = result.Where(x => x.UpdateDate.Date <= criteria.UpdateDate.Value.Date);
                        break;
                    case Shared.Model.FilterOperator.LessThan:
                        result = result.Where(x => x.UpdateDate.Date < criteria.UpdateDate.Value.Date);
                        break;
                    default:
                        break;
                }
            #endregion

            #region FileName
            if (criteria.FileName != null && !string.IsNullOrEmpty(criteria.FileName.Value))
                switch (criteria.FileName.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(x => x.FileName == criteria.FileName.Value);
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(x => x.FileName != criteria.FileName.Value);
                        break;
                    case Shared.Model.FilterOperator.Contains:
                        result = result.Where(x => x.FileName.Contains(criteria.FileName.Value));
                        break;
                    case Shared.Model.FilterOperator.StartsWith:
                        result = result.Where(x => x.FileName.StartsWith(criteria.FileName.Value));
                        break;
                    case Shared.Model.FilterOperator.EndsWith:
                        result = result.Where(x => x.FileName.EndsWith(criteria.FileName.Value));
                        break;
                    default:
                        break;
                }
            #endregion

            #region OriginalFileName
            if (criteria.OriginalFileName != null && !string.IsNullOrEmpty(criteria.OriginalFileName.Value))
                switch (criteria.OriginalFileName.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(x => x.OriginalFileName == criteria.OriginalFileName.Value);
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(x => x.OriginalFileName != criteria.OriginalFileName.Value);
                        break;
                    case Shared.Model.FilterOperator.Contains:
                        result = result.Where(x => x.OriginalFileName.Contains(criteria.OriginalFileName.Value));
                        break;
                    case Shared.Model.FilterOperator.StartsWith:
                        result = result.Where(x => x.OriginalFileName.StartsWith(criteria.OriginalFileName.Value));
                        break;
                    case Shared.Model.FilterOperator.EndsWith:
                        result = result.Where(x => x.OriginalFileName.EndsWith(criteria.OriginalFileName.Value));
                        break;
                    default:
                        break;
                }
            #endregion

            #region OriginalFileName
            if (criteria.OriginalFileName != null && !string.IsNullOrEmpty(criteria.OriginalFileName.Value))
                switch (criteria.OriginalFileName.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(x => x.OriginalFileName == criteria.OriginalFileName.Value);
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(x => x.OriginalFileName != criteria.OriginalFileName.Value);
                        break;
                    case Shared.Model.FilterOperator.Contains:
                        result = result.Where(x => x.OriginalFileName.Contains(criteria.OriginalFileName.Value));
                        break;
                    case Shared.Model.FilterOperator.StartsWith:
                        result = result.Where(x => x.OriginalFileName.StartsWith(criteria.OriginalFileName.Value));
                        break;
                    case Shared.Model.FilterOperator.EndsWith:
                        result = result.Where(x => x.OriginalFileName.EndsWith(criteria.OriginalFileName.Value));
                        break;
                    default:
                        break;
                }
            #endregion

            #region SizeInKb
            if (criteria.SizeInKb != null && criteria.SizeInKb.Value > 0)
                switch (criteria.SizeInKb.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(x => x.SizeInKb == criteria.SizeInKb.Value);
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(x => x.SizeInKb != criteria.SizeInKb.Value);
                        break;
                    case Shared.Model.FilterOperator.GreaterOrEqual:
                        result = result.Where(x => x.SizeInKb >= criteria.SizeInKb.Value);
                        break;
                    case Shared.Model.FilterOperator.LessOrEqual:
                        result = result.Where(x => x.SizeInKb <= criteria.SizeInKb.Value);
                        break;
                    case Shared.Model.FilterOperator.GreaterThan:
                        result = result.Where(x => x.SizeInKb > criteria.SizeInKb.Value);
                        break;
                    case Shared.Model.FilterOperator.LessThan:
                        result = result.Where(x => x.SizeInKb < criteria.SizeInKb.Value);
                        break;
                    default:
                        break;
                }
            #endregion

            #region Width
            if (criteria.Width != null && criteria.Width.Value > 0)
                switch (criteria.Width.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(x => x.Width == criteria.Width.Value);
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(x => x.Width != criteria.Width.Value);
                        break;
                    case Shared.Model.FilterOperator.GreaterOrEqual:
                        result = result.Where(x => x.Width >= criteria.Width.Value);
                        break;
                    case Shared.Model.FilterOperator.LessOrEqual:
                        result = result.Where(x => x.Width <= criteria.Width.Value);
                        break;
                    case Shared.Model.FilterOperator.GreaterThan:
                        result = result.Where(x => x.Width > criteria.Width.Value);
                        break;
                    case Shared.Model.FilterOperator.LessThan:
                        result = result.Where(x => x.Width < criteria.Width.Value);
                        break;
                    default:
                        break;
                }
            #endregion

            #region Height
            if (criteria.Height != null && criteria.Height.Value > 0)
                switch (criteria.Height.Operator)
                {
                    case Shared.Model.FilterOperator.Equal:
                        result = result.Where(x => x.Height == criteria.Height.Value);
                        break;
                    case Shared.Model.FilterOperator.NotEqual:
                        result = result.Where(x => x.Height != criteria.Height.Value);
                        break;
                    case Shared.Model.FilterOperator.GreaterOrEqual:
                        result = result.Where(x => x.Height >= criteria.Height.Value);
                        break;
                    case Shared.Model.FilterOperator.LessOrEqual:
                        result = result.Where(x => x.Height <= criteria.Height.Value);
                        break;
                    case Shared.Model.FilterOperator.GreaterThan:
                        result = result.Where(x => x.Height > criteria.Height.Value);
                        break;
                    case Shared.Model.FilterOperator.LessThan:
                        result = result.Where(x => x.Height < criteria.Height.Value);
                        break;
                    default:
                        break;
                }
            #endregion

            if (!string.IsNullOrEmpty(criteria.SearchText))
                result = result.Where(x => x.Title.Contains(criteria.SearchText) || x.Description.Contains(criteria.SearchText) || x.FileName.Contains(criteria.SearchText) || x.OriginalFileName.Contains(criteria.SearchText));

            return await result.Skip(criteria.Offset).Take(criteria.TopCount).ToListAsync();
        }
        public async Task<Media.Entity.Media?> GetByIdAsync(Guid id)
        {
            return await _context.Media.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task DeleteAsync(Media.Entity.Media media)
        {
            //TODO: remove file from disk logic
            _context.Media.Remove(media);
            await _context.SaveChangesAsync();
        }
    }
}
