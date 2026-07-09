using CyberSportsPortal.Core;
using CyberSportsPortal.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbSettings = builder.Configuration.GetRequiredSection(nameof(DbSettings)).Get<DbSettings>();
builder.Services.AddDataAccess(dbSettings);
builder.Services.AddBusinessLogic();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
                      policy =>
                      {
                          policy
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    Thread.Sleep(10000);
    var context = scope.ServiceProvider.GetService<CyberSportsContext>();
    context?.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("MyAllowSpecificOrigins");
app.UseAuthorization();
app.MapControllers();
app.Run();
