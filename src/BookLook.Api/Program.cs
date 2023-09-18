using BookLook.Application;
using BookLook.Application.Services;
using BookLook.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add layers
builder.Services
    .AddApplication()
    .AddInfrastructure();

// Add mappings
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add embedded resource books to in memory book repository
var bookParserService = app
    .Services
    .GetRequiredService<BookParserService>();

bookParserService
    .ParseEmbeddedBooksAsync()
    .Wait();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
