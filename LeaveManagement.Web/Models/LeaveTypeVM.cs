using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Web.Models
{
    public class LeaveTypeVM
    {
        public int Id { get; set; }
        [DisplayName("Leave Type Name")]
        public string Name { get; set; }
        [DisplayName("Default Number of Days")]
        //[Display(Name = "Default Number of Days")]
        public int DefaultDays { get; set; }
    }
}
