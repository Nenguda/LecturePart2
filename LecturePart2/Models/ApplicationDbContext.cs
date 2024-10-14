using Microsoft.EntityFrameworkCore;

namespace LecturePart2.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ProgramCoordinator> Coordinators { get; set; }
        public DbSet<ProgramManager> Managers { get; set; }
    }
}

