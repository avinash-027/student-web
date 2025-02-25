using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public bool Subscribed { get; set; }
    }
}
