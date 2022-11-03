using System.ComponentModel.DataAnnotations;

namespace CRUD_Core_MVC.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        public int Age { get; set; }
    }
}
