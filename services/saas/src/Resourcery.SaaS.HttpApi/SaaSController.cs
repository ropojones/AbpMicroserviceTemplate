using Resourcery.SaaS.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Resourcery.SaaS;

public abstract class SaaSController : AbpControllerBase
{
    protected SaaSController()
    {
        LocalizationResource = typeof(SaaSResource);
    }
}
