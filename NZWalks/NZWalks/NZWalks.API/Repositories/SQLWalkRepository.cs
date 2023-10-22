using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var theWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (theWalk == null) return null;

            dbContext.Remove(theWalk);
            await dbContext.SaveChangesAsync();
            return theWalk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, int pageNumber = 1, int pageSize = 10)
        {
            var walks = dbContext.Walks.Include("Region").Include("Difficulty").AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false) 
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Pagination
            var skipItems = (pageNumber - 1) * pageSize;
            walks = walks.Skip(skipItems).Take(pageSize);

            return await walks.ToListAsync();
        }

        public async Task<Walk> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.
                Include("Region").
                Include("Difficulty").
                FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk) { 
            var theWalkModel = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (theWalkModel == null) return null;

            // Continue updating...

            theWalkModel.Name = walk.Name;
            theWalkModel.Description = walk.Description;
            theWalkModel.LengthInKm = walk.LengthInKm;
            theWalkModel.WalkImageUrl = walk.WalkImageUrl;
            theWalkModel.DifficultyId = walk.DifficultyId;
            theWalkModel.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();
            return walk;
        }
    }
}
