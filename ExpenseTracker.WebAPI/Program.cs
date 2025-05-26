using ExpenseTracker.Core.Domain.Repository_Interfaces;
using ExpenseTracker.Core.Service_Classes;
using ExpenseTracker.Core.Service_Interfaces;
using ExpenseTracker.Infrastructure;
using ExpenseTracker.Infrastructure.Repository_Classes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (!builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
}

builder.Services.AddScoped<IUserRepository, UsersRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpensesRepository>();
builder.Services.AddScoped<ISharedExpenseRepository, SharedExpensesRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<ISharedExpenseService, SharedExpenseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHsts();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
