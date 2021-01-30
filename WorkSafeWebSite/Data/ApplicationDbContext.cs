using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WorkSafeWebSite.Models;

namespace WorkSafeWebSite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WorkSafeWebSite.Models.WSNotice> WSNotice { get; set; }
        public DbSet<WorkSafeWebSite.Models.PCBU> PCBU { get; set; }
    }
}
