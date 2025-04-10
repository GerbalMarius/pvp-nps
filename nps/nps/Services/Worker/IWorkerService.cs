namespace nps.Services.Worker
{
    public interface IWorkerService
    {
        Task<Models.Worker?> GetByEmail(string email);
    }
}
