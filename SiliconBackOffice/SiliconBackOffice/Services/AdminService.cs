using Microsoft.EntityFrameworkCore;
using SiliconBackOffice.Contexts;
using SiliconBackOffice.Data;
using SiliconBackOffice.Entities;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using static SiliconBackOffice.Components.Pages.EditAdmin;

namespace SiliconBackOffice.Services;

public class AdminService(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;



    //CREATE //behövs inte
    //public async Task<bool> CreateAdmin(string email, string hashedPassword)
    //{
    //    var existingAdmin = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    //    if (existingAdmin == null)
    //    {
    //        var entity = new ApplicationUser
    //        {
    //            Email = email,
    //            UserName = email,
    //            PasswordHash = hashedPassword,
    //            EmailConfirmed = true

    //        };

    //        var result = await _context.Users.AddAsync(entity);
    //        if (result != null)
    //        {
    //            await _context.SaveChangesAsync();
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }


    //    }

    //    return false;
    //}




    //GET
    public async Task<IEnumerable<ApplicationUser>> GetAllAdminsAsync()
    {
        try
        {

            
            var admins = await _context.Users.ToListAsync();
            if (admins != null && admins.Any())
            {
                return admins;

            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return Enumerable.Empty<ApplicationUser>();
    }

    public async Task<ApplicationUser> GetAdminByEmailAsync(string email)
    {
        try
        {
            var entity = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
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

    public async Task<ApplicationUser> GetAdminByIdAsync(string userId)
    {
        try
        {
            var entity = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
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


    //DELETE
    public async Task<bool> DeleteAdminAsync(string userId)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
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

    //UPDATE
    public async Task<bool> UpdateAdminAsync(string userId, UpdateFormModel model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        user.Email = model.Email;
        user.UserName = model.Email;

        _context.Entry(user).State = EntityState.Modified;
        var result = await _context.SaveChangesAsync();
        return true;
    }




    //ÖVRIGT

    public string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert byte array to a string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
