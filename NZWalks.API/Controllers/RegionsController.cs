using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        // API : api/regions
        public async Task<IActionResult> GetAll()
        {
            List<Region> regions = await regionRepository.GetAllAsync();

            return Ok(mapper.Map<List<RegionDTO>>(regions));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        // API : api/regions/{id}
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            Region? regionDomainModel = await regionRepository.GetByIdAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDTO>(regionDomainModel));
        }

        [HttpPost]
        // API : api/regions
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            Region regionDomainModel = mapper.Map<Region>(addRegionRequestDTO);

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, mapper.Map<RegionDTO>(regionDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        // API : api/regions/{id}
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            Region? regionDomainModel = mapper.Map<Region>(updateRegionRequestDTO);
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDTO>(regionDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        // API : api/regions/{id}
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Region? regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDTO>(regionDomainModel));
        }
    }
}
