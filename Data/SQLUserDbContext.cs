using Microsoft.EntityFrameworkCore;
using WebConnectMongoDBAndSQL.Model;

namespace WebConnectMongoDBAndSQL.Data
{
    public class SQLUserDbContext : DbContext
    {
        public SQLUserDbContext(DbContextOptions<SQLUserDbContext> options): base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
