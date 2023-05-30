using LeaveManagement.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Web.Models
{
    public class LeaveRequestVM
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LeaveTypeId { get; set; }
        public SelectList LeaveTypes { get; set; }
        public string? RequestComments { get; set; }
    }
}
