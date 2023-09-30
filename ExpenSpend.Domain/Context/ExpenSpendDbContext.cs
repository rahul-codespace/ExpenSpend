﻿using ExpenSpend.Domain.Models.Expenses;
using ExpenSpend.Domain.Models.Friends;
using ExpenSpend.Domain.Models.GroupMembers;
using ExpenSpend.Domain.Models.Groups;
using ExpenSpend.Domain.Models.Payments;
using ExpenSpend.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenSpend.Domain.Context
{
    public class ExpenSpendDbContext : IdentityDbContext<ESUser,IdentityRole<Guid>,Guid>
    {
        public DbSet<ESUser> Users { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public ExpenSpendDbContext(DbContextOptions<ExpenSpendDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Friendship>(b =>
            {
                // Set up the many-to-many relationship for ESUser and Friendship
                b.HasOne(f => f.Initiator)
                 .WithMany()
                 .HasForeignKey(f => f.InitiatorId)
                 .OnDelete(DeleteBehavior.Cascade)
                 .IsRequired().HasPrincipalKey(u => u.Id);

                b.HasOne(f => f.Recipient)
                 .WithMany()
                 .HasForeignKey(f => f.RecipientId)
                 .OnDelete(DeleteBehavior.Cascade)
                 .IsRequired().HasPrincipalKey(u => u.Id);

                // Ensure that InitiatorId and RecipientId combination is unique
                b.HasIndex(f => new { f.InitiatorId, f.RecipientId }).IsUnique();

                // Ensure that a user cannot be friends with themselves
                b.HasCheckConstraint("CK_Friendship_InitiatorId_RecipientId", "\"InitiatorId\" != \"RecipientId\"");
            });

            builder.Entity<Group>(b =>
            {
                b.HasMany(b => b.Members)
                 .WithOne(b=>b.Group)
                 .HasForeignKey(m => m.GroupId)
                 .OnDelete(DeleteBehavior.Cascade)
                 .IsRequired().HasPrincipalKey(g => g.Id);

                b.HasMany(b => b.Expenses)
                .WithOne(b => b.Group)
                    .HasForeignKey(e => e.GroupId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired().HasPrincipalKey(g => g.Id);
            });

            builder.Entity<GroupMember>(b =>
            {
                b.HasOne(b => b.User)
                 .WithMany()
                 .HasForeignKey(m => m.UserId)
                 .OnDelete(DeleteBehavior.Cascade)
                 .IsRequired().HasPrincipalKey(u => u.Id);
            });

            builder.Entity<Expense>(b =>
            {
                b.HasMany(b => b.Payments)
                 .WithOne(b => b.Expense)
                 .HasForeignKey(p => p.ExpenseId)
                 .OnDelete(DeleteBehavior.Cascade)
                 .IsRequired().HasPrincipalKey(e => e.Id);
            });

            builder.Entity<Payment>(b =>
            {
                b.HasOne(b => b.OwenedBy)
                 .WithMany()
                 .HasForeignKey(p => p.OwenedById)
                 .OnDelete(DeleteBehavior.Cascade)
                 .IsRequired().HasPrincipalKey(u => u.Id);
            });

        }
    }
}
