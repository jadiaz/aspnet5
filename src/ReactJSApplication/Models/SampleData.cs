using System;
using System.Threading.Tasks;
using Microsoft.Framework.DependencyInjection;

namespace ReactJsApplication.Models
{
    public static class SampleData
    {
        public static async Task InitializeUserTableAsync(IServiceProvider appServices)
        {
            using( var db = appServices.GetService<AppDbContext>())
            {
                db.Add(new User { Name = "User 1" });
                db.Add(new User { Name = "User 2" });
                db.Add(new User { Name = "User 3" });
                await db.SaveChangesAsync();
            }
        }
    }
}