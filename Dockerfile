# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Base image for the application runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build image to compile and publish the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar el archivo .csproj al contenedor
COPY API_COCHES.csproj ./API_COCHES.csproj

# Restaurar dependencias
RUN dotnet restore "./API_COCHES.csproj"

# Copiar el resto del código fuente después de restaurar dependencias
COPY . .

# Compilar el proyecto
WORKDIR "/src"
RUN dotnet build "./API_COCHES.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "./API_COCHES.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagen final para producción
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_COCHES.dll"]
