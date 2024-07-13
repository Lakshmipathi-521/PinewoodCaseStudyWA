using System.Text;
using Business.CRMSystem;
using DataAccess.CRMSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Models.Customers;
using ORM.CRMSystem;

var builder = WebApplication.CreateBuilder(args);

var CUSTOMERS_DB_CONNECTION_STRING_KEY = "CUSTOMERS_DB_CONNECTION_STRING";

builder.Services.AddDbContext<CustomersContext>(dbContextOptionsBuilder =>
{
    var encodedConnectionString = Environment.GetEnvironmentVariable(
        CUSTOMERS_DB_CONNECTION_STRING_KEY
    );
#pragma warning disable CS8604 // Possible null reference argument.
    var decodedConnectionString = Encoding.ASCII.GetString(
        Convert.FromBase64String(encodedConnectionString)
    );
#pragma warning restore CS8604 // Possible null reference argument.

    dbContextOptionsBuilder.UseSqlServer(decodedConnectionString);
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<ICustomersContext, CustomersContext>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<IBusinessValidator<string>, SearchStringValidator>();
builder.Services.AddScoped<IBusinessValidator<Customer>, CustomerEntityValidator>();
builder.Services.AddScoped<ICustomersBusinessComponent, CustomersBusinessComponent>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
