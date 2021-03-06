﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigHub.Core.Models;
using GigHub.Persistence;
using NUnit.Framework;

namespace GigHub.IntegrationTests {
    [SetUpFixture]
    public class GlobalSetup {
        [SetUp]
        public void SetUp() {
            MigrateDbToLatestVersion();
            Seed();
        }

        private static void MigrateDbToLatestVersion() {
            // Create and upgrade database to the lastest version if it's not already
            var configuration = new GigHub.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed() {
            var context = new ApplicationDbContext();

            if (context.Users.Any())
                return;

            context.Users.Add(new ApplicationUser { UserName = "user1", Name = "user1", Email = "-", PasswordHash = "-"});
            context.Users.Add(new ApplicationUser {UserName = "user2", Name = "user2", Email = "-", PasswordHash = "-"});
            context.SaveChanges();
        }
    }
}
