using Resourcery.Administration.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Resourcery.Administration;

public abstract class AdministrationController : AbpControllerBase
{
    protected AdministrationController()
    {
        LocalizationResource = typeof(AdministrationResource);
    }
}
