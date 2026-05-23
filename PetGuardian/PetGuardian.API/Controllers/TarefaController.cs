using Microsoft.AspNetCore.Mvc;
using PetGuardian.Application.DTOs;
using PetGuardian.Application.Services.Interfaces;

namespace PetGuardian.API.Controllers;

/// <summary>Tarefas de cuidado. Usuario é opcional; Veterinario é obrigatório.</summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class TarefaController(ITarefaService tarefaService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TarefaResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(tarefaService.GetAll());

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TarefaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var t = tarefaService.GetById(id);
        return t is null ? NotFound() : Ok(t);
    }

    [HttpGet("by-pet/{petId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<TarefaResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByPet(Guid petId) => Ok(tarefaService.GetByPetId(petId));

    [HttpGet("by-usuario/{usuarioId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<TarefaResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByUsuario(Guid usuarioId) =>
        Ok(tarefaService.GetByUsuarioId(usuarioId));

    [HttpGet("by-veterinario/{veterinarioId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<TarefaResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByVeterinario(Guid veterinarioId) =>
        Ok(tarefaService.GetByVeterinarioId(veterinarioId));

    [HttpGet("by-status/{statusId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<TarefaResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByStatus(Guid statusId) =>
        Ok(tarefaService.GetByStatusId(statusId));

    [HttpPost]
    [ProducesResponseType(typeof(TarefaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] TarefaRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = tarefaService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPost("{id:guid}/concluir")]
    [ProducesResponseType(typeof(TarefaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Concluir(Guid id, [FromBody] TarefaConcluirRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        return Ok(tarefaService.Concluir(id, request.UsuarioId));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id) => tarefaService.Delete(id) ? NoContent() : NotFound();
}
