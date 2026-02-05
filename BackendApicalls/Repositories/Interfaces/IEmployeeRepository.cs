namespace BackendApicalls.Repositories.Interfaces
{
    using BackendApicalls.Models.entities;
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();
    }
}