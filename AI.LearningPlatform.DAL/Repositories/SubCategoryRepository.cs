using AI.LearningPlatform.DAL.Models;
using MongoDB.Driver;

public class SubCategoryRepository
{
    private readonly IMongoCollection<SubCategory> _collection;

    public SubCategoryRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<SubCategory>("sub_categories");
    }

    public async Task<List<SubCategory>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(SubCategory subCategory)
    {
        await _collection.InsertOneAsync(subCategory);
    }
    public async Task<List<SubCategory>> GetSubCategoriesAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<List<SubCategory>> GetByCategoryIdAsync(string categoryId) =>
        await _collection.Find(sc => sc.CategoryId == categoryId).ToListAsync();

    public async Task CreateSubCategoryAsync(SubCategory subCategory) =>
        await _collection.InsertOneAsync(subCategory);
}
