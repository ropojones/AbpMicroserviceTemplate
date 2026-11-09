using Resourcery.IdentityService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Resourcery.IdentityService;

public abstract class IdentityServiceController : AbpControllerBase
{
    protected IdentityServiceController()
    {
        LocalizationResource = typeof(IdentityServiceResource);
    }
}
