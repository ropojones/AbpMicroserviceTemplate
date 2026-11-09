# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/identity/NuGet.Config", "services/identity/"]
COPY ["services/projects/NuGet.Config", "services/projects/"]
COPY ["services/saas/NuGet.Config", "services/saas/"]
COPY ["shared/Resourcery.DbMigrator/Resourcery.DbMigrator.csproj", "shared/Resourcery.DbMigrator/"]
COPY ["services/administration/src/Resourcery.Administration.Application.Contracts/Resourcery.Administration.Application.Contracts.csproj", "services/administration/src/Resourcery.Administration.Application.Contracts/"]
COPY ["services/administration/src/Resourcery.Administration.Domain.Shared/Resourcery.Administration.Domain.Shared.csproj", "services/administration/src/Resourcery.Administration.Domain.Shared/"]
COPY ["services/administration/src/Resourcery.Administration.EntityFrameworkCore/Resourcery.Administration.EntityFrameworkCore.csproj", "services/administration/src/Resourcery.Administration.EntityFrameworkCore/"]
COPY ["shared/Resourcery.Shared/Resourcery.Shared.csproj", "shared/Resourcery.Shared/"]
COPY ["services/administration/src/Resourcery.Administration.Domain/Resourcery.Administration.Domain.csproj", "services/administration/src/Resourcery.Administration.Domain/"]
COPY ["services/identity/src/Resourcery.IdentityService.Application.Contracts/Resourcery.IdentityService.Application.Contracts.csproj", "services/identity/src/Resourcery.IdentityService.Application.Contracts/"]
COPY ["services/identity/src/Resourcery.IdentityService.Domain.Shared/Resourcery.IdentityService.Domain.Shared.csproj", "services/identity/src/Resourcery.IdentityService.Domain.Shared/"]
COPY ["services/identity/src/Resourcery.IdentityService.EntityFrameworkCore/Resourcery.IdentityService.EntityFrameworkCore.csproj", "services/identity/src/Resourcery.IdentityService.EntityFrameworkCore/"]
COPY ["services/identity/src/Resourcery.IdentityService.Domain/Resourcery.IdentityService.Domain.csproj", "services/identity/src/Resourcery.IdentityService.Domain/"]
COPY ["services/projects/src/Resourcery.Projects.Application.Contracts/Resourcery.Projects.Application.Contracts.csproj", "services/projects/src/Resourcery.Projects.Application.Contracts/"]
COPY ["services/projects/src/Resourcery.Projects.Domain.Shared/Resourcery.Projects.Domain.Shared.csproj", "services/projects/src/Resourcery.Projects.Domain.Shared/"]
COPY ["services/projects/src/Resourcery.Projects.EntityFrameworkCore/Resourcery.Projects.EntityFrameworkCore.csproj", "services/projects/src/Resourcery.Projects.EntityFrameworkCore/"]
COPY ["services/projects/src/Resourcery.Projects.Domain/Resourcery.Projects.Domain.csproj", "services/projects/src/Resourcery.Projects.Domain/"]
COPY ["services/saas/src/Resourcery.SaaS.Application.Contracts/Resourcery.SaaS.Application.Contracts.csproj", "services/saas/src/Resourcery.SaaS.Application.Contracts/"]
COPY ["services/saas/src/Resourcery.SaaS.Domain.Shared/Resourcery.SaaS.Domain.Shared.csproj", "services/saas/src/Resourcery.SaaS.Domain.Shared/"]
COPY ["services/saas/src/Resourcery.SaaS.EntityFrameworkCore/Resourcery.SaaS.EntityFrameworkCore.csproj", "services/saas/src/Resourcery.SaaS.EntityFrameworkCore/"]
COPY ["services/saas/src/Resourcery.SaaS.Domain/Resourcery.SaaS.Domain.csproj", "services/saas/src/Resourcery.SaaS.Domain/"]
COPY ["shared/Resourcery.Microservice.Shared/Resourcery.Microservice.Shared.csproj", "shared/Resourcery.Microservice.Shared/"]
COPY ["shared/Resourcery.Hosting.Shared/Resourcery.Hosting.Shared.csproj", "shared/Resourcery.Hosting.Shared/"]
COPY ["shared/Resourcery.ServiceDefaults/Resourcery.ServiceDefaults.csproj", "shared/Resourcery.ServiceDefaults/"]
RUN dotnet restore "./shared/Resourcery.DbMigrator/Resourcery.DbMigrator.csproj"
COPY . .
WORKDIR "/src/shared/Resourcery.DbMigrator"
RUN dotnet build "./Resourcery.DbMigrator.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Resourcery.DbMigrator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Resourcery.DbMigrator.dll"]