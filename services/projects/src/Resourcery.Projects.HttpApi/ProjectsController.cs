using Resourcery.Projects.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Resourcery.Projects;

public abstract class ProjectsController : AbpControllerBase
{
    protected ProjectsController()
    {
        LocalizationResource = typeof(ProjectsResource);
    }
}
