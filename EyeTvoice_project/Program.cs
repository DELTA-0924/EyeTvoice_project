using EyeTvoice_project.Abstraction;
using EyeTvoice_project.Contact;
using EyeTvoice_project.Entity;
using EyeTvoice_project.Repository;
using EyeTvoice_project.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options=>options.AddPolicy("AllowAnything"
                                                    ,buider=>buider.AllowAnyOrigin()
                                                    .AllowAnyMethod()
                                                    .AllowAnyHeader()                                                    
                                                    )    
                        );
builder.Services.AddDbContext<LessonDbContext>();
builder.Services.AddScoped<IServiceLessons, ServiceLesson>();
builder.Services.AddScoped<ILessonRepository,LessonRepository>();


var app = builder.Build();

app.UseCors("AllowAnything");

    
app.MapGet("/", (LessonDbContext db) => db.LessonEntity.ToList());

app.MapGet("/get-lesson/{id}", GetLesson);
async Task<IResult> GetLesson(IServiceLessons servicelesson, HttpContext context, int id){
    var result = await servicelesson.getLesson(id);
    if (result.IsFailure)
    {
        context.Response.StatusCode = 500;

        return Results.BadRequest(context);
    }
    
    // Отправить успешный результат в виде JSON
    return Results.Ok(result.Value);
}
app.MapGet("/get-lessons/", GetLessons);


async Task<IResult> GetLessons(IServiceLessons servicelesson, HttpContext context)  {
    int i = 1;
    var result = await servicelesson.getLessons();
    if (result.IsFailure)
    {
        // Отправить статус ошибки с сообщением
        context.Response.StatusCode = 500; // Например, Internal Server Error

        return Results.BadRequest(context);
    }
    List<Responce>responce = new List<Responce>();
    // Отправить успешный результат в виде JSON
    return Results.Ok(result.Value);
};
app.MapGet("/extract-audio/", (IServiceLessons servicelesson) =>
{
    servicelesson.createAudio();
});
app.Run();
