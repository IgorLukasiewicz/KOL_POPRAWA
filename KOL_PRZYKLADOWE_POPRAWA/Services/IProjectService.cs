using KOL_PRZYKLADOWE_POPRAWA.DTOs;

namespace KOL_PRZYKLADOWE_POPRAWA.Services;

public interface IProjectService
{
    public Task<ProjectInfoDTO> GetProjectInfo(int projectId);
}