using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Common.Models
{
    public class LeaveTypeVM
    {
        public int Id { get; set; }
        [DisplayName("Leave Type Name")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Default Number of Days")]
        [Required]
        [Range(1, 25, ErrorMessage = "Please enter a valid number")]
        //[Display(Name = "Default Number of Days")]
        public int DefaultDays { get; set; }
    }
}
