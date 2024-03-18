using Infraestructure.DataAccess;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApi.Services.Events;
    public class EventsRepository : IEventsRepository
    {
        private readonly photoExpressContext _db;
        private readonly IConfiguration _conf;

        public EventsRepository(photoExpressContext db, IConfiguration config)
        {
            _db = db;
            _conf = config;
        }

        public async Task<Response<Event>> AddAsync(EventRequest eventToCreate)
        {
            var newEvent = new Event
            {
                InstitutionId = eventToCreate.InstitutionId,
                NumberOfStudents = eventToCreate.NumberOfStudents,
                StartTime= eventToCreate.StartTime,
                ServiceCost = CalculateCost(eventToCreate),
                CapAndGown = eventToCreate.CapAndGown
            };

            if(!string.IsNullOrEmpty(eventToCreate.InstitutionAddress) && !string.IsNullOrEmpty(eventToCreate.InstitutionName))
            {
                newEvent.Institution = new HigherEducationInstitution
                {
                    InstitutionName = eventToCreate.InstitutionName,
                    InstitutionAddress = eventToCreate.InstitutionAddress
                };
            }
            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
            var savedEventEntry = await _db.Events.AddAsync(newEvent);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                return Response<Event>.Success(savedEventEntry.Entity);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Response<Event>.Fail(ex.Message);
            }
        }

        public async Task<Response<Event>> DeleteAsync(Guid id)
        {
            var eventToDelete = await _db.Events.FindAsync(id);
            if (eventToDelete == null)
            {
                return Response<Event>.Fail("Event not found");
            }
            var modifications = _db.EventModificationLogs.Where(m => m.EventId == id);
            _db.EventModificationLogs.RemoveRange(modifications);
            await _db.SaveChangesAsync();
            _db.Events.Remove(eventToDelete);
            await _db.SaveChangesAsync();
            return Response<Event>.Success(eventToDelete);
        }

        public async Task<Response<List<Event>>> GetAllAsync()
        {
            var events = await _db.Events.Include(e => e.Institution).Include(e=>e.EventModificationLogs).ToListAsync();
            return Response<List<Event>>.Success(events);
        }

        public async Task<Response<Event>> GetByIdAsync(Guid id)
        {
            var eventFound = await _db.Events.Include(i=>i.Institution).FirstOrDefaultAsync(e=>e.EventId==id);
            if (eventFound is null)
            {
                return Response<Event>.Fail("Event not found");
            }
            return Response<Event>.Success(eventFound);
        }

        public async Task<Response<Event>> UpdateAsync(Guid id,EventRequest eventToUpdate)
        {
            var foundEvent = await GetByIdAsync(id);
            if (!foundEvent.IsSuccess || foundEvent.Data is null)
                return Response<Event>.Fail(foundEvent.ErrorMessage);
            var found = foundEvent.Data;
            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                string eventBeforeUpdate = JsonSerializer.Serialize(found);

                found.ServiceCost = CalculateCost(eventToUpdate);
                found.InstitutionId = eventToUpdate.InstitutionId;
                found.CapAndGown = eventToUpdate.CapAndGown;
                found.NumberOfStudents = eventToUpdate.NumberOfStudents;
                found.StartTime = eventToUpdate.StartTime;
                await _db.SaveChangesAsync();
                var updated = await GetByIdAsync(id);
                if (!updated.IsSuccess || updated.Data is null)
                    return Response<Event>.Fail(foundEvent.ErrorMessage);
                string eventAfterUpdate = JsonSerializer.Serialize(updated.Data);
                var modificationLog = new EventModificationLog
                {
                    ModificationId = new Guid(),
                    EventAfter = eventAfterUpdate,
                    EventBefore = eventBeforeUpdate,
                    EventId = id,
                    ModificationDate = DateTime.Now
                };
                var a = await _db.EventModificationLogs.AddAsync(modificationLog);
                var b = await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                return Response<Event>.Success(found);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                return Response<Event>.Fail(e.Message);
            }
        }
        private decimal CalculateCost(EventRequest data)
        {
            
            decimal serviceTotalCost = _conf.GetValue<decimal>("BaseServiceCost");
            if(data.ServiceCost!=0)
            serviceTotalCost = data.ServiceCost;
            if (data.CapAndGown)
            {
                decimal CapAndGownCost = _conf.GetValue<decimal>("CapAndGownCost");
                decimal addedCost = CapAndGownCost * data.NumberOfStudents;
                serviceTotalCost += addedCost;
            }
            return serviceTotalCost;
        }
}
