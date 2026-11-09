using Volo.Abp.Modularity;

namespace Resourcery.Administration;

[DependsOn(typeof(AdministrationApplicationModule))]
[DependsOn(typeof(AdministrationDomainTestModule))]
public class AdministrationApplicationTestModule : AbpModule { }
