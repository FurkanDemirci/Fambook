using System;
using System.Collections.Generic;
using System.Text;
using Fambook.AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace Fambook.AuthService.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Credentials> Credentials { get; set; }
    }
}
