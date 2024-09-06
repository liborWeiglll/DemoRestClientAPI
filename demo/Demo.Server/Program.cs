using Demo.Server.Chaos;
using Demo.Server.Endpoints;
using Demo.Server.Products;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ChaosMiddleware>();
builder.Services.AddSingleton<ChaosContext>();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("Db")));
builder.Services.AddTransient<ProductService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(x=> x.DisplayOperationId());

app.UseHttpsRedirection();
app.UseMiddleware<ChaosMiddleware>();

var api = app.MapGroup("api");

api.MapDemoApi().WithOpenApi(x => { x.Tags.Add(new() {Name = "Failing API"}); return x; });

app.Services
    .CreateScope().ServiceProvider
    .GetRequiredService<AppDbContext>()
    .Database.EnsureCreated();

await app.RunAsync();