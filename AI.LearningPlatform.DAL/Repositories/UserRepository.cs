
using AI.LearningPlatform.DAL.Models;
using MongoDB.Driver;

public class UserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<User>("users");
    }
    public async Task AddAsync(User user)
    {
        await _collection.InsertOneAsync(user);
    }

    public async Task<List<User>> GetAllUsersAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<User?> GetUserByIdAsync(string id) =>
        await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();

    public async Task AddUserAsync(User user) =>
        await _collection.InsertOneAsync(user);

    public async Task UpdateUserAsync(User user) =>
        await _collection.ReplaceOneAsync(u => u.Id == user.Id, user);
}
