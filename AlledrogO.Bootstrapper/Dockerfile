FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AlledrogO.Bootstrapper/AlledrogO.Bootstrapper.csproj", "AlledrogO.Bootstrapper/"]
RUN dotnet restore "AlledrogO.Bootstrapper/AlledrogO.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/AlledrogO.Bootstrapper"
RUN dotnet build "AlledrogO.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AlledrogO.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AlledrogO.Bootstrapper.dll"]
