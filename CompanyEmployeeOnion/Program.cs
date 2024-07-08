using CompanyEmployeeOnion.Extensions;
using CompanyEmployeeOnion.Middlewares;
using Database;
using Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddTransient<ExceptionHandlingMiddleware>();
services.AddControllersConfiguration();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddServices();
services.AddRepositories();

services.AddDatabase(configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();