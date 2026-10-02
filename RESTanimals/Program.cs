using RESTanimals.Repos;

var builder = WebApplication.CreateBuilder(args);

const string AllowAllPolicy = "AllowAll";

// CORS – så en frontend (HTML/JS) må kalde API'et
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowAllPolicy, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Singleton = samme liste for alle requests, ellers forsvinder data efter hvert kald.
// INGEN database / connectionstring – kun en List.
builder.Services.AddSingleton<IAnimalsRepository>(new AnimalsRepositoryList(includesTestData: true));

var app = builder.Build();

// Swagger altid slået til (også når det ligger i skyen)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(AllowAllPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
