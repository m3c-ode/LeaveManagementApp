using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Web.Models
{
    public class AdminLeaveRequestViewVM
    {
        [Display(Name = "Total number of requests")]
        public int TotalRequests { get; set; }
        [Display(Name = "Number of Approved requests")]
        public int ApprovedRequests { get; set; }
        [Display(Name = "Number of Pending requests")]
        public int PendingRequests { get; set; }
        [Display(Name = "Number of Rejected requests")]
        public int RejectedRequests { get; set; }
        public List<LeaveRequestVM> LeaveRequests { get; set; }
    }
}
