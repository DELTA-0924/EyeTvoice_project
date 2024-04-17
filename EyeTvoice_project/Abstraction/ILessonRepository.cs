using CSharpFunctionalExtensions;
using EyeTvoice_project.Entity.Models;

namespace EyeTvoice_project.Abstraction
{
    public interface ILessonRepository
    {
        Task<Result<Lesson>> GetLessonById(int id);
        Task<Result<List<Lesson>>> GetLessons();
    }
}
