using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_DTOs.SharedDTOs.EntriesDTOs;
using CS2Tracker_Models;
using CS2Tracker_MVC.ViewModels.Entries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CS2Tracker_MVC.Controllers
{
    [Authorize]
    public class EntriesController : Controller
    {
        private readonly IEntryService _entryService;
        public EntriesController(IEntryService entryService)
        {
            _entryService = entryService;
        }

        [HttpGet]
        public IActionResult Add(int sheetId)
        {
            return View(new CreateEntryViewModel { SheetId = sheetId });
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Add(CreateEntryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var res = await _entryService.AddEntryAsync(userId, model.ToDto());

            if (!res.Success)
            {
                TempData["ErrorMessage"] = res.ErrorMessage;
                return View(model);
            }

            TempData["SuccessMessage"] = $"{model.SkinName} added to the sheet";

            return RedirectToAction("Details", "Sheets", new { id = model.SheetId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int sheetId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var result = await _entryService.CancelEntryAsync(userId,id);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Entry deleted.";
            }
            else
            {
                TempData["ErrorMessage"] = result.ErrorMessage;
            }

            return RedirectToAction("Details", "Sheets", new { id = sheetId });
        }
        [HttpGet]
        public async Task<IActionResult> Close(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var res = await _entryService.GetEntryByIdAsync(userId, id);

            if (!res.Success)
            {
                return RedirectToAction("Details", "Sheets");
            }

            CloseEntryViewModel model = new CloseEntryViewModel()
            {
                EntryId = res.Data.Id,
                SheetId = res.Data.SheetId,
                Name = res.Data.Name,
                BuyPrice = res.Data.BuyPrice,
                DateBought = res.Data.DateBought,
                DateSold = DateTime.UtcNow
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Close(CloseEntryViewModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entry = await _entryService.GetEntryByIdAsync(userId,model.EntryId);

            if (!entry.Success)
            {
                TempData["ErrorMessage"] = entry.ErrorMessage;
                return RedirectToAction("Details", "Sheets");
            }

            model.Name = entry.Data.Name;
            model.DateBought = entry.Data.DateBought;
            model.BuyPrice = entry.Data.BuyPrice;
            model.SheetId = entry.Data.SheetId;


            CloseEntryDTO close = new CloseEntryDTO()
            {
                EntryId = entry.Data.Id,
                DateSold = model.DateSold,
                SellPrice = model.SellPrice,
            };

            var result = await _entryService.CloseEntryAsync(userId,close);

            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.ErrorMessage;
                return RedirectToAction("Details", "Sheets", new { id = model.SheetId });
            }

            TempData["SuccessMessage"] = $"{model.Name} closed!";

            return RedirectToAction("Details", "Sheets", new { id = model.SheetId });
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var res = await _entryService.GetEntryByIdAsync(userId, id);

            if (!res.Success)
            {
                TempData["ErrorMessage"] = res.ErrorMessage;
                return RedirectToAction("Details", "Sheets");
            }

            EditEntryViewModel model = new EditEntryViewModel()
            {
                EntryId = res.Data.Id,
                SheetId = res.Data.SheetId,
                Float = res.Data.SkinInfo.Float,
                Name = res.Data.Name,
                SkinCondition = res.Data.SkinInfo.SkinCondition,
                SkinVariant = res.Data.SkinInfo.SkinVariant,
                DateBought = res.Data.DateBought,
                BuyPrice = res.Data.BuyPrice
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditEntryViewModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var res = await _entryService.EditEntryAsync(userId, model.ToDto());

            if (!res.Success)
            {
                TempData["ErrorMessage"] = res.ErrorMessage;
                return RedirectToAction("Details", "Sheets");
            }

            return RedirectToAction("Details", "Sheets", new { id = model.SheetId });
        }

    }
}
