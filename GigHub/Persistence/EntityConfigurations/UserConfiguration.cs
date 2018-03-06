using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Persistence.EntityConfigurations {
    public class UserConfiguration : EntityTypeConfiguration<ApplicationUser> {
        public UserConfiguration() {
            Property(u => u.Name)
             .IsRequired()
             .HasMaxLength(100);

            HasMany(u => u.Followers)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);

            HasMany(u => u.Followees)
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false);

            HasMany(u => u.UserNotifications)
                .WithRequired(un => un.User)
                .WillCascadeOnDelete(false);
        }
    }
}