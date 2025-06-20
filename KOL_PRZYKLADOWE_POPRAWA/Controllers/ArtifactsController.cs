using KOL_PRZYKLADOWE_POPRAWA.DTOs;
using KOL_PRZYKLADOWE_POPRAWA.Services;
using Microsoft.AspNetCore.Mvc;

namespace KOL_PRZYKLADOWE_POPRAWA.Controllers;

[ApiController]
[Route("api/artifacts")]
public class ArtifactsController : ControllerBase
{
    private readonly IArtifactService _projectService;

    public ArtifactsController(IArtifactService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost]
    public async Task<IActionResult> AddArtifactAndProject([FromBody] AddArtifactAndProjectDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var newId = await _projectService.AddArtifactAndProject(dto);
            return CreatedAtAction(nameof(AddArtifactAndProject), new { id = newId }, new { id = newId });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}
