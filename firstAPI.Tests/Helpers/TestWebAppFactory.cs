using FirstAPI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace firstAPI.Tests.Helpers
{
    public class TestWebAppFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Add JWT config for testing
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Jwt:Key"] = "TestSuperSecretKeyThatIsAtLeast32CharactersLong!",
                    ["Jwt:Issuer"] = "FirstAPI",
                    ["Jwt:Audience"] = "FirstAPIUsers"
                });
            });

            builder.ConfigureServices(services =>
            {
                // Remove everything DB related
                var toRemove = services.Where(d =>
                    d.ServiceType.FullName != null &&
                    d.ServiceType.FullName.Contains("EntityFrameworkCore"))
                    .ToList();

                foreach (var d in toRemove)
                    services.Remove(d);

                // Add in-memory DB
                services.AddDbContext<FirstAPIContext>(options =>
                    options.UseInMemoryDatabase("TestDb"));

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<FirstAPIContext>();
                db.Database.EnsureCreated();
            });
        }
    }
}

