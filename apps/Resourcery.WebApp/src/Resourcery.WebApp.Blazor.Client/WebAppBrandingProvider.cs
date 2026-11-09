using Microsoft.Extensions.Localization;
using Resourcery.WebApp.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Resourcery.WebApp.Blazor.Client;

[Dependency(ReplaceServices = true)]
public class WebAppBrandingProvider(IStringLocalizer<WebAppResource> localizer)
    : DefaultBrandingProvider
{
    private readonly IStringLocalizer<WebAppResource> _localizer = localizer;

    public override string AppName => _localizer["AppName"];
}
