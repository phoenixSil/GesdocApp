﻿using System.Net;

using Gesd.Features.Contrats.Services.Fichiers;
using Gesd.Features.Dtos.Fichiers;

using Microsoft.AspNetCore.Mvc;

namespace Gesd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichierController : ControllerBase
    {
        private readonly IFichierService _fichierService;
        private readonly ILogger<FichierController> _logger;

        public FichierController(IFichierService fichierService, ILogger<FichierController> logger)
        {
            _fichierService = fichierService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FileAddedDto>> Get(IFormFile file)
        {
            try
            {
                _logger.LogInformation($"Controller:: {nameof(FichierController)}");
                var response = await _fichierService.Add(file).ConfigureAwait(false);
                var fichierAjoute = response.Data;
                return StatusCode(response.StatusCode, fichierAjoute);
            }
            catch (Exception ex)
            {
                _logger.LogError($"une erreur est survenue dans lors de l'execution du programme {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<FileAddedDto>>> Get()
        {
            try
            {
                _logger.LogInformation($"Controller:: {nameof(FichierController)}");
                var response = await _fichierService.Get().ConfigureAwait(false);
                var listFichiers = response.Data;
                return StatusCode(response.StatusCode, listFichiers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"une erreur est survenue dans lors de le la lecture de fichier {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            try
            {
                _logger.LogInformation($"Controller:: {nameof(FichierController)}");
                var response = await _fichierService.Delete(id).ConfigureAwait(false);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"une erreur est survenue dans lors de le la suppresion du fichier {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}