using distributed_memory_cache;
using in_memory_cache;
using memory_cache.bal;
using memory_cache.bal.entries;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddScoped<ITestData, TestData>();
builder.Services.AddScoped<IMemoryCacheService, MemoryCacheService>();
builder.Services.AddControllers();
builder.Services.AddSingleton<IDistributedRedisCache, DistributedRedisCache>();
builder.Services.AddStackExchangeRedisCache(config =>
{
    config.Configuration = "127.0.0.1:6379,abortConnect=false,connectTimeout=30000,responseTimeout=30000";
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
