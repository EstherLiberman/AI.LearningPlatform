using AI.LearningPlatform.DAL.Models;
using MongoDB.Driver;

public class PromptRepository
{
    //private readonly IMongoCollection<Prompt> _collection;

    //public PromptRepository(IMongoDatabase database)
    //{
    //    _collection = database.GetCollection<Prompt>("prompts");
    //}

    //public async Task AddAsync(Prompt prompt)
    //{
    //    await _collection.InsertOneAsync(prompt);
    //}

    //public async Task<List<Prompt>> GetAllAsync()
    //{
    //    return await _collection.Find(_ => true).ToListAsync();
    //}

    //public async Task AddPromptAsync(Prompt prompt) =>
    //    await _collection.InsertOneAsync(prompt);

    //public async Task<List<Prompt>> GetPromptsByUserIdAsync(string userId) =>
    //    await _collection.Find(p => p.UserId == userId).ToListAsync();

    ////// 🆕 הוספת המתודה לפי הדרישה
    ////public async Task<Prompt?> GetByIdAsync(string id)
    ////{
    ////    return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
    ////}

    //// אפשר להוסיף גם מתודה לעדכון במידת הצורך:
    //public async Task UpdateAsync(string id, Prompt updatedPrompt)
    //{
    //    await _collection.ReplaceOneAsync(p => p.Id == id, updatedPrompt);
    //}
    //public async Task UpdateAsync(Prompt prompt)
    //{
    //    var filter = Builders<Prompt>.Filter.Eq(p => p.Id, prompt.Id);
    //    await _collection.ReplaceOneAsync(filter, prompt);
    //}


    private readonly IMongoCollection<Prompt> _collection;

    public PromptRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Prompt>("prompts");
    }

    public async Task<List<Prompt>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Prompt?> GetByIdAsync(string id)
    {
        return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Prompt prompt)
    {
        await _collection.InsertOneAsync(prompt);
    }

    public async Task UpdateAsync(Prompt prompt)
    {
        await _collection.ReplaceOneAsync(p => p.Id == prompt.Id, prompt);
    }

    public async Task<List<Prompt>> GetPromptsByUserIdAsync(string userId)
    {
        return await _collection.Find(p => p.UserId == userId).ToListAsync();
    }



}
