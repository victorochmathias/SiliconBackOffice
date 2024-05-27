using Microsoft.EntityFrameworkCore;
using SiliconBackOffice.Contexts;
using SiliconBackOffice.Data;
using SiliconBackOffice.Entities;
using System.Diagnostics;
using static SiliconBackOffice.Components.Pages.Editpage;

namespace SiliconBackOffice.Services;

public class UserService(DataContext context)
{
    private readonly DataContext _context = context;



    //GET
    public async Task<IEnumerable<AspNetUser>> GetAllUsersAsync()
    {
        try
        {
            
            var users = await _context.AspNetUsers.ToListAsync();
            if (users != null && users.Any())
            {
                return users;
            }
        }
        catch (Exception ex)
        { 
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        
        return Enumerable.Empty<AspNetUser>();
    }

    public async Task<AspNetUser> GetUserByIdAsync(string userId)
    {
        try
        {
            var entity = await _context.AspNetUsers.FirstOrDefaultAsync(x => x.Id == userId);
            if (entity != null) 
            {
                return entity;
            }
        }
        catch (Exception ex)
        {
            
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return null!;
    }

    public async Task<AspNetUser> GetUserByEmailAsync(string email)
    {
        try
        {
            var entity = await _context.AspNetUsers.FirstOrDefaultAsync(x => x.Email == email);
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex)
        {

            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return null!;
    }





    //UPDATE

    public async Task<bool> UpdateUserAsync(string userId, UpdateFormModel model)
    {
        var user = await _context.AspNetUsers
            .Include( u => u.UserProfile)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        user.Email = model.Email;
        user.UserName = model.Email;
        user.PhoneNumber = model.Phone;

        if (user.UserProfile != null)
        {
            user.UserProfile.FirstName = model.FirstName;
            user.UserProfile.LastName = model.LastName;
        }

        _context.Entry(user).State = EntityState.Modified;
        if (user.UserProfile != null)
        {
            _context.Entry(user.UserProfile).State = EntityState.Modified;
        }

        var result = await _context.SaveChangesAsync();
        return true;
    }


    //DELETE
    public async Task<bool> DeleteUserAsync(string userId)
    {
        try
        {
            var user = await _context.AspNetUsers.FindAsync(userId);
            if (user != null)
            {
                _context.AspNetUsers.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
            return false;
        }

        return false;
    }


    //ÖVRIGT


    private async Task<Guid> GenerateNewUserProfileId()
    {
        return await Task.FromResult(Guid.NewGuid()); // Returnera en ny GUID
    }
}
