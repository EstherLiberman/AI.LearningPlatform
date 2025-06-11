using AI.LearningPlatform.BL.Services;
using AI.LearningPlatform.DAL.Repositories;
using AI.LearningPlatform.Server.NewFolder;
using MongoDB.Driver;
using OpenAI.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

//קריאת הגדרות
var mongoSettingsSection = builder.Configuration.GetSection("MongoDbSettings");
string connectionString = mongoSettingsSection.GetValue<string>("ConnectionString") ?? throw new Exception("Missing ConnectionString");
string databaseName = mongoSettingsSection.GetValue<string>("DatabaseName") ?? throw new Exception("Missing DatabaseName");



//   רישום
builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionString));
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(databaseName);
});

//תלויות
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<SubCategoryRepository>();
builder.Services.AddScoped<PromptRepository>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<SubCategoryService>();
builder.Services.AddScoped<PromptService>();
builder.Services.AddScoped<LessonRepository>();
builder.Services.AddScoped<LessonService>();


// === שאר הדברים נשארים ===
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddOpenAIService(); // מתוך Betalgo.OpenAI


//builder.Configuration
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);


Console.WriteLine(">>> OpenAI API Key = " + builder.Configuration["OpenAISettings:ApiKey"]);

builder.Services.AddOpenAIService(settings =>
{
    settings.ApiKey = builder.Configuration["OpenAISettings:ApiKey"];
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = new DbSeeder(
        scope.ServiceProvider.GetRequiredService<UserRepository>(),
        scope.ServiceProvider.GetRequiredService<CategoryRepository>(),
        scope.ServiceProvider.GetRequiredService<SubCategoryRepository>(),
        scope.ServiceProvider.GetRequiredService<PromptRepository>());

    await seeder.SeedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();






