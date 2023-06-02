using AutoMapper;
using LeaveManagement.Web.Constants;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;
using LeaveManagement.Web.Services_Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Repositories___Services
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Employee> userManager;
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public LeaveAllocationRepository(ApplicationDbContext context,
            UserManager<Employee> userManager,
            IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository) : base(context)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
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
                .ToListAsync();
            var employee = await userManager.FindByIdAsync(employeeId);

            var employeeAllocationModel = mapper.Map<EmployeeAllocationVM>(employee);

            employeeAllocationModel.LeaveAllocations = mapper.Map<List<LeaveAllocationVM>>(allocations);

            return employeeAllocationModel;

        }

        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation (int allocationId)
        {
            var allocation = await context.LeaveAllocations
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == allocationId);
            if (allocation == null)
            {
                return null;
            }

            var employee = await userManager.FindByIdAsync (allocation.EmployeeId);
            var leaveAllocationEditVM = mapper.Map<LeaveAllocationEditVM>(allocation);
            leaveAllocationEditVM.Employee = mapper.Map<EmployeesListVM>(employee);
            return leaveAllocationEditVM;
        }

        public async Task LeaveAllocation(int leaveTypeId)
        {
            var employees = await userManager.GetUsersInRoleAsync(Roles.User);
            var period = DateTime.Now.Year;
            var leaveType = await leaveTypeRepository.GetAsync(leaveTypeId);
            var allocations = new List<LeaveAllocation>();

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
            }
            await AddRangeAsync(allocations);

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
