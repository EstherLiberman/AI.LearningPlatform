//using AI.LearningPlatform.BL.Configuration;
//using AI.LearningPlatform.BL.NewFolder;
//using AI.LearningPlatform.BL.Services;
//using AI.LearningPlatform.DAL.NewFolder;
//using AI.LearningPlatform.DAL.Repositories;
//using AI.LearningPlatform.Server.NewFolder;
//using Microsoft.Extensions.Options;
//using MongoDB.Driver;
//using OpenAI.Extensions;
//using OpenAI.Interfaces;
//using OpenAI.Managers;

//var builder = WebApplication.CreateBuilder(args);

//// CORS
//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
//    {
//        policy.WithOrigins("http://localhost:5180")
//              .AllowAnyHeader()
//              .AllowAnyMethod();
//    });
//});

//// קונפיגורציה
//builder.Configuration
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
//    .AddUserSecrets<Program>();



//// MongoDB
//var mongoSettingsSection = builder.Configuration.GetSection("MongoDbSettings");
//string connectionString = mongoSettingsSection.GetValue<string>("ConnectionString") ?? throw new Exception("Missing ConnectionString");
//string databaseName = mongoSettingsSection.GetValue<string>("DatabaseName") ?? throw new Exception("Missing DatabaseName");

//builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionString));
//builder.Services.AddSingleton<IMongoDatabase>(sp =>
//{
//    var client = sp.GetRequiredService<IMongoClient>();
//    return client.GetDatabase(databaseName);
//});

//// רישום תלויות לפי שכבות
//builder.Services.AddDALRepositories();   // DAL
//builder.Services.AddBLServices();       // BL

//// OpenAI
//builder.Services.Configure<OpenAISettings>(builder.Configuration.GetSection("OpenAISettings"));
//builder.Services.AddOpenAIService();
//builder.Services.AddScoped<IAiService, OpenAiService>();

//// ASP.NET
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// אתחול נתונים (Seeder)
//using (var scope = app.Services.CreateScope())
//{
//    var seeder = new DbSeeder(
//        scope.ServiceProvider.GetRequiredService<UserRepository>(),
//        scope.ServiceProvider.GetRequiredService<CategoryRepository>(),
//        scope.ServiceProvider.GetRequiredService<SubCategoryRepository>(),
//        scope.ServiceProvider.GetRequiredService<PromptRepository>());

//    await seeder.SeedAsync();




//}

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseCors(MyAllowSpecificOrigins);
//app.UseAuthorization();
//app.MapControllers();

//app.Run();




using AI.LearningPlatform.BL.Configuration;
using AI.LearningPlatform.BL.NewFolder;
using AI.LearningPlatform.BL.Services;
using AI.LearningPlatform.DAL.Models;
using AI.LearningPlatform.DAL.NewFolder;
using AI.LearningPlatform.DAL.Repositories;
using AI.LearningPlatform.Server.NewFolder;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using OpenAI.Extensions;
using OpenAI.Interfaces;
using OpenAI.Managers;

var builder = WebApplication.CreateBuilder(args);

BsonClassMap.RegisterClassMap<User>(cm =>
{
    cm.AutoMap();
    cm.SetIgnoreExtraElements(true); // 👈 חשוב - זה מה שמתעלם משדות לא קיימים
});

// CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:5180")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// קונפיגורציה
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>();

// MongoDB
var mongoSettingsSection = builder.Configuration.GetSection("MongoDbSettings");
string connectionString = mongoSettingsSection.GetValue<string>("ConnectionString") ?? throw new Exception("Missing ConnectionString");
string databaseName = mongoSettingsSection.GetValue<string>("DatabaseName") ?? throw new Exception("Missing DatabaseName");

builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionString));
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(databaseName);
});

// רישום תלויות לפי שכבות
builder.Services.AddDALRepositories();   // DAL
builder.Services.AddBLServices();       // BL

// OpenAI
builder.Services.Configure<OpenAISettings>(builder.Configuration.GetSection("OpenAISettings"));
builder.Services.AddOpenAIService();
builder.Services.AddScoped<IAiService, OpenAiService>();

// ASP.NET
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// אתחול נתונים - עובר לפונקציה חיצונית
await SeedDataAsync(app);

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();

// 📌 זה חייב להיות בסוף אחרת השרת ייסגר מיד
app.Run();


// פונקציית אתחול נתונים
async Task SeedDataAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var seeder = new DbSeeder(
        scope.ServiceProvider.GetRequiredService<UserRepository>(),
        scope.ServiceProvider.GetRequiredService<CategoryRepository>(),
        scope.ServiceProvider.GetRequiredService<SubCategoryRepository>(),
        scope.ServiceProvider.GetRequiredService<PromptRepository>());

    await seeder.SeedAsync();
}

