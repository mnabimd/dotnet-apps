using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get all regions
        [HttpGet]
        public IActionResult Index()
        {
            var domainRegions = dbContext.Regions.ToList();

            // Convert to DTO
            var regions = new List<RegionDto>();
            foreach (var region in domainRegions) {
                regions.Add(new RegionDto {
                    Code = region.Code,
                    Id = region.Id,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });                    
            }

            return Ok(regions);
        }


        [HttpGet]
        [Route("{id:Guid}")] 
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var theRegion = await dbContext.Regions.FindAsync(id);
                
            if (theRegion == null) { 
                return NotFound();
            }

            // Great!
            var regionDto = new RegionDto()
            {
                Id = theRegion.Id,
                Name = theRegion.Name,
                Code = theRegion.Code,
                RegionImageUrl = theRegion.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto data)
        {
            // Convert to RegionData
            var newRegion = new Region() {
                Code = data.Code,
                Name = data.Name,
                RegionImageUrl = data.RegionImageUrl
            };

            await dbContext.Regions.AddAsync(newRegion);
            await dbContext.SaveChangesAsync();

            // Convert back to DTO
            var regionDto = new RegionDto()
            {
                Id = newRegion.Id,
                Code = newRegion.Code,
                RegionImageUrl = newRegion.RegionImageUrl,
                Name = newRegion.Name
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id, }, regionDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updatedRegion)
        {
            var theRegion = await dbContext.Regions.FindAsync(id);

            if (theRegion == null) return NotFound();

            // Update region
            theRegion.Name = updatedRegion.Name;
            theRegion.Code = updatedRegion.Code;
            theRegion.RegionImageUrl = updatedRegion.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            var regionDto = new RegionDto() { 
                Id = id, Code = updatedRegion.Code,
                Name = updatedRegion.Name,
                RegionImageUrl = updatedRegion.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) {
            var theRegion = await dbContext.Regions.FindAsync(id);

            if (theRegion == null) return NotFound();

            // Found? Proceed deletion
            dbContext.Regions.Remove(theRegion);

            dbContext.SaveChanges();

            return Ok();
        }
    }
}
