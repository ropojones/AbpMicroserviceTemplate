namespace Resourcery.IdentityService;

public static class IdentityServiceDbProperties
{
    public const string ConnectionStringName = "ResourceryIdentityServiceDb";
    public static string DbTablePrefix { get; set; } = "IdentityService";

    public static string DbSchema { get; set; } = null;
}
