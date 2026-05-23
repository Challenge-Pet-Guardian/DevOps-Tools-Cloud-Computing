using Microsoft.AspNetCore.Mvc;
using PetGuardian.Application.DTOs;
using PetGuardian.Application.Services.Interfaces;

namespace PetGuardian.API.Controllers;

/// <summary>Vínculo N:N entre usuário e pet, com flag de responsável principal.</summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class UsuarioPetController(IUsuarioPetService usuarioPetService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<UsuarioPetResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(usuarioPetService.GetAll());

    [HttpGet("by-usuario/{usuarioId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<UsuarioPetResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByUsuario(Guid usuarioId) => Ok(usuarioPetService.GetByUsuarioId(usuarioId));

    [HttpGet("by-pet/{petId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<UsuarioPetResponse>), StatusCodes.Status200OK)]
    public IActionResult GetByPet(Guid petId) => Ok(usuarioPetService.GetByPetId(petId));

    [HttpGet("rede-cuidado/{usuarioId:guid}")]
    [ProducesResponseType(typeof(RedeCuidadoResponse), StatusCodes.Status200OK)]
    public IActionResult GetRedeCuidado(Guid usuarioId) => Ok(usuarioPetService.GetRedeCuidadoByUsuarioId(usuarioId));

    [HttpPost]
    [ProducesResponseType(typeof(UsuarioPetResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] UsuarioPetRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = usuarioPetService.Create(request);
        return CreatedAtAction(nameof(GetByUsuario), new { usuarioId = created.UsuarioId }, created);
    }

    [HttpPost("invite/by-usuario")]
    [ProducesResponseType(typeof(UsuarioPetResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult InviteByUsuario([FromBody] UsuarioPetInviteByUsuarioRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = usuarioPetService.InviteByUsuario(request);
        return CreatedAtAction(nameof(GetByPet), new { petId = created.PetId }, created);
    }

    [HttpPost("invite/by-email")]
    [ProducesResponseType(typeof(UsuarioPetResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult InviteByEmail([FromBody] UsuarioPetInviteByEmailRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = usuarioPetService.InviteByEmail(request);
        return CreatedAtAction(nameof(GetByPet), new { petId = created.PetId }, created);
    }

    /// <summary>Remove um vínculo pela chave composta (usuarioId + petId).</summary>
    [HttpDelete("{usuarioId:guid}/{petId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid usuarioId, Guid petId) =>
        usuarioPetService.Delete(usuarioId, petId) ? NoContent() : NotFound();
}
