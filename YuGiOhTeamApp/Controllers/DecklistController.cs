using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuGiOhTeamApp.Models;
using YuGiOhTeamApp.Services;

namespace YuGiOhTeamApp.Controllers
{
    [Route("api/decklist")]
    [ApiController]
    public class DecklistController : ControllerBase
    {
        private readonly IDecklistService _decklistService;

        public DecklistController(IDecklistService decklistService)
        {
            _decklistService = decklistService;
        }

        [HttpPost]
        [Route("create")]
        public ActionResult CreateDecklist([FromForm]IFormFile file)
        {
            _decklistService.CreateDecklist(file);
            return Ok();
        }
        [HttpPatch]
        [Route("{teamId}")]
        public ActionResult EditDecklist([FromRoute] int teamId,[FromBody]EditDecklistDto dto)
        {
            _decklistService.EditDecklist(teamId, dto);
            return Ok();
        }
    }
}
