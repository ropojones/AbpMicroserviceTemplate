namespace Resourcery.Projects;

public static class ProjectsDbProperties
{
    public const string ConnectionStringName = "ResourceryProjectsDb";
    public static string DbTablePrefix { get; set; } = "Projects";

    public static string DbSchema { get; set; } = null;
}
