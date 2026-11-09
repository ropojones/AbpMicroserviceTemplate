using Resourcery.SaaS.Localization;
using Volo.Abp.Application.Services;

namespace Resourcery.SaaS;

public abstract class SaaSAppService : ApplicationService
{
    protected SaaSAppService()
    {
        LocalizationResource = typeof(SaaSResource);
        ObjectMapperContext = typeof(SaaSApplicationModule);
    }
}
