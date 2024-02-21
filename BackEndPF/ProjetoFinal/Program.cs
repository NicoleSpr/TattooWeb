using ProjetoFinal.Data;
using Microsoft.EntityFrameworkCore;
using System;
static void ConfigureServices(IServiceCollection services)
{
   /* services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
            builder => builder.WithOrigins("http://127.0.0.1:5500")
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials());
    });*/

    // Outros serviços
}
/*
static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseCors("CorsPolicy");

    // Outras configurações de middleware
}*/
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connectionString = "Host=roundhouse.proxy.rlwy.net;Port=50784;Username=postgres;Password=cadG*D4BGcd-ABBcEC-CfBg3DG6baC1b;Database=railway";
builder.Services.AddDbContext<ApiContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt =>  opt.AllowAnyHeader().AllowAnyOrigin() );
app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
