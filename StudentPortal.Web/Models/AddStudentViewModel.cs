using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models
{
    public class AddStudentViewModel
    {
        [Required]
        public string Name { get; set; }
		[Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [DisplayName("Phone Number")]
        [Range(1,9999999999, ErrorMessage= "Enter correct number")]
        public string Phone { get; set; }
        public bool Subscribed { get; set; }
    }
}
