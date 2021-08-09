using ARCLanguageApi.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARCLanguageApi.EntityFrameworkCore
{
    public class ARCLanguageContext:DbContext
    {
        public DbSet<ARCLANGUAGE> ARCLanguages { get; set; }

        public ARCLanguageContext(DbContextOptions<ARCLanguageContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ARCLANGUAGE>().ToTable("ARCLANGUAGE");
        }
    }
}
