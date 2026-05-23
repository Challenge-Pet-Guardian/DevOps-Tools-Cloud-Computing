using Microsoft.AspNetCore.Mvc;
using PetGuardian.Application.DTOs;
using PetGuardian.Application.Services.Interfaces;

namespace PetGuardian.API.Controllers;

/// <summary>Telefones de contato. Devem ser criados antes de Usuario e Veterinaria.</summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class TelefoneController(ITelefoneService telefoneService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TelefoneResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(telefoneService.GetAll());

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TelefoneResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var telefone = telefoneService.GetById(id);
        return telefone is null ? NotFound() : Ok(telefone);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TelefoneResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] TelefoneRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = telefoneService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id) => telefoneService.Delete(id) ? NoContent() : NotFound();
}