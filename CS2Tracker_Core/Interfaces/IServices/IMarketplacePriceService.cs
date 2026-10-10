using CS2Tracker_Core.Models.DTOs.ApiDTOs.EntiresDTOs.MarketPricesDTOs;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models.Responses;
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
