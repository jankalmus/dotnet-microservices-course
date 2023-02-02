FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./PlatformService/PlatformService.csproj", "PlatformService/"]
COPY ["./Infrastructure.Data.Contracts/Infrastructure.Data.Contracts.csproj", "Infrastructure.Data.Contracts/"]

RUN dotnet restore "PlatformService/PlatformService.csproj"
RUN dotnet restore "Infrastructure.Data.Contracts/Infrastructure.Data.Contracts.csproj"

COPY "./PlatformService" "PlatformService/"
COPY "./Infrastructure.Data.Contracts/" "Infrastructure.Data.Contracts/"

WORKDIR "/src/PlatformService"
RUN dotnet build "PlatformService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlatformService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlatformService.dll"]
