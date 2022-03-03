using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(AdoptPet.Areas.Identity.IdentityHostingStartup))]
namespace AdoptPet.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}