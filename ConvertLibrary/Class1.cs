using CSharpFunctionalExtensions;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace ConvertLibrary
{
    class Responce
    {
        public string text { get; set; }
    }
    public class Convert_lesson
    {
        
        static private readonly string path = @"F:\Users\Scorp\video_mp4\Даяр\videos";
        static private readonly string path1 = @"F:\Users\Scorp\video_mp4\Даяр\videos\audios";
            
        public static void video_audio(string filename)
        {
            var startcmd = new ProcessStartInfo()
            {
                FileName = "cmd",
                Arguments = $@"/k""ffmpeg -i {filename}.mp4 -map 0:1 audio-{filename}.mp3""",
                WorkingDirectory = $"{path}",
                UseShellExecute = true
            };
            var process = Process.Start(startcmd);                        
        } 
        public static void trim_audio(string filename)
        {
            var inputFilePath = Path.Combine(path, $"audio-{filename}.mp3");
            var startcmd = new ProcessStartInfo()
            {
                FileName = "cmd",
                Arguments = $@"/k""ffmpeg -i {inputFilePath} -acodec copy -ss 00:00:05 -t 00:00:30 trim-{filename}.mp3""",
                WorkingDirectory = $"{path1}",
                UseShellExecute = false
            };
            Process.Start(startcmd);
        }
        public static void delete_cach()
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $@"/c del /s /q ""{path1}\*.*""",
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                UseShellExecute = false,
            };

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.Start();               
            }
        }
        public static async Task<Result<string>> AudioToText(string filename)
        {
            using (var client =new HttpClient())
            {
                string url = "https://asr.ulut.kg/api/receive_data";
                string audio_file_path = Path.Combine(path1, "trim-"+filename+".mp3");
                 Console.WriteLine(audio_file_path);
                var form = new MultipartFormDataContent();
                byte[] fileBytes = File.ReadAllBytes(audio_file_path);
                form.Add(new ByteArrayContent(fileBytes), "audio", Path.GetFileName(audio_file_path));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "85055d095d3c5bfc638db36a75b14e3cf93ec648a7b666fd67df81af1d256e3deb2d50bf1168074551b0410fc68a3546c15643958925c17c6d56663e6bdc7673");
                HttpResponseMessage responce = new HttpResponseMessage() ;
                Console.WriteLine("Начало отправки запроса.");
                try
                {
                     responce = await client.PostAsync(url, form);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return Result.Failure<string>(ex.Message);
                }
                Console.WriteLine("Запрос успешно отправлен.");                                                           
                if (responce==null)
                    Console.WriteLine("responce null");
                if (!responce.IsSuccessStatusCode)
                    return Result.Failure<string>(responce.StatusCode.ToString());
                    
                var responseContent = await responce.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<Responce>(responseContent, new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
                //Console.WriteLine(responseData.text);
                return Result.Success(responseData.text);                        
            }            
        }
    }

}
