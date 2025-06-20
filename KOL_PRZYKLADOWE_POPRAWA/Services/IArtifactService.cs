using KOL_PRZYKLADOWE_POPRAWA.DTOs;

namespace KOL_PRZYKLADOWE_POPRAWA.Services;

public interface IArtifactService
{
    public Task<int> AddArtifactAndProject(AddArtifactAndProjectDTO input);
}