using Microsoft.EntityFrameworkCore;
using NotasUnivoDev.Models;

namespace NotasUnivoDev.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FacultiesModel> Faculties { get; set; }
        public DbSet<CareersModel> Careers { get; set; }
        public DbSet<SubjectsModel> Subjects { get; set; }
    }
}
