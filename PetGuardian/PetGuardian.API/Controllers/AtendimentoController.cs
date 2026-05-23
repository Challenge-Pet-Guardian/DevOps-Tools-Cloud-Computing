using Microsoft.AspNetCore.Mvc;
using PetGuardian.Application.DTOs;
using PetGuardian.Application.Services.Interfaces;

namespace PetGuardian.API.Controllers;

/// <summary>Atendimentos veterinários. Liga Pet + Veterinario + Tipo + Status.</summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class AtendimentoController(IAtendimentoService atendimentoService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AtendimentoResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(atendimentoService.GetAll());

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AtendimentoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var a = atendimentoService.GetById(id);
        return a is null ? NotFound() : Ok(a);
    }

    [HttpGet("by-pet/{petId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<AtendimentoResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByPet(Guid petId) => Ok(atendimentoService.GetByPetId(petId));

    [HttpGet("by-veterinario/{veterinarioId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<AtendimentoResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByVeterinario(Guid veterinarioId) =>
        Ok(atendimentoService.GetByVeterinarioId(veterinarioId));

    [HttpPost]
    [ProducesResponseType(typeof(AtendimentoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] AtendimentoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = atendimentoService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id) => atendimentoService.Delete(id) ? NoContent() : NotFound();
}