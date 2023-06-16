using LeaveManagement.Common.Constants;
using LeaveManagement.Application.Contracts;
using LeaveManagement.Data;
using LeaveManagement.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Controllers
{
    [Authorize]
    public class LeaveRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly ILogger<LeaveRequestsController> logger;

        public LeaveRequestsController(ApplicationDbContext context,
            ILeaveRequestRepository leaveRequestRepository,
            ILogger<LeaveRequestsController> logger
            )
        {
            _context = context;
            this.leaveRequestRepository = leaveRequestRepository;
            this.logger = logger;
        }
        // GET: LeaveRequests
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Index()
        {
            var model = await leaveRequestRepository.GetAdminLeaveRequestList();
            return View(model);
            //var applicationDbContext = _context.LeaveRequests.Include(l => l.LeaveType).Include(l => l.RequestingEmployee);
            //return View(await applicationDbContext.ToListAsync());

        }
        public async Task<ActionResult> MyLeaves()
        {
            var model = await leaveRequestRepository.GetMyLeaveRequestDetails();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            //We don't delete the request, just add cancelled flag
            try
            {
                await leaveRequestRepository.CancelRequest(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error canceling request");
                throw;
            }
            return RedirectToAction(nameof(MyLeaves));

        }

        // GET: LeaveRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var model = await leaveRequestRepository.GetLeaveRequestAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRequestApproval(int id, bool approved)
        {
            try
            {
                await leaveRequestRepository.ChangeRequestApproval(id, approved);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error approving request");
                throw;
            }
            return RedirectToAction("Index");
        }

        // GET: LeaveRequests/Create
        public IActionResult Create()
        {
            var model = new LeaveRequestCreateVM
            {
                LeaveTypes = new SelectList(_context.LeaveTypes, "Id", "Name"),
            };
            //ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            //ViewData["RequestingEmployeeId"] = new SelectList(_context.Users, "Id", "Id");
            return View(model);
        }

        // POST: LeaveRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestCreateVM leaveRequestVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isReqValid = await leaveRequestRepository.CreateLeaveRequest(leaveRequestVM);
                    if (isReqValid)
                    {
                        return RedirectToAction(nameof(MyLeaves));
                    }
                    ModelState.AddModelError(string.Empty, "Allocation limit is exceeded with this request");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating leave request");
                ModelState.AddModelError(string.Empty, "An error has occured. Please try again later");
            }
            leaveRequestVM.LeaveTypes = new SelectList(_context.LeaveTypes, "Id", "Name", leaveRequestVM.LeaveTypeId);
            //ViewData["RequestingEmployeeId"] = new SelectList(_context.Users, "Id", "Id", leaveRequestVM.RequestingEmployeeId);
            return View(leaveRequestVM);
        }

        // GET: LeaveRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LeaveRequests == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Id", leaveRequest.LeaveTypeId);
            ViewData["RequestingEmployeeId"] = new SelectList(_context.Users, "Id", "Id", leaveRequest.RequestingEmployeeId);
            return View(leaveRequest);
        }

        // POST: LeaveRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StartDate,EndDate,LeaveTypeId,RequestComments,Approved,Cancelled,RequestingEmployeeId,Id,DateCreated,DateUpdated")] LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveRequestExists(leaveRequest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Id", leaveRequest.LeaveTypeId);
            ViewData["RequestingEmployeeId"] = new SelectList(_context.Users, "Id", "Id", leaveRequest.RequestingEmployeeId);
            return View(leaveRequest);
        }

        // GET: LeaveRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LeaveRequests == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests
                .Include(l => l.LeaveType)
                .Include(l => l.RequestingEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        // POST: LeaveRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //if (_context.LeaveRequests == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.LeaveRequests'  is null.");
            //}
            //var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            //if (leaveRequest != null)
            //{
            //    _context.LeaveRequests.Remove(leaveRequest);
            //}

            //await _context.SaveChangesAsync();
            await leaveRequestRepository.DeleteAsync(id);

            return RedirectToAction(nameof(MyLeaves));
        }

        private bool LeaveRequestExists(int id)
        {
            return (_context.LeaveRequests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
