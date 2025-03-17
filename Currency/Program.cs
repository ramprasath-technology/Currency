using Asp.Versioning;
using CurrencyConverter.Service;
using NetworkCalls.HTTP;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
});

builder.Services.AddHttpClient();
builder.Services.AddLogging();
builder.Services.AddScoped<ICurrencyConverterFactory, CurrencyConverterFactory>();
builder.Services.AddScoped<IGetCalls, GetCalls>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.UsePathBase(new PathString("/currencyapi"));

app.MapControllers();

app.Run();
