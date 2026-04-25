using EncurtadorUrl.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace EncurtadorUrl.Data.Context
{
    public class EncurtadorUrlDbContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }
        public string DbPath { get; }

        public EncurtadorUrlDbContext(DbContextOptions<EncurtadorUrlDbContext> options)
            : base(options)
        {
        }
    }
}
