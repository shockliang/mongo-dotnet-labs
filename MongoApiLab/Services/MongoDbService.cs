using Microsoft.Extensions.Options;
using MongoApiLab.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoApiLab.Services;

public class MongoDbService
{
    private readonly IMongoCollection<Playlist> _playlistCollection;

    public MongoDbService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _playlistCollection = database.GetCollection<Playlist>(mongoDbSettings.Value.CollectionName);
    }

    public async Task CreateAsync(Playlist playlist)
    {
        await _playlistCollection.InsertOneAsync(playlist);
    }

    public async Task<List<Playlist>> GetAsync()
    {
        return await _playlistCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task AddToPlaylistAsync(string id, string movieId)
    {
        var filter = Builders<Playlist>.Filter.Eq("Id", id);
        var update = Builders<Playlist>.Update.AddToSet<string>("items", movieId);
        await _playlistCollection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<Playlist>.Filter.Eq("Id", id);
        await _playlistCollection.DeleteOneAsync(filter);
    }

}