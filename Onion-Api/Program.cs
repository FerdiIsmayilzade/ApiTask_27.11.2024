using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Data;
using Service;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                 x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
                 .AddFluentValidation(v =>
                 {

                     v.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                 });


//builder.Services.AddControllers().AddFluentValidation(v => { v.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));

});


builder.Services.AddRepositorylayer();
builder.Services.AddServicelayer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => Results.Redirect("/swagger"))
   .ExcludeFromDescription();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
