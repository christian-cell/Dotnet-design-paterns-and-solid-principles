FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Adapter.Injection/Adapter.Injection.csproj", "Adapter.Injection/"]
RUN dotnet restore "Adapter.Injection/Adapter.Injection.csproj"
COPY . .
WORKDIR "/src/Adapter.Injection"
RUN dotnet build "Adapter.Injection.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Adapter.Injection.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adapter.Injection.dll"]
