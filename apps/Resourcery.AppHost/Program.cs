using Microsoft.Extensions.Hosting;
using Projects;

namespace Resourcery.AppHost;

internal class Program
{
    private static void Main(string[] args)
    {
        const string LaunchProfileName = "Aspire";
        var builder = DistributedApplication.CreateBuilder(args);

        var postgres = builder.AddPostgres(ResourceryNames.Postgres).WithPgWeb();
        var rabbitMq = builder.AddRabbitMQ(ResourceryNames.RabbitMq).WithManagementPlugin();
        var redis = builder.AddRedis(ResourceryNames.Redis).WithRedisCommander();
        var seq = builder.AddSeq(ResourceryNames.Seq);

        var adminDb = postgres.AddDatabase(ResourceryNames.AdministrationDb);
        var identityDb = postgres.AddDatabase(ResourceryNames.IdentityServiceDb);
        var projectsDb = postgres.AddDatabase(ResourceryNames.ProjectsDb);
        var saasDb = postgres.AddDatabase(ResourceryNames.SaaSDb);

        var migrator = builder
            .AddProject<Resourcery_DbMigrator>(
                ResourceryNames.DbMigrator,
                launchProfileName: LaunchProfileName
            )
            .WithReference(adminDb)
            .WithReference(identityDb)
            .WithReference(projectsDb)
            .WithReference(saasDb)
            .WithReference(seq)
            .WaitFor(postgres);

        var admin = builder
            .AddProject<Resourcery_Administration_HttpApi_Host>(
                ResourceryNames.AdministrationApi,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(adminDb)
            .WithReference(identityDb)
            .WithReference(rabbitMq)
            .WithReference(redis)
            .WithReference(seq)
            .WaitForCompletion(migrator);

        var identity = builder
            .AddProject<Resourcery_IdentityService_HttpApi_Host>(
                ResourceryNames.IdentityServiceApi,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(adminDb)
            .WithReference(identityDb)
            .WithReference(saasDb)
            .WithReference(rabbitMq)
            .WithReference(redis)
            .WithReference(seq)
            .WaitForCompletion(migrator);

        var saas = builder
            .AddProject<Resourcery_SaaS_HttpApi_Host>(
                ResourceryNames.SaaSApi,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(adminDb)
            .WithReference(saasDb)
            .WithReference(rabbitMq)
            .WithReference(redis)
            .WithReference(seq)
            .WaitForCompletion(migrator);

        builder
            .AddProject<Resourcery_Projects_HttpApi_Host>(
                ResourceryNames.ProjectsApi,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(adminDb)
            .WithReference(projectsDb)
            .WithReference(rabbitMq)
            .WithReference(redis)
            .WithReference(seq)
            .WaitForCompletion(migrator);

        var gateway = builder
            .AddProject<Resourcery_Gateway>(ResourceryNames.Gateway, launchProfileName: LaunchProfileName)
            .WithExternalHttpEndpoints()
            .WithReference(seq)
            .WaitFor(admin)
            .WaitFor(identity)
            .WaitFor(saas);

        var authserver = builder
            .AddProject<Resourcery_AuthServer>(
                ResourceryNames.AuthServer,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(adminDb)
            .WithReference(identityDb)
            .WithReference(saasDb)
            .WithReference(rabbitMq)
            .WithReference(redis)
            .WithReference(seq)
            .WaitForCompletion(migrator);

        builder
            .AddProject<Resourcery_WebApp_Blazor>(
                ResourceryNames.WebAppClient,
                launchProfileName: LaunchProfileName
            )
            .WithExternalHttpEndpoints()
            .WithReference(seq)
            .WaitFor(authserver)
            .WaitFor(gateway);

        builder.Build().Run();
    }
}
