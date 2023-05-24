using LeaveManagement.Web.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Web.Models
{
    public class LeaveAllocationVM
    {
        public int Id { get; set; }
        [Display(Name="Number of Days")]
        public int NumberOfDays { get; set; }
        public int Period { get; set; }
        public LeaveTypeVM LeaveType { get; set; }
    }
}