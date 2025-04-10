using Microsoft.EntityFrameworkCore;
using nps.Migrations.Data;
using nps.Services.Order;
using nps.Services.Survey;

namespace nps.Services.Worker
{
    public sealed class WorkerService : IWorkerService
    {
        private readonly AppDbContext _db;

        private readonly ILogger<WorkerService> _logger;

        public WorkerService(ILogger<WorkerService> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<Models.Worker?> GetByEmail(string email)
        {
            return await _db.Workers.FirstOrDefaultAsync(worker => worker.Email == email);
        }
    }
}
