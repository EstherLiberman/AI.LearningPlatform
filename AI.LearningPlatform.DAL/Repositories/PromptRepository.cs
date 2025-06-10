
using AI.LearningPlatform.DAL.Models;
using MongoDB.Driver;

public class PromptRepository
{
    private readonly IMongoCollection<Prompt> _collection;

    public PromptRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Prompt>("prompts");
    }
    public async Task AddAsync(Prompt prompt)
    {
        await _collection.InsertOneAsync(prompt);
    }

    public async Task<List<Prompt>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
    public async Task AddPromptAsync(Prompt prompt) =>
        await _collection.InsertOneAsync(prompt);

    public async Task<List<Prompt>> GetPromptsByUserIdAsync(string userId) =>
        await _collection.Find(p => p.UserId == userId).ToListAsync();

    public async Task<Prompt?> GetPromptByIdAsync(string id) =>
        await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
}
