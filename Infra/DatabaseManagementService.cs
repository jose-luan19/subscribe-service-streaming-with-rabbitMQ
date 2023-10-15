using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace desafioBack.Infra{

    public static class DatabaseManagementService {
        public static void MigrationInitialization(this IApplicationBuilder app){
            using (var serviceScope = app.ApplicationServices.CreateScope()){
                var serviceDb = serviceScope.ServiceProvider.GetService<DbContextClass>();
                serviceDb.Database.Migrate();
            }
        }
    }
}