# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Configurar el directorio de trabajo
WORKDIR /src

# Copiar el archivo de proyecto y restaurar las dependencias
COPY EsettMvcIntegration.csproj ./
RUN dotnet restore EsettMvcIntegration.csproj

# Copiar el resto del código fuente y construir la aplicación
COPY . .
RUN dotnet build EsettMvcIntegration.csproj -c Release -o /app/build

# Crear la etapa de publicación
FROM build AS publish
RUN dotnet publish EsettMvcIntegration.csproj -c Release -o /app/publish

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Configurar el directorio de trabajo
WORKDIR /app

# Exponer el puerto en el que la aplicación escuchará
EXPOSE 80

# Copiar los archivos publicados desde la etapa de publicación
COPY --from=publish /app/publish .

# Configurar el punto de entrada para ejecutar la aplicación
ENTRYPOINT ["dotnet", "EsettMvcIntegration.dll"]
