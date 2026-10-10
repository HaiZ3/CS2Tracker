using BCrypt.Net;
using CS2Tracker_Core.Models;
using CS2Tracker_Core.Context;
using CS2Tracker_Core.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS2Tracker_Core.Services
{
    public class UserService : IUserService
    {
        private CS2TrackerContext _context;
        public UserService(CS2TrackerContext context)
        {
            this._context = context;
        }
        public async Task<ServiceResult> CreateUserAsync(string username,string email ,string password)
        {
            //check if a user with the same username exists
            if(await _context.Users.AnyAsync(u => u.Username == username))
            {
                return ServiceResult.Fail($"The username: {username} already exists");
            }
            if(username.Length < 3)
            {
                return ServiceResult.Fail("The username must be at least 3 characters long");
            }
            //hash the password with BCrypt
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password, 12);

            //add the user to the database
            User user = new User(username,email,passwordHash);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> LoginUserAsync(string username,string password)
        {
            //get the user
            User? user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user is null)
            {
                return ServiceResult.Fail("Such a user does not exist!");
            }
            //get the hashed password and verify it
            string hashedPassword = user.PasswordHash;
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            if(!isPasswordCorrect)
            {
                return ServiceResult.Fail("Incorrect password!");
            }
            //Add the user info to the session
            return ServiceResult.Ok(user.Id,user.Username);
        }
    }
}
