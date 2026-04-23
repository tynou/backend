FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 7003

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Posts.API/Posts.API.csproj", "Posts.API/"]
COPY ["Posts.Application/Posts.Application.csproj", "Posts.Application/"]
COPY ["Common.Application/Common.Application.csproj", "Common.Application/"]
COPY ["Posts.Domain/Posts.Domain.csproj", "Posts.Domain/"]
COPY ["Common.Contracts/Common.Contracts.csproj", "Common.Contracts/"]
COPY ["Posts.Infrastructure/Posts.Infrastructure.csproj", "Posts.Infrastructure/"]
COPY ["Common.Infrastructure/Common.Infrastructure.csproj", "Common.Infrastructure/"]
RUN dotnet restore "Posts.API/Posts.API.csproj"
COPY . .
WORKDIR "/src/Posts.API"
RUN dotnet build "./Posts.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Posts.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Posts.API.dll"]
