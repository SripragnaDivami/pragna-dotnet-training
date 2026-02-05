namespace BackendApicalls.Services.Interfaces
{
    using BackendApicalls.Models.entities;
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeesAsync();
    }
}