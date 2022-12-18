using DB.Repositories;
using gRPCService.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MSSQLConnection");
builder.Services.AddSingleton<IDbRepository>(new EfSqlRepository(connectionString));
builder.Services.AddSingleton<DbService>();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<DataSender>();

app.Run();
