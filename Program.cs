using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Registering the DbContext with SQLite
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite("Data Source=C:\\Users\\u499230\\Desktop\\Doctor_Module\\DoctorApp.db"));

// Adding controllers
builder.Services.AddControllers();

// Adding API explorer for endpoint documentation
builder.Services.AddEndpointsApiExplorer();

// Configuring Swagger for API documentation
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DoctorModuleAPI", Version = "v1" });
});

var app = builder.Build();

// Configuring middleware for development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enforcing HTTPS redirection
app.UseHttpsRedirection();

// Adding authorization middleware
app.UseAuthorization();

// Mapping controller routes
app.MapControllers();

// Running the application
app.Run();
