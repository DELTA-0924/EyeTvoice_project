
using EyeTvoice_project.Entity.Models;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace EyeTvoice_project.Entity
{
    public class LessonDbContext:DbContext
    {
        public DbSet<Lesson>LessonEntity { get; set; }
        public LessonDbContext(DbContextOptions<LessonDbContext>options):base(options) {             
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseMySql("server=localhost;user=root;password=tima12345;database=LessonDb;", new MySqlServerVersion(new Version(8, 0, 2)));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Lesson>().HasKey(l => l.Id);
            modelBuilder.Entity<Lesson>().HasData(
            new Lesson { Id = 1,  TitleName = "1-lesson", Imagepath= @"F:\Users\Scorp\video_mp4\Даяр\image\1-lesson.png" ,VideoPath= @"F:\Users\Scorp\video_mp4\Даяр\1-lesson.mp4" },
                new Lesson { Id = 2,  TitleName = "2-lesson", Imagepath = @"F:\Users\Scorp\video_mp4\Даяр\image\2-lesson.png" , VideoPath = @"F:\Users\Scorp\video_mp4\Даяр\2-lesson.mp4" },
                new Lesson { Id = 3, TitleName = "3-lesson" , Imagepath = @"F:\Users\Scorp\video_mp4\Даяр\image\3-lesson.png" ,VideoPath = @"F:\Users\Scorp\video_mp4\Даяр\3-lesson.mp4" },
                new Lesson { Id = 4, TitleName = "4-lesson" , Imagepath = @"F:\Users\Scorp\video_mp4\Даяр\image\4-lesson.png" ,VideoPath = @"F:\Users\Scorp\video_mp4\Даяр\4-lesson.mp4" }
                );
        }
    }
}
