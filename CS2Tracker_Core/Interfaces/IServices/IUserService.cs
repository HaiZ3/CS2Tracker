using CS2Tracker_Core.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Interfaces.IServices
{
    public interface IUserService
    {
        Task<ServiceResult> LoginUserAsync(string username, string password);
        Task<ServiceResult> CreateUserAsync(string username, string email, string password);
        
    }
}
