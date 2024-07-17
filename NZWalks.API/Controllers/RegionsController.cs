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
        [HttpGet]
        // API : api/regions
        public IActionResult GetAll()
        {
            List<Region> regions = dbContext.Regions.ToList();

            List<RegionDTO> regionDTOs = regions.Select(r => new RegionDTO
            {
                Id = r.Id,
                Code = r.Code,
                Name = r.Name,
                RegionImageUrl = r.RegionImageUrl
            }).ToList();

            return Ok(regionDTOs);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        // API : api/regions/{id}
        public IActionResult GetById([FromRoute] Guid id)
        {
            Region region = dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            RegionDTO regionDTO = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };
            return Ok(regionDTO);
        }

        [HttpPost]
        // API : api/regions
        public IActionResult Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            Region regionDomainModel = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            RegionDTO regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        // API : api/regions/{id}
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            Region region = dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            region.Code = updateRegionRequestDTO.Code;
            region.Name = updateRegionRequestDTO.Name;
            region.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;

            dbContext.SaveChanges();

            RegionDTO regionDTO = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        // API : api/regions/{id}
        public IActionResult Delete([FromRoute] Guid id)
        {
            Region region = dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            dbContext.Regions.Remove(region);
            dbContext.SaveChanges();

            RegionDTO regionDTO = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDTO);
        }
    }
}
