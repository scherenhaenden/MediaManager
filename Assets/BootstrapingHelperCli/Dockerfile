FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Assets/BootstrapingHelperCli/BootstrapingHelperCli.csproj", "Assets/BootstrapingHelperCli/"]
RUN dotnet restore "Assets/BootstrapingHelperCli/BootstrapingHelperCli.csproj"
COPY . .
WORKDIR "/src/Assets/BootstrapingHelperCli"
RUN dotnet build "BootstrapingHelperCli.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BootstrapingHelperCli.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BootstrapingHelperCli.dll"]
