using LeaveManagement.Web.Data;

namespace LeaveManagement.Web.Contracts
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task LeaveAllocation(int leaveTypeId);
        Task<bool> DoesAllocationExists(string employeeId, int leaveTypeId, int period);


    }
}
