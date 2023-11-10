FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Seeder/Seeder.csproj", "Seeder/"]
RUN dotnet restore "Seeder/Seeder.csproj"
COPY . .
WORKDIR "/src/Seeder"
RUN dotnet build "Seeder.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Seeder.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Seeder.dll"]
