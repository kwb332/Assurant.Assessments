
using Assurant.Pricing.Domain;
using Assurant.Pricing.Domain.Contract.Interface;
using Assurant.Pricing.Domain.Service;
using Assurant.Pricing.Infrastructure.Contract.Interface;
using Assurant.Pricing.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);
var configBuilder = new ConfigurationBuilder();
configBuilder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
// Add services to the container.
IConfiguration config = configBuilder.Build();


builder.Services.AddDbContext<PricingContext>(options => options.UseSqlServer(config["ConnectionStrings:PricingDB"]));
builder.Services.AddControllers();
builder.Services.Configure<ApplicationOptions>(options => config.GetSection("ApplicationOptions").Bind(options));
builder.Services.AddOptions<ApplicationOptions>();
builder.Services.AddTransient<IRuleService, RuleService>();
builder.Services.AddTransient<IPriceRepository, PriceRepository>();
builder.Services.AddTransient<IHolidayRepository, HolidayRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
