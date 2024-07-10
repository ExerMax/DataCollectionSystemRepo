using Microsoft.EntityFrameworkCore;
using DbAccess;
using DataCollectionSystem.Services;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddTransient<IVehicleSearcher, VehicleSearcher>();
builder.Services.AddTransient<IFixationHandler, FixationHandler>();
builder.Services.AddTransient<IMessageSender, MessageSender>();
builder.Services.AddTransient<ITaxComputer, TaxComputer>();
builder.Services.AddTransient<IDbWriter, DbWriter>();
builder.Services.AddTransient<IExternalSearcher, ExternalSearcher>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
