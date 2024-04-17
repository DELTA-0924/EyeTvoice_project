using System.ComponentModel.DataAnnotations;

namespace EyeTvoice_project.Entity.Models
{
    public class Lesson
    {                
        public int Id { get; set; }
        public string TitleName { get; set; }
        public string Imagepath { get; set; }
        public string VideoPath { get; set; }
    }
}
