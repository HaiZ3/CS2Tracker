using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_DTOs.DTOs.ApiDTOs.EntriesDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CS2Tracker_Api.Controllers
{
    [Route("api/entries")]
    [ApiController]
    [Authorize]
    public class EntriesController : ControllerBase
    {
        private readonly IEntryService _entryService;
        public EntriesController(IEntryService entryService)
        {
            _entryService = entryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEntryFromSheet(GetEntryDTO request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var entryRes = await _entryService.GetEntryBySheetIdAsync(userId, request.EntryId, request.SheetId);

            if (!entryRes.Success)
            {
                return NotFound(entryRes.ErrorMessage);
            }

            return Ok(entryRes.Data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEntry(CreateEntryDTO request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            try
            {
                var result = await _entryService.AddEntryAsync(userId,request);

                if (!result.Success)
                {
                    return BadRequest(result.ErrorMessage);
                }

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<IActionResult> CloseEntry(CloseEntryDTO request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var result = await _entryService.CloseEntryAsync(userId,request);

            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> CancelEntry(CancelEntryDTO request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var result = await _entryService.CancelEntryAsync(userId,request);

            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok();
        }
    }
}
