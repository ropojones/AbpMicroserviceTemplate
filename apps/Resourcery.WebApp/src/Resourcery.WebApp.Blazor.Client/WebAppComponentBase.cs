using Resourcery.WebApp.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Resourcery.WebApp.Blazor.Client;

public abstract class WebAppComponentBase : AbpComponentBase
{
    protected WebAppComponentBase()
    {
        LocalizationResource = typeof(WebAppResource);
    }
}
