using ExpenSpend.Domain.Models.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenSpend.Domain.Context
{
    public class ExpenSpendDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ExpenSpendDbContext>();

                context.Database.EnsureCreated();


                // Seed Roles
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(new List<IdentityRole>()
                    {
                        new IdentityRole() {Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin"},
                        new IdentityRole() {Name = "User", ConcurrencyStamp = "2", NormalizedName = "User"}
                    });
                    context.SaveChanges();
                }

                // Seed Users

                var hasher = new PasswordHasher<User>();
                if (!context.Users.Any())
                {
                    context.Users.AddRange(new List<User>()
                    {
                        new User() {
                            Email = "admin@asp.net",
                            FirstName = "Admin",
                            LastName = "User",
                            UserName = "admin",
                            NormalizedUserName = "ADMIN",
                            ConcurrencyStamp ="1",
                            EmailConfirmed = true,
                            LockoutEnabled = true,
                            PasswordHash = hasher.HashPassword(null,"Admin@123")
                        },
                        new User() {
                            Email = "user@asp.net",
                            FirstName = "User",
                            LastName = "User",
                            UserName = "user",
                            NormalizedUserName = "USER",
                            ConcurrencyStamp ="2",
                            EmailConfirmed = true,
                            LockoutEnabled = true,
                            PasswordHash = hasher.HashPassword(null, "User@123")
                        } 
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
