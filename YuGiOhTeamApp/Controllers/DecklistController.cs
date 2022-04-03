using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
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
        [Route("{decklistId}")]
        public ActionResult EditDecklist([FromRoute] int decklistId,[FromBody]EditDecklistDto dto)
        {
            _decklistService.EditDecklist(decklistId, dto);
            return Ok();
        }
        [HttpGet]
        [Route("download/{decklistId}")]
        public ActionResult Download([FromRoute]int decklistId)
        {
            var contentDisposition = new ContentDispositionHeaderValue("attachment");
            contentDisposition.SetHttpFileName(_decklistService.DownloadDecklist(decklistId).Item2);
            Response.Headers[HeaderNames.ContentDisposition] = contentDisposition.ToString();
            return File(_decklistService.DownloadDecklist(decklistId).Item1, "text/plain");
        }
        [HttpGet]
        [Route("{visibility}")]
        public ActionResult<PagedResult<DecklistDto>> GetAll([FromQuery]PageQuery query, [FromRoute]Visibility visibility)
        {
            return _decklistService.getAll(query, visibility);
        }
        [HttpGet]
        [Route("details/{decklistId}")]
        public ActionResult getDetails([FromRoute]int decklistId)
        {
            return Ok(_decklistService.GetDecklistDetails(decklistId));
        }
    }
}
