namespace LeaveManagement.Web.Models
{
    public class EmployeeAllocationVM: EmployeesListVM
    {
        public List<LeaveAllocationVM> LeaveAllocations { get; set; }
    }
}
