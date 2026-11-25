using CS2Tracker_Common.Responses;
using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_DTOs.SharedDTOs.DashBoardDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CS2Tracker_MVC.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashBoardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            ServiceResultGeneric<DashBoardSummaryDTO> res = await _dashboardService.GetSheetDashboardAsync(userId);

            if (!res.Success)
            {
                return NotFound(res.ErrorMessage);
            }

            return View(res.Data);
        }
    }
}
