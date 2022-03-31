﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ActionResult UploadDecklist([FromForm]IFormFile file)
        {
            return Ok(_decklistService.UploadDecklist(file));
        }
    }
}
