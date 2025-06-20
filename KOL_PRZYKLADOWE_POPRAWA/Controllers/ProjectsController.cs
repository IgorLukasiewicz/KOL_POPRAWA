using KOL_PRZYKLADOWE_POPRAWA.Exceptions;
using KOL_PRZYKLADOWE_POPRAWA.Services;
using Microsoft.AspNetCore.Mvc;

namespace KOL_PRZYKLADOWE_POPRAWA.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProjectsController:ControllerBase
{
    private readonly IProjectService _dbService;

    public ProjectsController(IProjectService dbService)
    {
        _dbService = dbService;
    }
    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetProjectInfo(int projectId){
        try
        {
            
            var visit = await _dbService.GetProjectInfo(projectId);
            return Ok(visit);

        }
        catch (NotFoundException e)
        {
            return NotFound();
        }
    }
}