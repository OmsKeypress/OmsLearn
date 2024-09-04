using EmployeeDirectory.BLL;
using EmployeeDirectory.BLL.Interface;
using EmployeeDirectory.DAL;
using EmployeeDirectory.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OmsLearn.BLL;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Configuration
ConfigurationManager configuration = builder.Configuration;

// Servicesc
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ILearn, LearnService>();
builder.Services.AddScoped<IDropDown, DropdownListService>();
builder.Services.AddScoped<ApiService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});

var customConfiguration = new ConfigurationBuilder()
                        .SetBasePath(builder.Environment.ContentRootPath)
                        .AddJsonFile("CacheKey.json")
                        .Build();

GetFavoriteKey.Datakey = customConfiguration.GetSection("GetFavorite:Datakey").Value;
GetFavoriteKey.ExpiryTime = Convert.ToInt32(customConfiguration?.GetSection("GetFavorite:ExpiryTime").Value);

GetBookKey.Datakey = customConfiguration.GetSection("BookList:Datakey").Value;
GetBookKey.ExpiryTime = Convert.ToInt32(customConfiguration.GetSection("BookList:ExpiryTime").Value);


// Authenticationa
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(Options =>
    {
        Options.RequireHttpsMetadata = false;
        Options.SaveToken = true;
        Options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = configuration["Jwt:Audience"],
            ValidIssuer = configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });

// Connection settings
Connection.ConnectionString = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
Connection.ConnectionString1 = configuration.GetSection("ConnectionStrings:DefaultConnection1").Value;
Connection.LoggerTimeSpan = Convert.ToInt32(configuration.GetSection("Logger:TimeSpan").Value);
Connection.MongoConnectionString = configuration.GetSection("ConnectionStrings:MongoDbConnection").Value;

// File Logger Task
Task fileLoggerTask = new Task(() =>
{
    while (true)
    {
        if (!CommomHelper.IsSaveInProgress)
        {
            CommomHelper.SaveQueuedDataIntoFile();
        }
        Thread.Sleep(Connection.LoggerTimeSpan);
    }
});     

fileLoggerTask.Start();

// Logging settings
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
//Add Swagger
 
// Application setup
var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}
 
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Welcome, You are visiting API content. Please contact respective team for more information.");
});

app.Run();