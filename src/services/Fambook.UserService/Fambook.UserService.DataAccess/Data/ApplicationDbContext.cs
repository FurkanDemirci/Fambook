using System;
using System.Collections.Generic;
using System.Text;
using Fambook.UserService.Models;
using Microsoft.EntityFrameworkCore;

namespace Fambook.UserService.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Profile> Profile { get; set; }
    }
}
