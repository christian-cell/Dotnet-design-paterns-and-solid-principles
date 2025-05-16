FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Creational.Prototype/Creational.Prototype.csproj", "Creational.Prototype/"]
RUN dotnet restore "Creational.Prototype/Creational.Prototype.csproj"
COPY . .
WORKDIR "/src/Creational.Prototype"
RUN dotnet build "Creational.Prototype.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Creational.Prototype.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Creational.Prototype.dll"]
