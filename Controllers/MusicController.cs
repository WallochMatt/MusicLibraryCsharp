using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MusicLibraryWebAPI.Data;
using MusicLibraryWebAPI.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicLibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MusicController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllMusic() 
        {
            var music = _context.Songs;
            return Ok(music);
        }

        [HttpGet("{id}")]
        public IActionResult GetMusicById(int id) 
        {
            var musicById = _context.Songs.Where(x => x.Id == id).SingleOrDefault();
            return Ok(musicById);
        }

        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(PostSong), new { id = song.Id, title = song.Title, artist = song.Artist, album = song.Album,
                releaseDate = song.ReleaseDate, genre = song.Genre} , song);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Song>> PutSong(long id, Song song)
        {
            if (id != song.Id)
            {
                return BadRequest();
            }

            _context.Entry(song).State = EntityState.Modified; ;
            
            try
            {
                await _context.SaveChangesAsync();  
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id) 
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);   
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool SongExists(long id)
        {
            return _context.Songs.Any(x => x.Id == id);
        }
    }
}
