using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_DTOs.DTOs.ApiDTOs.SheetDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CS2Tracker_MVC.Controllers
{
    [Authorize]
    public class SheetsController : Controller
    {
        private readonly ISheetService _sheetService;
        public SheetsController(ISheetService sheetService)
        {
            _sheetService = sheetService;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var result = await _sheetService.GetSheetsIdsByUserId(userId);

            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }

            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddNewSheetDTO model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            if (!ModelState.IsValid!)
            {
                return View(model);
            }

            var res = await _sheetService.CreateSheetAsync(userId, model.Name, model.SheetType);

            if (!res.Success)
            {
                TempData["ErrorMessage"] = "Failed to create sheet.";
                return RedirectToAction(nameof(All));
            }

            TempData["SuccessMessage"] = $"Sheet: {model.Name} created!";

            return RedirectToAction(nameof(All));
        }
    }
}
