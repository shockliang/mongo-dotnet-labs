using Microsoft.AspNetCore.Mvc;
using MongoApiLab.Models;
using MongoApiLab.Services;

namespace MongoApiLab.Controllers;

[Controller]
[Route("api/[controller]")]
public class PlaylistController : Controller
{
    private readonly MongoDbService _mongoDbService;

    public PlaylistController(MongoDbService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }

    [HttpGet]
    public async Task<List<Playlist>> Get()
    {
        return await _mongoDbService.GetAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Playlist playlist)
    {
        await _mongoDbService.CreateAsync(playlist);
        return CreatedAtAction(nameof(Get), new { id = playlist.Id }, playlist);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AddToPlaylist(string id, [FromBody] string movieId)
    {
        await _mongoDbService.AddToPlaylistAsync(id, movieId);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mongoDbService.DeleteAsync(id);
        return NoContent();
    }
}