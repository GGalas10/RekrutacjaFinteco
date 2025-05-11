using Finteco.Application;
using Finteco.DataAccess;
using Finteco.DataAccess.Databases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseInMemoryDatabase("UserDatabase"));
builder.Services.AddDbContext<DataDbContext>(options =>
    options.UseInMemoryDatabase("DataDb"));

builder.Services.AddApplicationLayer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        var angular = builder.Configuration.GetValue<string>("Angular" ;
        policy.WithOrigins(angular == null ? "http://localhost:4200/" : angular)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowAngularApp");
app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    UserDbSeeder.SeedUsers(dbContext);
}
app.Run();
