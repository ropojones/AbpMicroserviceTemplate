using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Resourcery;

[Dependency(ReplaceServices = true)]
public class ResourceryBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Resourcery";
}
