﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        [HttpPost]
        // API : api/walks
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequest)
        {
            Walk walkDomain = _mapper.Map<Walk>(addWalkRequest);

            await _walkRepository.CreateAsync(walkDomain);

            return Ok(_mapper.Map<WalkDTO>(walkDomain));
        }

        [HttpGet]
        // API : api/walks
        public async Task<IActionResult> GetAll()
        {
            List<Walk> walks = await _walkRepository.GetAllAsync();
            return Ok(_mapper.Map<List<WalkDTO>>(walks));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        // API : api/walks/{id}
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            Walk? walkDomain = await _walkRepository.GetByIdAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDTO>(walkDomain));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        // API : api/walks/{id}
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO updateWalkRequest)
        {
            Walk walkToUpdate = _mapper.Map<Walk>(updateWalkRequest);

            Walk? updatedWalk = await _walkRepository.UpdateAsync(id, walkToUpdate);

            if (updatedWalk == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDTO>(updatedWalk));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        // API : api/walks/{id}
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Walk? deletedWalk = await _walkRepository.DeleteAsync(id);

            if (deletedWalk == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDTO>(deletedWalk));
        }
    }
}
