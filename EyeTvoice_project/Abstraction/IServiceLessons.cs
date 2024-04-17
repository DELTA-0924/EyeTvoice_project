using CSharpFunctionalExtensions;
using EyeTvoice_project.Domain;

namespace EyeTvoice_project.Abstraction
{
    public interface IServiceLessons
    {
        Task<Result<LessonDomain>> getLesson(int id);
        Task<Result<List<LessonDomain>>> getLessons();
        Task createAudio();
    }
}
