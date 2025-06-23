using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Registering the DbContext with SQLite
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite("Data Source=DoctorApp.db"));
builder.Services.AddRazorPages();
// Adding controllers
builder.Services.AddControllers();
builder.Services.AddHttpClient(); 
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
app.UseStaticFiles();
// Adding authorization middleware
app.UseAuthorization();
app.UseRouting();
// Mapping controller routes
app.MapControllers();
app.MapRazorPages();
// Running the application

app.MapGet("/", context =>
{
    context.Response.Redirect("/Login");
    return Task.CompletedTask;
});

app.Run();
