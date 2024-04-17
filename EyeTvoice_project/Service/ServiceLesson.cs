using CSharpFunctionalExtensions;
using EyeTvoice_project.Abstraction;
using ConvertLibrary;
using static System.Formats.Asn1.AsnWriter;
using System.Net;
using System.Diagnostics;
using EyeTvoice_project.Domain;
namespace EyeTvoice_project.Service
{
    public class ServiceLesson : IServiceLessons
    {
        private readonly ILessonRepository _lessonRepository;
        public ServiceLesson(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task createAudio()
        {
            var result = await _lessonRepository.GetLessons();
            List<string> names = new List<string>();
            foreach (var item in result.Value)
                names.Add(item.TitleName);
            ParallelLoopResult result1 = Parallel.ForEach<string>(
             names,
             Convert_lesson.video_audio
             );
        }
        private SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        public async Task<Result<LessonDomain>> getLesson(int id)
        {
            await _lock.WaitAsync();
            try
            {
                var result = await _lessonRepository.GetLessonById(id);

                if (result.IsFailure)
                    return Result.Failure<LessonDomain>(result.Error);

                Convert_lesson.trim_audio(result.Value.TitleName);
                Thread.Sleep(3000); // Не рекомендуется использовать Thread.Sleep в асинхронном коде
                var content = await Convert_lesson.AudioToText(result.Value.TitleName);
                Convert_lesson.delete_cach();
                if (content.IsFailure)
                    return Result.Failure<LessonDomain>(content.Error);
                byte[] imageData = File.ReadAllBytes(result.Value.Imagepath);
                byte[] videoData = File.ReadAllBytes(result.Value.VideoPath);
                var lesson = new LessonDomain(result.Value.TitleName, content.Value, imageData, videoData, false);
                return Result.Success(lesson);
            }
            finally
            {
                _lock.Release();
            }
        }
        //public async Task<Result<LessonDomain>> getLesson(int id)
        //{
        //    var result = await _lessonRepository.GetLessonById(id);

        //    if (result.IsFailure)
        //        return Result.Failure<LessonDomain>(result.Error);

        //    Convert_lesson.trim_audio(result.Value.TitleName);
        //    Thread.Sleep(3000);
        //    var content = await Convert_lesson.AudioToText(result.Value.TitleName);
        //    Convert_lesson.delete_cach();
        //    if (content.IsFailure)
        //        return Result.Failure<LessonDomain>(content.Error);
        //    byte[] imageData = File.ReadAllBytes(result.Value.Imagepath);
        //    byte[] videoData = File.ReadAllBytes(result.Value.VideoPath);
        //    var lesson = new LessonDomain(result.Value.TitleName, content.Value, imageData, videoData, false);
        //    return Result.Success(lesson);
        //}

        public async Task<Result<List<LessonDomain>>> getLessons()
        {
            var result = await _lessonRepository.GetLessons();
            List<string> names = new List<string>();
            //foreach (var item in result.Value)
            //    names.Add(item.TitleName);
            //ParallelLoopResult result1 = Parallel.ForEach<string>(
            // names,
            // Convert_lesson.trim_audio
            // );
            //Thread.Sleep(5000);
            //List<string> contents = new List<string>();
            List<LessonDomain> lessons = new List<LessonDomain>();
            foreach (var item in result.Value)
            {
                //var _result = await Convert_lesson.AudioToText(item.TitleName);
                //contents.Add(_result.Value);                
                    byte[] imageData = File.ReadAllBytes(item.Imagepath);
                    lessons.Add(new LessonDomain(item.TitleName, null,imageData,null,false));                
            }
            Convert_lesson.delete_cach();
            return Result.Success(lessons);
        }
    }
}
