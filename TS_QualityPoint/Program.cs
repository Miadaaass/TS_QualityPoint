using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using TS_QualityPoint.Options;
using TS_QualityPoint.Services;
using TS_QualityPoint.Middleware;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// ��������� �����������
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// �������� ������������
builder.Services.Configure<DaDataOptions>(builder.Configuration.GetSection("DaData"));

// ��������� HttpClient
builder.Services.AddHttpClient<IAddressService, AddressService>();

// ��������� CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// ��������� ������������
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ��������� middleware
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});

app.UseAuthorization();
app.MapControllers();
app.Run();