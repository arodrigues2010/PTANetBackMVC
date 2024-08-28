FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EsettMvcIntegration/EsettMvcIntegration.csproj", "EsettMvcIntegration/"]
RUN dotnet restore "EsettMvcIntegration/EsettMvcIntegration.csproj"
COPY . .
WORKDIR "/src/EsettMvcIntegration"
RUN dotnet build "EsettMvcIntegration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EsettMvcIntegration.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EsettMvcIntegration.dll"]
