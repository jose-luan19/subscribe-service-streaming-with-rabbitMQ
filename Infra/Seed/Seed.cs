using Microsoft.EntityFrameworkCore;
using Models;

namespace Infra.Seed
{
    public static class Seed
    {
        public static void GenerateSeed(this ModelBuilder model)
        {
            SeedStatus(model);
        }

        private static void SeedStatus(ModelBuilder model)
        {
            model.Entity<Status>().HasData(new Status() { Id = Guid.Parse("c35834ae-6acf-45d8-9f75-95ff9035bee3"), StatusName = "Enabled" });
            model.Entity<Status>().HasData(new Status() { Id = Guid.Parse("03f4ec06-8cc0-4e9f-af16-4b40d912fac1"), StatusName = "Disabled" });
        }
    }
}
