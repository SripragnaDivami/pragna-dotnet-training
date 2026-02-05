namespace BackendApicalls.Models.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int Experience { get; set; }
        public string? City { get; set; }
        public required string Department { get; set; }
    }
}