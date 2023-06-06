using AutoMapper;
using AutoMapper.QueryableExtensions;
using LeaveManagement.Common.Constants;
using LeaveManagement.Application.Contracts;
using LeaveManagement.Data;
using LeaveManagement.Common.Models;
//using LeaveManagement.Web.Services;
using LeaveManagement.Application.Repositories___Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Application.Repositories___Services
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Employee> userManager;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;
        private readonly AutoMapper.IConfigurationProvider configurationProvider;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public LeaveAllocationRepository(ApplicationDbContext context,
            UserManager<Employee> userManager,
            IMapper mapper,
            IEmailSender emailSender,
            AutoMapper.IConfigurationProvider configurationProvider,
            ILeaveTypeRepository leaveTypeRepository) : base(context)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
            this.emailSender = emailSender;
            this.configurationProvider = configurationProvider;
            this.leaveTypeRepository = leaveTypeRepository;
        }
        //Checks to see if we already have an employee with the same leave allocation, to use before assigning.
        public Task<bool> DoesAllocationExists(string employeeId, int leaveTypeId, int period)
        {
            return context.LeaveAllocations.AnyAsync(query => query.EmployeeId == employeeId
                                                            && query.LeaveTypeId == leaveTypeId
                                                            && query.Period == period);

        }

        public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string employeeId)
        {
            var allocations = await context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Where(q => q.EmployeeId == employeeId)
                .ProjectTo<LeaveAllocationVM>(configurationProvider)
                .ToListAsync();
            var employee = await userManager.FindByIdAsync(employeeId);

            var employeeAllocationModel = mapper.Map<EmployeeAllocationVM>(employee);

            //no longer need to map because we did it ProjecctTo in query
            //employeeAllocationModel.LeaveAllocations = mapper.Map<List<LeaveAllocationVM>>(allocations);
            employeeAllocationModel.LeaveAllocations = allocations;

            return employeeAllocationModel;

        }

        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation (int allocationId)
        {
            var leaveAllocationEditVM = await context.LeaveAllocations
                .Include(q => q.LeaveType)
                .ProjectTo<LeaveAllocationEditVM>(configurationProvider)
                .FirstOrDefaultAsync(q => q.Id == allocationId);
            if (leaveAllocationEditVM == null)
            {
                return null;
            }

            var employee = await userManager.FindByIdAsync (leaveAllocationEditVM.EmployeeId);
            //var leaveAllocationEditVM = mapper.Map<LeaveAllocationEditVM>(allocation);
            leaveAllocationEditVM.Employee = mapper.Map<EmployeesListVM>(employee);
            return leaveAllocationEditVM;
        }

        public async Task LeaveAllocation(int leaveTypeId)
        {
            var employees = await userManager.GetUsersInRoleAsync(Roles.User);
            var period = DateTime.Now.Year;
            var leaveType = await leaveTypeRepository.GetAsync(leaveTypeId);
            var allocations = new List<LeaveAllocation>();
            var employeesWithNewAllocation = new List<Employee>();

            foreach (var employee in employees)
            {
                //Employee already has the allocation, go to next, continue
                if (await DoesAllocationExists(employee.Id, leaveTypeId, period)) continue;

                allocations.Add(new LeaveAllocation
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveTypeId,
                    Period = period,
                    NumberOfDays = leaveType.DefaultDays
                });
                employeesWithNewAllocation.Add(employee);
            }
            await AddRangeAsync(allocations);

            foreach (var employee in employeesWithNewAllocation)
            {
                //if (await DoesAllocationExists(employee.Id, leaveTypeId, period)) continue;


                await emailSender.SendEmailAsync(employee.Email, $"Leave Allocation posted for period {period}", $"Your {leaveType.Name} leave " +
                    $"has been posted for the period of {period}. You have been given {leaveType.DefaultDays} days." );
            }

        }

        public async Task<bool> UpdateEmployeeAllocation(LeaveAllocationEditVM model)
        {
            var allocation = await GetAsync(model.Id);
            if (allocation == null)
            {
                return false;
            }
            allocation.Period = model.Period;
            allocation.NumberOfDays = model.NumberOfDays;
            await UpdateAsync(allocation);
            return true;
        }

        public async Task<LeaveAllocation?> GetEmployeeAllocation(string employeeId, int leaveTypeId)
        {
            return await context.LeaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == employeeId && q.LeaveTypeId == leaveTypeId);
        }
    }
}
