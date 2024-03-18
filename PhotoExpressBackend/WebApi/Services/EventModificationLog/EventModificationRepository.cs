using Infraestructure.DataAccess;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace WebApi.Services.EventModification
{
    public class EventModificationRepository : IEventModificationRepository
    {
        private readonly photoExpressContext _db;

        public EventModificationRepository(photoExpressContext db)
        {
            _db = db;
        }
        public async Task<Response<List<EventModificationLog>>> GetAllByEventIdAsync(Guid id)
        {
            var modifications = await _db.EventModificationLogs.Where(em => em.EventId == id).ToListAsync();
            return Response<List<EventModificationLog>>.Success(modifications);
        }

        public async Task<Response<EventModificationLog>> GetByIdAsync(Guid id)
        {

            var modification = await _db.EventModificationLogs.Where(em => em.ModificationId == id).FirstOrDefaultAsync();
            if (modification is null)
            {
                return Response<EventModificationLog>.Fail("Modification not found");
            }
            return Response<EventModificationLog>.Success(modification);
            
        }
    }
}
