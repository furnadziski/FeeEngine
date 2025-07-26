using FeeEngine.Rules;
using FeeEngine.Services.Implementations;
using FeeEngine.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRule, RulePos>();
builder.Services.AddScoped<IRule, RuleEcommerce>();
builder.Services.AddScoped<IFeeCalculatorService, FeeCalculatorService>();
builder.Services.AddSingleton<ITransactionHistoryService, TransactionHistoryService>();
builder.Services.AddScoped<IBatchFeeCalculatorService, BatchFeeCalculatorService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

