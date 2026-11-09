using Serilog;
using Resourcery.Administration.EntityFrameworkCore;
using Resourcery.Projects.EntityFrameworkCore;
using Resourcery.SaaS.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Resourcery.DbMigrator;

internal class Program
{
    private static async Task Main(string[] args)
    {
        ResourceryLogging.Initialize();

        var builder = Host.CreateApplicationBuilder(args);

        builder.AddServiceDefaults();

        builder.AddNpgsqlDbContext<AdministrationDbContext>(
            connectionName: ResourceryNames.AdministrationDb
        );
        builder.AddNpgsqlDbContext<IdentityDbContext>(connectionName: ResourceryNames.IdentityServiceDb);
        builder.AddNpgsqlDbContext<SaaSDbContext>(connectionName: ResourceryNames.SaaSDb);
        builder.AddNpgsqlDbContext<ProjectsDbContext>(connectionName: ResourceryNames.ProjectsDb);

        builder.Configuration.AddAppSettingsSecretsJson();

        builder.Logging.AddSerilog();

        builder.Services.AddHostedService<DbMigratorHostedService>();

        var host = builder.Build();

        await host.RunAsync();
    }
}
