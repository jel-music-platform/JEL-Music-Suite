namespace JELMusic.Application.Projects.UpdateProject;

public sealed record UpdateProjectResult(
    Guid ProjectId,
    int ProjectVersion);