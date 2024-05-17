FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bootstrapper/AlledrogO.Bootstrapper/AlledrogO.Bootstrapper.csproj", "Bootstrapper/AlledrogO.Bootstrapper/"]
COPY ["Modules/AlledrogO.Post/AlledrogO.Post.Domain/AlledrogO.Post.Domain.csproj", "Modules/AlledrogO.Post/AlledrogO.Post.Domain/"]
COPY ["Shared/AlledrogO.Shared/AlledrogO.Shared.csproj", "Shared/AlledrogO.Shared/"]
COPY ["Modules/AlledrogO.Post/AlledrogO.Post.Application/AlledrogO.Post.Application.csproj", "Modules/AlledrogO.Post/AlledrogO.Post.Application/"]
COPY ["Modules/AlledrogO.Post/AlledrogO.Post.Api/AlledrogO.Post.Api.csproj", "Modules/AlledrogO.Post/AlledrogO.Post.Api/"]
COPY ["Modules/AlledrogO.Post/AlledrogO.Post.Infrastructure/AlledrogO.Post.Infrastructure.csproj", "Modules/AlledrogO.Post/AlledrogO.Post.Infrastructure/"]
RUN dotnet restore "Bootstrapper/AlledrogO.Bootstrapper/AlledrogO.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/Bootstrapper/AlledrogO.Bootstrapper"
RUN dotnet build "AlledrogO.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AlledrogO.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /app/wwwroot/images
RUN chmod -R 777 /app/wwwroot/images
USER $APP_UID
ENTRYPOINT ["dotnet", "AlledrogO.Bootstrapper.dll"]
