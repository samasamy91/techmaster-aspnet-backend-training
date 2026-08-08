using Microsoft.EntityFrameworkCore;
using ProfessionalArchitectureTrainingCenter;
using ProfessionalArchitectureTrainingCenter.Extensions;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.Middlewares;
using TrainingCenterAuthorization.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<AppDbContext>(options => options
.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Services Extension
builder.Services.AddApplicationServices();
//Auth Extension
builder.Services.AddJwtAuthentication(builder.Configuration);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DatabaseSeeder.SeedAdmin(context);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddlewares>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
