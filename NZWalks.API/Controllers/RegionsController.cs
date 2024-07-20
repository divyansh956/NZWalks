using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        // API : api/regions
        public async Task<IActionResult> GetAll()
        {
            List<Region> regions = await _regionRepository.GetAllAsync();

            return Ok(_mapper.Map<List<RegionDTO>>(regions));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        // API : api/regions/{id}
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            Region? region = await _regionRepository.GetByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDTO>(region));
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        // API : api/regions
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequest)
        {
            Region region = _mapper.Map<Region>(addRegionRequest);

            region = await _regionRepository.CreateAsync(region);

            return CreatedAtAction(nameof(GetById), new { id = region.Id }, _mapper.Map<RegionDTO>(region));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        // API : api/regions/{id}
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequest)
        {
            Region regionToUpdate = _mapper.Map<Region>(updateRegionRequest);
            Region? updatedRegion = await _regionRepository.UpdateAsync(id, regionToUpdate);
            if (updatedRegion == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDTO>(updatedRegion));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        // API : api/regions/{id}
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Region? deletedRegion = await _regionRepository.DeleteAsync(id);
            if (deletedRegion == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDTO>(deletedRegion));
        }
    }
}
