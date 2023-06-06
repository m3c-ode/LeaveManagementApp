using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Common.Models
{
    public class LeaveRequestVM : LeaveRequestCreateVM
    {
        public int Id { get; set; }
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }
        [Display(Name = "Date requested")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Leave Type")]
        public LeaveTypeVM? LeaveType { get; set; }

        public EmployeesListVM Employee { get; set; }
        //public string? RequestingEmployeeId { get; set; }
    }
}
