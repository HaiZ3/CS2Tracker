using CS2Tracker_Common.Responses;
using CS2Tracker_DTOs.SharedDTOs.DashBoardDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Interfaces.IServices
{
    public interface IDashboardService
    {
        public Task<ServiceResultGeneric<DashBoardSummaryDTO>> GetSheetDashboardAsync(string userId);

    }
}
