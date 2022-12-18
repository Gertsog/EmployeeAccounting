using DB.Repositories;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var connectionString = builder.Configuration.GetConnectionString("MSSQLConnection");
builder.Services.AddSingleton<IDbRepository>(new EfSqlRepository(connectionString));

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options => options
        .SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options => options
        .SerializerSettings.ContractResolver = new DefaultContractResolver());

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
