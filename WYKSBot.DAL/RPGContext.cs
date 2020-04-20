using WYKSBot.DAL.Models.Items;
using WYKSBot.DAL.Profiles;
using Microsoft.EntityFrameworkCore;

namespace WYKSBot.DAL
{
    public class RPGContext : DbContext
    {
        public RPGContext(DbContextOptions<RPGContext> options) : base(options) { }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ProfileItem> ProfileItems { get; set; }
    }
}
