using Microsoft.EntityFrameworkCore;
using TaskApp.Data.Context;

var builder = WebApplication.CreateBuilder(args);

var conStrBuilder = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder(
        builder.Configuration.GetConnectionString("TaskApp"));
conStrBuilder.Password = builder.Configuration["TaskAppDbPassword"];
var connection = conStrBuilder.ConnectionString;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TaskAppDbContext>(options =>
    options.UseMySQL(connection)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
