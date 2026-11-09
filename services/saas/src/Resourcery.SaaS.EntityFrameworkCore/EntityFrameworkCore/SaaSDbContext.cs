using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Resourcery.SaaS.EntityFrameworkCore;

[ConnectionStringName(ResourceryNames.SaaSDb)]
public class SaaSDbContext(DbContextOptions<SaaSDbContext> options)
    : AbpDbContext<SaaSDbContext>(options),
        ITenantManagementDbContext,
        ISaaSDbContext
{
    public DbSet<Tenant> Tenants { get; set; }

    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureSaaS();
        builder.ConfigureTenantManagement();
    }
}
