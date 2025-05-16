FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Singleton.Implementation/Singleton.Implementation.csproj", "Singleton.Implementation/"]
RUN dotnet restore "Singleton.Implementation/Singleton.Implementation.csproj"
COPY . .
WORKDIR "/src/Singleton.Implementation"
RUN dotnet build "Singleton.Implementation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Singleton.Implementation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Singleton.Implementation.dll"]
