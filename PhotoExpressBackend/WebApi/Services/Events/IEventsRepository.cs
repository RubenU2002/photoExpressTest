using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Services.Events
{
    public interface IEventsRepository
    {
        Task<Response<List<Event>>> GetAllAsync();
        Task<Response<Event>> GetByIdAsync(Guid id);
        Task<Response<Event>> AddAsync(EventRequest eventToCreate);
        Task<Response<Event>> UpdateAsync(Guid id,EventRequest eventToUpdate);
        Task<Response<Event>> DeleteAsync(Guid id);
    }
}
