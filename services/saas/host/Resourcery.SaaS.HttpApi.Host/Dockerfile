# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/saas/NuGet.Config", "services/saas/"]
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/saas/host/Resourcery.SaaS.HttpApi.Host/Resourcery.SaaS.HttpApi.Host.csproj", "services/saas/host/Resourcery.SaaS.HttpApi.Host/"]
COPY ["shared/Resourcery.Microservice.Shared/Resourcery.Microservice.Shared.csproj", "shared/Resourcery.Microservice.Shared/"]
COPY ["services/administration/src/Resourcery.Administration.EntityFrameworkCore/Resourcery.Administration.EntityFrameworkCore.csproj", "services/administration/src/Resourcery.Administration.EntityFrameworkCore/"]
COPY ["shared/Resourcery.Shared/Resourcery.Shared.csproj", "shared/Resourcery.Shared/"]
COPY ["services/administration/src/Resourcery.Administration.Domain/Resourcery.Administration.Domain.csproj", "services/administration/src/Resourcery.Administration.Domain/"]
COPY ["services/administration/src/Resourcery.Administration.Domain.Shared/Resourcery.Administration.Domain.Shared.csproj", "services/administration/src/Resourcery.Administration.Domain.Shared/"]
COPY ["shared/Resourcery.Hosting.Shared/Resourcery.Hosting.Shared.csproj", "shared/Resourcery.Hosting.Shared/"]
COPY ["shared/Resourcery.ServiceDefaults/Resourcery.ServiceDefaults.csproj", "shared/Resourcery.ServiceDefaults/"]
COPY ["services/saas/src/Resourcery.SaaS.HttpApi/Resourcery.SaaS.HttpApi.csproj", "services/saas/src/Resourcery.SaaS.HttpApi/"]
COPY ["services/saas/src/Resourcery.SaaS.Application.Contracts/Resourcery.SaaS.Application.Contracts.csproj", "services/saas/src/Resourcery.SaaS.Application.Contracts/"]
COPY ["services/administration/src/Resourcery.Administration.Application.Contracts/Resourcery.Administration.Application.Contracts.csproj", "services/administration/src/Resourcery.Administration.Application.Contracts/"]
COPY ["services/saas/src/Resourcery.SaaS.Domain.Shared/Resourcery.SaaS.Domain.Shared.csproj", "services/saas/src/Resourcery.SaaS.Domain.Shared/"]
COPY ["services/saas/src/Resourcery.SaaS.Application/Resourcery.SaaS.Application.csproj", "services/saas/src/Resourcery.SaaS.Application/"]
COPY ["services/administration/src/Resourcery.Administration.Application/Resourcery.Administration.Application.csproj", "services/administration/src/Resourcery.Administration.Application/"]
COPY ["services/saas/src/Resourcery.SaaS.Domain/Resourcery.SaaS.Domain.csproj", "services/saas/src/Resourcery.SaaS.Domain/"]
COPY ["services/saas/src/Resourcery.SaaS.EntityFrameworkCore/Resourcery.SaaS.EntityFrameworkCore.csproj", "services/saas/src/Resourcery.SaaS.EntityFrameworkCore/"]
RUN dotnet restore "./services/saas/host/Resourcery.SaaS.HttpApi.Host/Resourcery.SaaS.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/saas/host/Resourcery.SaaS.HttpApi.Host"
RUN dotnet build "./Resourcery.SaaS.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Resourcery.SaaS.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Resourcery.SaaS.HttpApi.Host.dll"]