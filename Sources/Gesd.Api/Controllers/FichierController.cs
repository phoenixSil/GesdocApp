using Gesd.Features.Contrats.Services.Fichiers;
using Gesd.Features.Dtos.Fichiers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gesd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichierController : ControllerBase
    {
        private readonly IFichierService _fichierService;

        public FichierController(IFichierService fichierService)
        {
            _fichierService = fichierService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FileAddedDto>> Get(IFormFile file)
        {
            var response = await _fichierService.Add(file).ConfigureAwait(false);
            var fichierAjoute = response.Data;
            return StatusCode(response.StatusCode, fichierAjoute);
        }
    }
}