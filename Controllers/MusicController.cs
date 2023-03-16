﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MusicLibraryWebAPI.Data;
using MusicLibraryWebAPI.Models;

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


    }
}
