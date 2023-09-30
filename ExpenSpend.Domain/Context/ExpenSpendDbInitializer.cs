using ExpenSpend.Domain.Models.Expenses;
using ExpenSpend.Domain.Models.GroupMembers;
using ExpenSpend.Domain.Models.Groups;
using ExpenSpend.Domain.Models.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenSpend.Domain.Context
{
    public class ExpenSpendDbInitializer
    {
        public static async void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ExpenSpendDbContext>();

                context.Database.EnsureCreated();


                // Seed Roles
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(new List<IdentityRole<Guid>>()
                    {
                        new IdentityRole<Guid>() {Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin"},
                        new IdentityRole<Guid>() {Name = "User", ConcurrencyStamp = "2", NormalizedName = "User"}
                    });
                    await context.SaveChangesAsync();
                }

                // Seed Users

                var hasher = new PasswordHasher<ESUser>();
                if (!context.Users.Any())
                {
                    context.Users.AddRange(new List<ESUser>()
                    {
                        new ESUser() {
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
                        new ESUser() {
                            Email = "user@asp.net",
                            FirstName = "User",
                            LastName = "User",
                            UserName = "user",
                            NormalizedUserName = "USER",
                            ConcurrencyStamp ="2",
                            EmailConfirmed = true,
                            LockoutEnabled = true,
                            PasswordHash = hasher.HashPassword(null, "User@123")
                        },
                        new ESUser()
                        {
                            Email = "rahul@gmail.com",
                            FirstName = "Rahul",
                            LastName = "Kumar",
                            UserName = "rahul",
                            NormalizedUserName = "RAHUL",
                            ConcurrencyStamp = "3",
                            EmailConfirmed = true,
                            LockoutEnabled = true,
                            PasswordHash = hasher.HashPassword(null, "Rahul@123")
                        },
                        new ESUser()
                        {
                            Email = "aditya@gmail.com",
                            FirstName = "Aditya",
                            LastName = "Kumar",
                            UserName = "aditya",
                            NormalizedUserName = "ADITYA",
                            ConcurrencyStamp = "4",
                            EmailConfirmed = true,
                            LockoutEnabled = true,
                            PasswordHash = hasher.HashPassword(null, "Aditya@123")
                        }
                    });
                    await context.SaveChangesAsync();
                }

                // Get Users

                var adminUser = await context.Users.FirstOrDefaultAsync(x => x.UserName == "admin");
                var userUser = await context.Users.FirstOrDefaultAsync(x => x.UserName == "user");
                var rahulUser = await context.Users.FirstOrDefaultAsync(x => x.UserName == "rahul");
                var adityaUser = await context.Users.FirstOrDefaultAsync(x => x.UserName == "aditya");

                // Seed UserRoles
                if (!context.UserRoles.Any())
                {
                    var adminRole = await context.Roles.FirstOrDefaultAsync(x => x.Name == "Admin");
                    var userRole = await context.Roles.FirstOrDefaultAsync(x => x.Name == "User");

                    context.UserRoles.AddRange(new List<IdentityUserRole<Guid>>{
                        new IdentityUserRole<Guid>{ RoleId = adminRole.Id, UserId = adminUser.Id},
                        new IdentityUserRole<Guid>{ RoleId = userRole.Id, UserId = userUser.Id},
                        new IdentityUserRole<Guid>{ RoleId = userRole.Id, UserId = rahulUser.Id},
                        new IdentityUserRole<Guid>{ RoleId = userRole.Id, UserId = adityaUser.Id}
                    });
                    await context.SaveChangesAsync();
                }

                // Seed Groups
                if (!context.Groups.Any())
                {
                    context.Groups.AddRange(new List<Group>()
                    {
                        new Group()
                        {
                            Name = "Group 1",
                            About = "Group 1 Description",
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now
                        },
                        new Group()
                        {
                            Name = "Group 2",
                            About = "Group 2 Description",
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now
                        },
                        new Group()
                        {
                            Name = "Group 3",
                            About = "Group 3 Description",
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now
                        },
                        new Group()
                        {
                            Name = "Group 4",
                            About = "Group 4 Description",
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now
                        }
                    });
                    await context.SaveChangesAsync();
                }

                // Get Groups
                var group1 = await context.Groups.FirstOrDefaultAsync(x => x.Name == "Group 1");
                var group2 = await context.Groups.FirstOrDefaultAsync(x => x.Name == "Group 2");
                var group3 = await context.Groups.FirstOrDefaultAsync(x => x.Name == "Group 3");
                var group4 = await context.Groups.FirstOrDefaultAsync(x => x.Name == "Group 4");

                // Seed GroupMebmers

                if (!context.GroupMembers.Any())
                {
                    context.GroupMembers.AddRange(new List<GroupMember>()
                    {
                        new GroupMember()
                        {
                            GroupId = group1.Id,
                            UserId = adminUser.Id,
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now,
                        },
                        new GroupMember()
                        {
                            GroupId = group1.Id,
                            UserId = userUser.Id,
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now
                        },
                        new GroupMember()
                        {
                            GroupId = group1.Id,
                            UserId = rahulUser.Id,
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now
                        },
                        new GroupMember()
                        {
                            GroupId = group1.Id,
                            UserId = adityaUser.Id,
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now
                        },
                        new GroupMember()
                        {
                            GroupId = group2.Id,
                            UserId = adminUser.Id,
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now
                        },
                        new GroupMember()
                        {
                            GroupId = group2.Id,
                            UserId = userUser.Id,
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now
                        },
                        new GroupMember()
                        {
                            GroupId = group2.Id,
                            UserId = rahulUser.Id,
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now
                        },
                        new GroupMember()
                        {
                            GroupId = group2.Id,
                            UserId = adityaUser.Id,
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now
                        }
                    });
                    await context.SaveChangesAsync();
                }

                // Seed Expenses
                if (!context.Expenses.Any())
                {
                    context.Expenses.AddRange(new List<Expense>()
                    {
                        new Expense()
                        {
                            GroupId = group1.Id,
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now,
                            Amount = 100,
                            Title = "Expense 1",
                            Description = "Expense 1 Description",
                            PaidById = adminUser.Id,
                            SplitAs = SplitAs.Equally,
                            IsSettled = false
                        },
                        new Expense()
                        {
                            GroupId = group1.Id,
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now,
                            Amount = 200,
                            Title = "Expense 2",
                            Description = "Expense 2 Description",
                            PaidById = userUser.Id,
                            SplitAs = SplitAs.Equally,
                            IsSettled = false
                        },
                        new Expense()
                        {
                            GroupId = group1.Id,
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now,
                            Amount = 300,
                            Title = "Expense 3",
                            Description = "Expense 3 Description",
                            PaidById = rahulUser.Id,
                            SplitAs = SplitAs.Equally,
                            IsSettled = false
                        },
                        new Expense()
                        {
                            GroupId = group1.Id,
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now,
                            Amount = 400,
                            Title = "Expense 4",
                            Description = "Expense 4 Description",
                            PaidById = adityaUser.Id,
                            SplitAs = SplitAs.Equally,
                            IsSettled = false
                        },
                        new Expense()
                        {
                            GroupId = group2.Id,
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now,
                            Amount = 500,
                            Title = "Expense 5",
                            Description = "Expense 5 Description",
                            PaidById = adminUser.Id,
                            SplitAs = SplitAs.Equally,
                            IsSettled = false
                        },
                        new Expense()
                        {
                            GroupId = group2.Id,
                            CreatedBy = adminUser.Id,
                            CreatedAt = DateTime.Now,
                            Amount = 600,
                            Title = "Expense 6",
                            Description = "Expense 6 Description",
                            PaidById = userUser.Id,
                            SplitAs = SplitAs.Equally,
                            IsSettled = false
                        }
                    });
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
