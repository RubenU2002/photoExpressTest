using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infraestructure.Models;
using WebApi.Services.HigherEducationInstitutions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HigherEducationInstitutionsController : ControllerBase
    {
        private readonly ILogger<HigherEducationInstitutionsController> _logger;
        private readonly IHigherEducationInstitutionRepository _institutionRepository;

        public HigherEducationInstitutionsController(ILogger<HigherEducationInstitutionsController> logger, IHigherEducationInstitutionRepository institutionRepository)
        {
            _logger = logger;
            _institutionRepository = institutionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<HigherEducationInstitution>>>> GetAllInstitutions()
        {
            var response = await _institutionRepository.GetAllAsync();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<HigherEducationInstitution>>> GetInstitutionById(Guid id)
        {
            var response = await _institutionRepository.GetByIdAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPost]
        public async Task<ActionResult<Response<HigherEducationInstitution>>> CreateInstitution(HigherEducationInstitutionRequest institutionToCreate)
        {
            var response = await _institutionRepository.AddAsync(institutionToCreate);
            if (response.IsSuccess)
            {
                return CreatedAtAction(nameof(GetInstitutionById), new { id = response?.Data?.InstitutionId }, response);
            }
            return BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response<HigherEducationInstitution>>> UpdateInstitution(Guid id, HigherEducationInstitutionRequest institutionToUpdate)
        {
            var response = await _institutionRepository.UpdateAsync(id,institutionToUpdate);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<HigherEducationInstitution>>> DeleteInstitution(Guid id)
        {
            var response = await _institutionRepository.DeleteAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
