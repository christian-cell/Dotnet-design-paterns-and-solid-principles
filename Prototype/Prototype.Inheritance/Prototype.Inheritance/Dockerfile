FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Prototype.Inheritance/Prototype.Inheritance.csproj", "Prototype.Inheritance/"]
RUN dotnet restore "Prototype.Inheritance/Prototype.Inheritance.csproj"
COPY . .
WORKDIR "/src/Prototype.Inheritance"
RUN dotnet build "Prototype.Inheritance.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Prototype.Inheritance.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Prototype.Inheritance.dll"]
