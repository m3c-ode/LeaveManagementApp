using AutoMapper;
using AutoMapper.QueryableExtensions;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;
using LeaveManagement.Web.Services_Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Repositories___Services
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;
        private readonly AutoMapper.IConfigurationProvider configurationProvider;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly UserManager<Employee> userManager;

        public LeaveRequestRepository(ApplicationDbContext context,
            IMapper mapper,
            IEmailSender emailSender,
            AutoMapper.IConfigurationProvider configurationProvider,
            IHttpContextAccessor httpContextAccessor,
            ILeaveAllocationRepository leaveAllocationRepository,
            UserManager<Employee> userManager
            ) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.emailSender = emailSender;
            this.configurationProvider = configurationProvider;
            this.httpContextAccessor = httpContextAccessor;
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.userManager = userManager;
            //Check if accepted
        }

        public async Task ChangeRequestApproval(int leaveRequestId, bool approved)
        {
            //get leaveRequests
            var leaveRequest = await GetAsync(leaveRequestId);
            if (leaveRequest == null)
            {
                throw new ArgumentNullException(nameof(leaveRequest), "Leave request not found.");
            }
            leaveRequest.Approved = approved;
            if (approved)
            {
                //leaveRequest.Approved = true;
                leaveRequest.Cancelled = false;
                //make the opration on day deduction
                var allocation = await leaveAllocationRepository.GetEmployeeAllocation(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                var daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                allocation.NumberOfDays -= daysRequested;
                await leaveAllocationRepository.UpdateAsync(allocation);


            }
            else if (!approved)
            {
                //leaveRequest.Approved = false;
                leaveRequest.Cancelled = true;
            }
            await UpdateAsync(leaveRequest);

            var user = await userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId);

            var approvalStatus = approved ? "Approved" : "Declined";

            await emailSender.SendEmailAsync(user.Email, $"Leave Request {approvalStatus}", $"Your leave request from: " +
    $"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been {approvalStatus.ToLower()} ");


        }

        public async Task<bool> CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            var user = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);

            //Check if allocation allows taking number of days. Get employee leave alloc, compare days, return boolean
            var allocation = await leaveAllocationRepository.GetEmployeeAllocation(user.Id, model.LeaveTypeId);
            //check is allocation is null
            if (allocation == null)
            {
                return false;
            }
            var numberOfDays = (int)(model.EndDate.Value - model.StartDate.Value).TotalDays;
            if (numberOfDays > allocation.NumberOfDays)
            {
                return false;
            }

            var leaveRequest = mapper.Map<LeaveRequest>(model);
            leaveRequest.DateCreated = DateTime.Now;
            leaveRequest.RequestingEmployeeId = user.Id;

            await AddAsync(leaveRequest);

            await emailSender.SendEmailAsync(user.Email, "Leave Request Submitted", $"Your leave request from: " +
                $"{model.StartDate} to {model.EndDate} has been submitted for approval");

            return true;
        }

        public async Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList()
        {
            //var leaveRequests = await GetAllAsync();
            var leaveRequests = await context.LeaveRequests.Include(q => q.LeaveType).ToListAsync();
            var model = new AdminLeaveRequestViewVM
            {
                TotalRequests = leaveRequests.Count,
                ApprovedRequests = leaveRequests.Count(q => q.Approved == true),
                PendingRequests = leaveRequests.Count(q => q.Approved == null),
                RejectedRequests = leaveRequests.Count(q => q.Approved == false),
                LeaveRequests = mapper.Map<List<LeaveRequestVM>>(leaveRequests),
            };
            foreach (var item in model.LeaveRequests)
            {
                item.Employee = mapper.Map<EmployeesListVM>(await userManager.FindByIdAsync(item.RequestingEmployeeId));
            }
            return model;
        }

        public async Task<List<LeaveRequestVM>> GetEmployeeRequestsAsync(string employeeId)
        {
            return await context.LeaveRequests
                .Where(q => q.RequestingEmployeeId == employeeId)
                .ProjectTo<LeaveRequestVM>(configurationProvider)
                .ToListAsync();
        }

        public async Task<LeaveRequestVM?> GetLeaveRequestAsync(int? id)
        {
            var leaveReq = await context.LeaveRequests
                .Include(q => q.LeaveType)
                .Include(l => l.RequestingEmployee)
                .FirstOrDefaultAsync(q => q.Id == id);
            if (leaveReq == null)
            {
                return null;
            }
            var model = mapper.Map<LeaveRequestVM>(leaveReq);
            //is this line necessary? already inlcuded requesting employee, to double check
            model.Employee = mapper.Map<EmployeesListVM>(await userManager.FindByIdAsync(leaveReq?.RequestingEmployeeId));
            return model;
        }

        public async Task<EmployeeLeaveRequestViewVM> GetMyLeaveRequestDetails()
        {
            var user = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);
            var allocations = (await leaveAllocationRepository.GetEmployeeAllocations(user.Id)).LeaveAllocations;
            //var requests = mapper.Map<List<LeaveRequestVM>>(await GetEmployeeRequestsAsync(user.Id));
            var requests = await GetEmployeeRequestsAsync(user.Id);

            var model = new EmployeeLeaveRequestViewVM(allocations, requests);

            return model;
        }

        public async Task CancelRequest(int id)
        {
            var leaveReq = await GetAsync(id);
            leaveReq.Cancelled = true;
            await UpdateAsync(leaveReq);

            var user = await userManager.FindByIdAsync(leaveReq.RequestingEmployeeId);


            await emailSender.SendEmailAsync(user.Email, $"Leave Request Cancelled", $"Your leave request from: " +
$"{leaveReq.StartDate} to {leaveReq.EndDate} has been cancelled ");

        }
    }
}
