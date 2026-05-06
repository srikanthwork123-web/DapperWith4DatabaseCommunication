using DapperWith4DatabaseCommunication.Data;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Middlewares;
using DapperWith4DatabaseCommunication.Repositories;
using DapperWith4DatabaseCommunication.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//========================================================================
//We need to register Serilog to our dependency Injection Conatiner. The UseSerilog method is used to configure Serilog as the logging provider for the application. The configuration.ReadFrom.Configuration(context.Configuration) part tells Serilog to read its configuration settings from the application's configuration, which can be defined in appsettings.json or other configuration sources.
builder.Host.UseSerilog((context, configuration) =>
configuration.ReadFrom.Configuration(context.Configuration));
//==========================================================================
builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();//register the connection factory interface and its implementation in the dependency injection container of the application using the AddSingleton method   builder object. The AddSingleton method is used to register a service with a singleton lifetime, which means that a single instance of the service will be created and shared throughout the application's lifetime.
builder.Services.AddSingleton<ILoggingFactory, LoggingFactory>();//register the logging factory interface and its implementation in the dependency injection container of the application using the AddSingleton method   builder object. The AddSingleton method is used to register a service with a singleton lifetime, which means that a single instance of the service will be created and shared throughout the application's lifetime.

//=================================================================================
//register the dependency injection for the repository and service layers of the application in the program.cs file of the web api project using the AddScoped method   builder object. The AddScoped method is used to register a service with a scoped lifetime, which means that a new instance of the service will be created for each HTTP request and shared within that request.
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();//register the repository interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.
builder.Services.AddScoped<IEmployeeService, EmployeeServices>();//register the service interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.
//=====================================================================================
//register the dependency injection for the repository and service layers of the application in the program.cs file of the web api project using the AddScoped method   builder object. The AddScoped method is used to register a service with a scoped lifetime, which means that a new instance of the service will be created for each HTTP request and shared within that request.
builder.Services.AddScoped<IDepartmentRepository, DepartementRepository>();//register the repository interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.
builder.Services.AddScoped<IDepartementService, DepartementService>();//register the service interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.


//=========================================================================================
//register the dependency injection for the repository and service layers of the application in the program.cs file of the web api project using the AddScoped method   builder object. The AddScoped method is used to register a service with a scoped lifetime, which means that a new instance of the service will be created for each HTTP request and shared within that request.
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();//register the repository interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.
builder.Services.AddScoped<IOrdersService, OrdersService>();//register the service interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.

//========================================================================================================

var app = builder.Build();
//custom middlewares we need to register in the program.cs file of the web api project using the UseMiddleware method   app object. The UseMiddleware method is used to add custom middleware components to the application's request processing pipeline. By adding the GlobalErrorHandlerMiddleware, you ensure that any unhandled exceptions that occur during the processing of HTTP requests will be caught and handled by this middleware, allowing you to return a standardized error response to the client and log the error details as needed.
app.UseMiddleware<GlobalErrorHandlerMiddleware>();//This line of code is used to add the GlobalErrorHandlerMiddleware to the application's request processing pipeline. The UseMiddleware method is an extension method that allows you to add custom middleware components to the pipeline. By adding the GlobalErrorHandlerMiddleware, you ensure that any unhandled exceptions that occur during the processing of HTTP requests will be caught and handled by this middleware, allowing you to return a standardized error response to the client and log the error details as needed.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
