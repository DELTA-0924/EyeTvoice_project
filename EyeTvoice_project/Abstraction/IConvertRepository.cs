using CSharpFunctionalExtensions;

namespace EyeTvoice_project.Abstraction
{
    public interface IConvertRepository
    {
        Task<Result> ConverVideoToAudio();
        Task<Result<string>> ConverAudiotoText();
    }
}
