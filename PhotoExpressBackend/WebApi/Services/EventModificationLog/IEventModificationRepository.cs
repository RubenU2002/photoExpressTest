using Infraestructure.Models;

namespace WebApi.Services.EventModification
{
    public interface IEventModificationRepository
    {
        Task<Response<List<EventModificationLog>>> GetAllByEventIdAsync(Guid id);
        Task<Response<EventModificationLog>> GetByIdAsync(Guid id);
    }
}
