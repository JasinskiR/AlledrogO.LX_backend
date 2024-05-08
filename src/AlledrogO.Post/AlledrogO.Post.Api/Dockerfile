FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AlledrogO.Post/AlledrogO.Post.Api/AlledrogO.Post.Api.csproj", "src/AlledrogO.Post/AlledrogO.Post.Api/"]
RUN dotnet restore "src/AlledrogO.Post/AlledrogO.Post.Api/AlledrogO.Post.Api.csproj"
COPY . .
WORKDIR "/src/src/AlledrogO.Post/AlledrogO.Post.Api"
RUN dotnet build "AlledrogO.Post.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AlledrogO.Post.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AlledrogO.Post.Api.dll"]
