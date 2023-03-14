using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MusicLibraryWebAPI.Data;

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
            var music = _context; //.ModelName
            return Ok(music);
        }

        [HttpGet("{id}")]
        public IActionResult GetMusicById(int id) 
        {
            var musicById = _context; //.ModelNameWhere(x => x.Id == Id).SingleOrDefault();
            return Ok(musicById);
        }





    }
}
