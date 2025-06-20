namespace KOL_PRZYKLADOWE_POPRAWA.DTOs;

public class ProjectInfoDTO
{
    public int ProjectId { get; set; }
    public string Objective { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ArtifactInProcectInfoDTO Artifact { get; set; }
    public List<StaffInProcectInfoDTO> StaffAssignments { get; set; }
}

public class ArtifactInProcectInfoDTO
{
    public string Name {get; set;}
    public DateTime OrginDate { get; set; }
    public InstitutionIdInArtefactDTO Institution { get; set; }
}

public class InstitutionIdInArtefactDTO
{
    public int InstitutionId { get; set; }
    public string Name { get; set; }
    public int FoundedYear { get; set; }
}

public class StaffInProcectInfoDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime HireDate { get; set; }
    public string Role { get; set; }
}