using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Interfaces.IServices
{
    public interface ITokenService
    {
        string CreateToken(int userId, string username);
    }
}
