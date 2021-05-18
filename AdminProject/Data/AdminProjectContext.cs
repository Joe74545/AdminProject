using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdminProject.Models;

namespace AdminProject.Data
{
    public class AdminProjectContext : DbContext
    {
        public AdminProjectContext (DbContextOptions<AdminProjectContext> options)
            : base(options)
        {
        }

        public DbSet<AdminProject.Models.AllMembers> AllMembers { get; set; }
    }
}
