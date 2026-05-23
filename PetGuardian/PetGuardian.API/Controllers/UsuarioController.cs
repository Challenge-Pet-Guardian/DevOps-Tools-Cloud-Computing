using Microsoft.AspNetCore.Mvc;
using PetGuardian.Application.DTOs;
using PetGuardian.Application.Services.Interfaces;

namespace PetGuardian.API.Controllers;

/// <summary>Usuários. Senha nunca é exposta nas respostas.</summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class UsuarioController(IUsuarioService usuarioService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<UsuarioResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(usuarioService.GetAll());

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var u = usuarioService.GetById(id);
        return u is null ? NotFound() : Ok(u);
    }

    [HttpGet("by-email")]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetByEmail([FromQuery] string email)
    {
        var u = usuarioService.GetByEmail(email);
        return u is null ? NotFound() : Ok(u);
    }

    [HttpGet("{id:guid}/score")]
    [ProducesResponseType(typeof(UsuarioScoreResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetScore(Guid id) => Ok(usuarioService.GetScore(id));

    [HttpPost]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] UsuarioRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = usuarioService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id) => usuarioService.Delete(id) ? NoContent() : NotFound();
}
