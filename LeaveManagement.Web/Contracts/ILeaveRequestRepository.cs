﻿using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;

namespace LeaveManagement.Web.Contracts
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<bool> CreateLeaveRequest(LeaveRequestCreateVM model);
        Task<EmployeeLeaveRequestViewVM> GetMyLeaveRequestDetails();
        Task<List<LeaveRequestVM>> GetEmployeeRequestsAsync(string employeeId);
        Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList ();
        Task ChangeRequestApproval(int leaveRequestId, bool approved);
        Task<LeaveRequestVM> GetLeaveRequestAsync(int? id);
        Task CancelRequest(int id);

    }
}
