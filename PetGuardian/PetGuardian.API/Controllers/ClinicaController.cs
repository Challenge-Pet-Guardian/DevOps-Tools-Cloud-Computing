using Microsoft.AspNetCore.Mvc;
using PetGuardian.Application.DTOs;
using PetGuardian.Application.Services.Interfaces;

namespace PetGuardian.API.Controllers;

/// <summary>Clínicas veterinárias. Possuem endereço e telefone exclusivos.</summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class ClinicaController(IClinicaService clinicaService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ClinicaResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(clinicaService.GetAll());

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ClinicaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var c = clinicaService.GetById(id);
        return c is null ? NotFound() : Ok(c);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ClinicaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] ClinicaRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = clinicaService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id) => clinicaService.Delete(id) ? NoContent() : NotFound();
}