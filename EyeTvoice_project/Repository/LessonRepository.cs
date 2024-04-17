using CSharpFunctionalExtensions;
using EyeTvoice_project.Abstraction;
using EyeTvoice_project.Entity;
using Microsoft.EntityFrameworkCore;


using EyeTvoice_project.Entity.Models;
using System.Reflection.Metadata.Ecma335;
namespace EyeTvoice_project.Repository
{
    public class LessonRepository : ILessonRepository
    {
        private readonly LessonDbContext _context;
        public LessonRepository(LessonDbContext context) {
            _context = context;
        }
        public async Task<Result<Lesson>> GetLessonById(int id)
        {
            Lesson lesson=new Lesson();
            try
            {
                lesson = await _context.LessonEntity.FirstOrDefaultAsync(l => l.Id == id);
            }catch (Exception ex)
            {
                return Result.Failure<Lesson>(ex.Message);
            }
            if (lesson == null)
                Console.WriteLine("null");
            return Result.Success(lesson);
        }

        public async Task<Result<List<Lesson>>> GetLessons()
        {
            List<Lesson>lessonsName=new List<Lesson>();
            try
            {
                lessonsName = await _context.LessonEntity.AsNoTracking().ToListAsync();
            }catch(Exception ex)
            {
               return Result.Failure<List<Lesson>>(ex.Message);
            }
            return Result.Success(lessonsName);
        }
    }
}
