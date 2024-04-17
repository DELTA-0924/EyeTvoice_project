namespace EyeTvoice_project.Domain
{
    public class LessonDomain
    {
        public string TitleName { get; set; }
        public string? Theme { get; set; }
        public bool? Isblocked { get; set; }
        public byte[]Image { get; set; }
        public byte[]?Video { get; set; }
        public LessonDomain(string titlename,string? theme,byte[] image, byte[]?video,bool? isblocked) { 
            TitleName = titlename;
            Theme = theme;
            Image = image;
            Video = video;
            Isblocked = isblocked;
        }
    }
}
