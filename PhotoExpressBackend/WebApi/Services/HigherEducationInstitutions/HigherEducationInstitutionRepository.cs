using Infraestructure.DataAccess;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Services.HigherEducationInstitutions
{
    public class HigherEducationInstitutionRepository : IHigherEducationInstitutionRepository
    {
        private readonly photoExpressContext _db;

        public HigherEducationInstitutionRepository(photoExpressContext db)
        {
            _db = db;
        }

        public async Task<Response<HigherEducationInstitution>> AddAsync(HigherEducationInstitutionRequest institutionToAdd)
        {
            var newInstitution = new HigherEducationInstitution
            {
                InstitutionAddress= institutionToAdd.InstitutionAddress,
                InstitutionName= institutionToAdd.InstitutionName
            };  
            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var savedInstitutionEntry = await _db.HigherEducationInstitutions.AddAsync(newInstitution);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                return Response<HigherEducationInstitution>.Success(savedInstitutionEntry.Entity);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Response<HigherEducationInstitution>.Fail(ex.Message);
            }
        }

        public async Task<Response<HigherEducationInstitution>> DeleteAsync(Guid id)
        {
            var institutionToDelete = await _db.HigherEducationInstitutions.FindAsync(id);
            if (institutionToDelete == null)
            {
                return Response<HigherEducationInstitution>.Fail("Institution not found");
            }

            _db.HigherEducationInstitutions.Remove(institutionToDelete);
            await _db.SaveChangesAsync();
            return Response<HigherEducationInstitution>.Success(institutionToDelete);
        }

        public async Task<Response<List<HigherEducationInstitution>>> GetAllAsync()
        {
            var institutions = await _db.HigherEducationInstitutions.ToListAsync();
            return Response<List<HigherEducationInstitution>>.Success(institutions);
        }

        public async Task<Response<HigherEducationInstitution>> GetByIdAsync(Guid id)
        {
            var institutionFound = await _db.HigherEducationInstitutions.FindAsync(id);
            if (institutionFound == null)
            {
                return Response<HigherEducationInstitution>.Fail("Institution not found");
            }
            return Response<HigherEducationInstitution>.Success(institutionFound);
        }

        public async Task<Response<HigherEducationInstitution>> UpdateAsync(Guid id,HigherEducationInstitutionRequest institutionToUpdate)
        {
            var foundInstitute = await GetByIdAsync(id);
            if (!foundInstitute.IsSuccess)
                return Response<HigherEducationInstitution>.Fail(foundInstitute.ErrorMessage);
            var found = foundInstitute.Data;
            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                found.InstitutionAddress = institutionToUpdate.InstitutionAddress;
                found.InstitutionName = institutionToUpdate.InstitutionName;
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                return Response<HigherEducationInstitution>.Success(found);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                return Response<HigherEducationInstitution>.Fail(e.Message);
            }
        }
    }
}
