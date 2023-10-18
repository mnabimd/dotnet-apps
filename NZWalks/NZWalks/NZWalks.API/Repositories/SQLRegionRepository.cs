using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var theRegion = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (theRegion == null) return null;

            dbContext.Remove(theRegion);
            await dbContext.SaveChangesAsync();

            return theRegion;

        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            var theRegion =  await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (theRegion == null) return null;

            return theRegion;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region updatedRegion)
        {
            var theRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (theRegion == null) return null;

            theRegion.Name = updatedRegion.Name;
            theRegion.Code = updatedRegion.Code;
            theRegion.RegionImageUrl = updatedRegion.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return theRegion;
        }
    }
}
