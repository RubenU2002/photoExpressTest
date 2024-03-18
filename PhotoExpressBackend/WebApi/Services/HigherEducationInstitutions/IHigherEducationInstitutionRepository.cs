using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infraestructure.Models;
namespace WebApi.Services.HigherEducationInstitutions
{
    public interface IHigherEducationInstitutionRepository
    {
        Task<Response<List<HigherEducationInstitution>>> GetAllAsync();
        Task<Response<HigherEducationInstitution>> GetByIdAsync(Guid id);
        Task<Response<HigherEducationInstitution>> AddAsync(HigherEducationInstitutionRequest institutionToAdd);
        Task<Response<HigherEducationInstitution>> UpdateAsync(Guid id,HigherEducationInstitutionRequest institutionToUpdate);
        Task<Response<HigherEducationInstitution>> DeleteAsync(Guid id);
    }
}
