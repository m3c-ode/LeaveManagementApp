namespace LeaveManagement.Web.Models
{
    public class LeaveAllocationEditVM: LeaveAllocationVM
    {
        public EmployeesListVM? Employee { get; set; }
        public string EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
    }
}
