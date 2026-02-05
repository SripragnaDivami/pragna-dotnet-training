using BackendApicalls.Models.entities;
using BackendApicalls.Repositories;
using BackendApicalls.Services.Interfaces;

namespace BackendApicalls.Services
{
    public class EmployeeService:IEmployeeService

    {

        private readonly EmployeeRepository employeeRepository;
        public EmployeeService(EmployeeRepository employeesRepository)
        {
            employeeRepository = employeesRepository;
        }
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await employeeRepository.GetAllAsync();
        }

        
    }
}