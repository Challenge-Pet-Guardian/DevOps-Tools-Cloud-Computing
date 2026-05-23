using Microsoft.AspNetCore.Mvc;
using PetGuardian.Application.DTOs;
using PetGuardian.Application.Services.Interfaces;

namespace PetGuardian.API.Controllers;

/// <summary>Veterinários. Podem ou não estar vinculados a uma clínica.</summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class VeterinarioController(IVeterinarioService veterinarioService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarioResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(veterinarioService.GetAll());

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(VeterinarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var v = veterinarioService.GetById(id);
        return v is null ? NotFound() : Ok(v);
    }

    /// <summary>Obtém veterinário pelo e-mail.</summary>
    [HttpGet("by-email")]
    [ProducesResponseType(typeof(VeterinarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetByEmail([FromQuery] string email)
    {
        var v = veterinarioService.GetByEmail(email);
        return v is null ? NotFound() : Ok(v);
    }

    /// <summary>Lista veterinários de uma clínica.</summary>
    [HttpGet("by-clinica/{clinicaId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarioResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByClinica(Guid clinicaId) =>
        Ok(veterinarioService.GetByClinicaId(clinicaId));

    [HttpPost]
    [ProducesResponseType(typeof(VeterinarioResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] VeterinarioRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = veterinarioService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id) => veterinarioService.Delete(id) ? NoContent() : NotFound();
}