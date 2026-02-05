namespace BackendApicalls.Services.Interfaces
{
    using BackendApicalls.Models.DTOs;
   
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAllEmployeesAsync();
    }
}