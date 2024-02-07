using AppCheckBackend.Context;
using AppCheckBackend.Middleware;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS
builder.Services.AddCors(c =>
{
    c.AddPolicy(name: MyAllowSpecificOrigins,
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var configuration = builder.Configuration;
builder.Services.AddDbContext<LocalContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Credentials.json");

FirebaseApp firebaseApp = FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(fullPath),
    ProjectId = configuration.GetValue<string>("FirebaseProjectId"),
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.UseMiddleware<AppcheckMiddleware>(firebaseApp);

app.MapControllers();

app.Run();