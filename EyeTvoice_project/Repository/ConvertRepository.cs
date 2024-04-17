//using ConvertLibrary;
//using CSharpFunctionalExtensions;
//using EyeTvoice_project.Abstraction;

//namespace EyeTvoice_project.Repository
//{
//    public class ConvertRepository:IConvertRepository
//    {
//        public async Task<Result<string>> ConverAudiotoText()
//        {

//            Convert_lesson.trim_audio("ss");
//            var result = await Convert_lesson.AudioToText("ss");
//            if (result.IsFailure)
//                return Result.Failure<string>(result.Error);
//            return Result.Success(result.Value);
//        }

//        public async Task<Result> ConverVideoToAudio()
//        {
//            try
//            {
//                ParallelLoopResult result = Parallel.ForEach<string>(
//              files_name,
//              Convert.video_audio
//              );
//            }
//            catch (Exception ex)
//            {
//                return Result.Failure(ex.Message);
//            }
//            return Result.Success();
//        }
//    }
//}
