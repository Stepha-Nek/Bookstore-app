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
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FirstAPI_data;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");

            return new FirstAPIContext(optionsBuilder.Options);
        }
    }
}
