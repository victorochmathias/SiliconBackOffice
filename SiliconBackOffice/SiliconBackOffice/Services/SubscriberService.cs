using Microsoft.EntityFrameworkCore;
using SiliconBackOffice.Contexts;
using SiliconBackOffice.Data;
using SiliconBackOffice.Entities;
using System.Diagnostics;
using static SiliconBackOffice.Components.Pages.EditSubscriber;

namespace SiliconBackOffice.Services;

public class SubscriberService(DataContext context)
{
    private readonly DataContext _context = context;

    public async Task<IEnumerable<Subscriber>> GetAllSubscribersAsync()
    {
        try
        {

            var subs = await _context.Subscribers.ToListAsync();
            if (subs != null && subs.Any())
            {
                return subs;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return Enumerable.Empty<Subscriber>();
    }

    public async Task<Subscriber> GetSubscriberByEmailAsync(string email)
    {
        try
        {
            var entity = await _context.Subscribers.FirstOrDefaultAsync(x => x.Email == email);
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


    public async Task<bool> UpdateSubscriberAsync(string email, UpdateFormModel model)
    {
        var sub = await _context.Subscribers.FirstOrDefaultAsync(u => u.Email == email);

        if (sub == null)
        {
            return false;
        }

        sub.Email = model.Email;

        _context.Entry(sub).State = EntityState.Modified;
        var result = await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteSubscriberAsync(string email)
    {
        try
        {
            var sub = await _context.Subscribers.FindAsync(email);
            if (sub != null)
            {
                _context.Subscribers.Remove(sub);
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
}
