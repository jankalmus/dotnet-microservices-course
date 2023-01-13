FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TodoGateway/TodoGateway.csproj", "TodoGateway/"]
RUN dotnet restore "TodoGateway/TodoGateway.csproj"
COPY . .
WORKDIR "/src/TodoGateway"
RUN dotnet build "TodoGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoGateway.dll"]
