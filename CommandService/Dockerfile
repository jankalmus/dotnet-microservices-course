FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./CommandService/CommandService.csproj", "CommandService/"]
COPY ["./Infrastructure.Data.Contracts/Infrastructure.Data.Contracts.csproj", "Infrastructure.Data.Contracts/"]

RUN dotnet restore "CommandService/CommandService.csproj"
RUN dotnet restore "Infrastructure.Data.Contracts/Infrastructure.Data.Contracts.csproj"

COPY "./CommandService" "CommandService/"
COPY "./Infrastructure.Data.Contracts/" "Infrastructure.Data.Contracts/"

WORKDIR "/src/CommandService"
RUN dotnet build "CommandService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CommandService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommandService.dll"]
