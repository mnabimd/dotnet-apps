using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // Get all regions
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> Index()
        {
            var domainRegions = await regionRepository.GetAllAsync();

            // Convert to DTO
            var regions = mapper.Map<List<RegionDto>>(domainRegions);

            return Ok(regions);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var theRegion = await regionRepository.GetByIdAsync(id);
                
            if (theRegion == null) { 
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(theRegion));
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto data)
        {
            // Convert to RegionData
            var newRegion = mapper.Map<Region>(data);

            await regionRepository.CreateAsync(newRegion);

            // Convert back to DTO
            var regionDto = mapper.Map<RegionDto>(newRegion);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id, }, regionDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updatedRegion)
        {

            // First we should convert the DTO to Domain Model
            var updatedRegionAsDomainModel = mapper.Map<Region>(updatedRegion);

            var theRegion = await regionRepository.UpdateAsync(id, updatedRegionAsDomainModel);

            if (theRegion == null) return NotFound();


            var regionDto = mapper.Map<RegionDto>(theRegion);
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) {
            var theRegion = await regionRepository.DeleteAsync(id);

            if (theRegion == null) return NotFound();

            return Ok(mapper.Map<RegionDto>(theRegion));
        }
    }
}
