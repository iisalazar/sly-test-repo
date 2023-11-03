FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BackgroundWorkers/BackgroundWorkers.csproj", "BackgroundWorkers/"]
RUN dotnet restore "BackgroundWorkers/BackgroundWorkers.csproj"
COPY . .
WORKDIR "/src/BackgroundWorkers"
RUN dotnet build "BackgroundWorkers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BackgroundWorkers.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackgroundWorkers.dll"]
