using LupExercise_ArchieGonzales.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LupExercise_ArchieGonzales.DAL
{
    public class ApplicationDbContext : IdentityDbContext<Users, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {

        }

        public DbSet<Events> Events { get; set; }
    }
}
