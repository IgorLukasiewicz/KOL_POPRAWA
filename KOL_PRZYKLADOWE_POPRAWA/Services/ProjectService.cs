using KOL_PRZYKLADOWE_POPRAWA.DTOs;
using KOL_PRZYKLADOWE_POPRAWA.Exceptions;
using Microsoft.Data.SqlClient;

namespace KOL_PRZYKLADOWE_POPRAWA.Services;

public class ProjectService : IProjectService
{
    private readonly string _connectionString;

    public ProjectService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConn");
    }

    public async Task<ProjectInfoDTO> GetProjectInfo(int projectId)
    {
        await using SqlConnection conn = new SqlConnection(_connectionString);
        await using SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;
        cmd.CommandText = @"SELECT 
                        P.ProjectId, 
                        P.Objective, 
                        P.StartDate, 
                        P.EndDate, 
                        A.Name AS ArtifactName, 
                        A.OriginDate, 
                        I.InstitutionId, 
                        I.Name AS InstitutionName, 
                        I.FoundedYear, 
                        S.FirstName, 
                        S.LastName, 
                        S.HireDate, 
                        SA.Role
                        FROM Preservation_Project P
                        JOIN Artifact A ON P.ArtifactId = A.ArtifactId
                        JOIN Institution I ON A.InstitutionId = I.InstitutionId
                        LEFT JOIN Staff_Assignment SA ON P.ProjectId = SA.ProjectId
                        LEFT JOIN Staff S ON SA.StaffId = S.StaffId
                        WHERE P.ProjectId=@ProjectId";

        cmd.Parameters.AddWithValue("@ProjectId", projectId);
        await conn.OpenAsync();

        await using var reader = await cmd.ExecuteReaderAsync();
        ProjectInfoDTO? result = null;

        while (await reader.ReadAsync())
        {
            if (result is null)
            {
                result = new ProjectInfoDTO()
                {
                    ProjectId = reader.GetInt32(0),
                    Objective = reader.GetString(1),
                    StartDate = reader.GetDateTime(2),
                    EndDate = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                    Artifact = new ArtifactInProcectInfoDTO()
                    {
                        Name = reader.GetString(4),
                        OrginDate = reader.GetDateTime(5),
                        Institution = new InstitutionIdInArtefactDTO()
                        {
                            InstitutionId = reader.GetInt32(6),
                            Name = reader.GetString(7),
                            FoundedYear = reader.GetInt32(8),
                        }
                    },
                    StaffAssignments = new List<StaffInProcectInfoDTO>()
                };
            }
            
            if (!reader.IsDBNull(9))
            {
                result.StaffAssignments.Add(new StaffInProcectInfoDTO
                {
                    FirstName = reader.GetString(9),
                    LastName = reader.GetString(10),
                    HireDate = reader.GetDateTime(11),
                    Role = reader.GetString(12),
                });
            }
        }

        if (result is null)
            throw new NotFoundException($"Projekt {projectId} nie istnieje");

        return result;
    }
}
