using EtiqaFreelancerApi.DataContext;
using EtiqaFreelancerApi.Interfaces;
using EtiqaFreelancerApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configure database context
builder.Services.AddDbContext<FreelancerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(5),
         errorNumbersToAdd: null);
    }));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader();
        });
});

builder.Services.AddScoped<IUser,UserRepository>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddResponseCaching();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAllOrigins");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region StartHtmlFile
app.UseHttpsRedirection();
var options = new DefaultFilesOptions();
options.DefaultFileNames.Clear();
options.DefaultFileNames.Add("mydefault.html");
app.UseDefaultFiles(options);
app.UseStaticFiles();
app.UseRouting();
#endregion

app.UseAuthorization();
app.MapControllers();
app.Run();
