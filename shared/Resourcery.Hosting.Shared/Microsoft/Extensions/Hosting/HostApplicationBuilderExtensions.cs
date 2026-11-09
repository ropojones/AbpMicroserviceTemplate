using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.Hosting;

public static class HostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddSharedEndpoints(this IHostApplicationBuilder builder)
    {
        builder.AddRabbitMQClient(
            connectionName: ResourceryNames.RabbitMq,
            action =>
                action.ConnectionString = builder.Configuration.GetConnectionString(
                    ResourceryNames.RabbitMq
                )
        );
        builder.AddRedisDistributedCache(connectionName: ResourceryNames.Redis);
        builder.AddSeqEndpoint(connectionName: ResourceryNames.Seq);

        return builder;
    }
}
