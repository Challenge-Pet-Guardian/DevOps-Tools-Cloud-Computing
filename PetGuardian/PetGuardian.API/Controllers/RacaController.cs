using Microsoft.AspNetCore.Mvc;
using PetGuardian.Application.DTOs;
using PetGuardian.Application.Services.Interfaces;

namespace PetGuardian.API.Controllers;

/// <summary>Raças de pets.</summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class RacaController(IRacaService racaService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<RacaResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(racaService.GetAll());

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RacaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var raca = racaService.GetById(id);
        return raca is null ? NotFound() : Ok(raca);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RacaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] RacaRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = racaService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id) => racaService.Delete(id) ? NoContent() : NotFound();
}