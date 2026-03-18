using FirstAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstAPI.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FirstAPIContext>
    {
        public FirstAPIContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FirstAPIContext>();
            //changed from sql server to sqlite, cos i was having hosting issues with azure
            //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FirstAPI_data;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
            
            //using sqlite
            optionsBuilder.UseSqlite("Data Source=bookstore.db");

            return new FirstAPIContext(optionsBuilder.Options);
        }
    }
}
