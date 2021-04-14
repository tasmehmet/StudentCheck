using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentCheckApp.Models.DbModel;

namespace StudentCheckApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<Teachers>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Students> Students { get; set; }
        public DbSet<CheckDay> CheckDay { get; set; }
        public DbSet<Homeworks> Homeworks { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<TeacherStudent> TeacherStudent { get; set; }
    }
}
