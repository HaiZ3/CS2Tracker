using CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs.MarketPricesDTOs;
using CS2Tracker_Core.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Interfaces.IServices
{
    public interface IMarketplacePriceService 
    {
        public Task<ServiceResultGeneric<SkinPriceDTO>> GetSkinMarketplacePrice(string marketHashName);
    }
}
