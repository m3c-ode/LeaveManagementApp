using LeaveManagement.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Web.Models
{
    public class LeaveRequestVM: LeaveRequestCreateVM
    {
        public int Id { get; set; }
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }
        [Display(Name ="Date requested")]
        public DateTime DateCreated { get; set; }
        [Display(Name ="Leave Type")]
        public LeaveTypeVM? LeaveType { get; set; }

        public EmployeesListVM Employee { get; set; }
        //public string? RequestingEmployeeId { get; set; }
    }
}
