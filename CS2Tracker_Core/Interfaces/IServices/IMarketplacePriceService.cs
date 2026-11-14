using CS2Tracker_Common.Responses;
using CS2Tracker_DTOs.ApiDTOs.EntriesDTOs.MarketPricesDTOs;
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
