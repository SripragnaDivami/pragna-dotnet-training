using System.ComponentModel.DataAnnotations.Schema;

namespace BackendApicalls.Models.entities
{
   [Table("employees_list")]
    public class Employee
    {
        
        [Column("id")]
        public int Id { get; set; }

        [Column("first_name")]
        public required string FirstName { get; set; }

        [Column("last_name")]
        public required string LastName { get; set; }
        [Column("experience")]
         public int Experience { get; set; }

        [Column("city")]
        public string? City { get; set; }
        [Column("department")]
        public required string Department { get; set; }

       
    }
}