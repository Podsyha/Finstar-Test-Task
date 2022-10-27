using System.Reflection;
using FINSTAR_Test_Task.Common.Assert;
using FINSTAR_Test_Task.Common.Middlewares;
using FINSTAR_Test_Task.Infrastructure.Context;
using FINSTAR_Test_Task.Infrastructure.Repository.CodeValueRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Serilog
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
{
    { "message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
    { "message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
    { "level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
    { "raise_date", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
    { "exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
    { "properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
    { "props_test", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
    {
        "machine_name",
        new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l")
    }
};
var logger = new LoggerConfiguration()
    .WriteTo.PostgreSQL(connectionString, "logs", columnWriters, needAutoCreateTable: true)
    .CreateLogger();
builder.Host.UseSerilog(logger);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Example API", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString), ServiceLifetime.Transient);

builder.Services.AddTransient<ICodeValueRepository, CodeValueRepository>();
builder.Services.AddSingleton<IAssert, Assert>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.Run();