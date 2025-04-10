namespace nps.Services.Worker
{
    public interface IWorkerService
    {
        Task<Models.Worker?> GetByEmail(string email);
        bool PasswordMatch(string password, string db_password);
        bool PasswordMatchHASH(string  password, string db_password);   
    }
}
