using Microsoft.EntityFrameworkCore;
using TaskApp.Data.Context;
using TaskApp.Services.Interfaces;
using TaskApp.Services.Services;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration["TaskApp:ConnectionString"];

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TaskAppDbContext>(options =>
    options.UseMySQL(connection)
);

builder.Services.AddScoped<ICurrentUserService ,CurrentUserService>();
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();

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
