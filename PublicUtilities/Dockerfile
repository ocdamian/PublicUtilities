# Usamos la imagen base de .NET 8 para Azure Functions en Linux
FROM mcr.microsoft.com/azure-functions/dotnet:4-dotnet8.0 AS base
WORKDIR /home/site/wwwroot
EXPOSE 8080

# Instalamos las dependencias necesarias para Chromium en Linux
RUN apt-get update && apt-get install -y \
    libnspr4 \
    libnss3 \
    libatk-bridge2.0-0 \
    libatk1.0-0 \
    libx11-xcb1 \
    libxcomposite1 \
    libxdamage1 \
    libxrandr2 \
    libgbm1 \
    libasound2 \
    libpangocairo-1.0-0 \
    libpango-1.0-0 \
    libgdk-pixbuf2.0-0 \
    libatspi2.0-0 \
    fonts-liberation \
    libappindicator3-1 \
    libxss1 \
    libgdk-pixbuf2.0-0 \
    xdg-utils \
    chromium

# Esta etapa es usada para la construcción y publicación de la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PublicUtilities/PublicUtilities.csproj", "PublicUtilities/"]
RUN dotnet restore "./PublicUtilities/PublicUtilities.csproj"
COPY . . 
WORKDIR "/src/PublicUtilities"
RUN dotnet build "./PublicUtilities.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicamos la aplicación
FROM build AS publish
RUN dotnet publish "./PublicUtilities.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta etapa es usada para el contenedor final
FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .

# Verificamos que el ejecutable de Chrome esté disponible en el contenedor
RUN ls -l /usr/bin/chromium

# Establecemos las variables de entorno necesarias para la Azure Function
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true











## See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
## This stage is used when running from VS in fast mode (Default for Debug configuration)
#FROM mcr.microsoft.com/azure-functions/dotnet:4-dotnet8.0 AS base
#WORKDIR /home/site/wwwroot
#EXPOSE 8080
#
#
## This stage is used to build the service project
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["PublicUtilities/PublicUtilities.csproj", "PublicUtilities/"]
#RUN dotnet restore "./PublicUtilities/PublicUtilities.csproj"
#COPY . .
#WORKDIR "/src/PublicUtilities"
#RUN dotnet build "./PublicUtilities.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
## This stage is used to publish the service project to be copied to the final stage
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./PublicUtilities.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
#FROM base AS final
#WORKDIR /home/site/wwwroot
#COPY --from=publish /app/publish .
#ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    #AzureFunctionsJobHost__Logging__Console__IsEnabled=true







    #
## This stage is used when running from VS in fast mode (Default for Debug configuration)
#FROM mcr.microsoft.com/azure-functions/dotnet:4-dotnet8.0 AS base
#WORKDIR /home/site/wwwroot
#EXPOSE 8080
#
## This stage is used to build the service project
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["PublicUtilities/PublicUtilities.csproj", "PublicUtilities/"]
#RUN dotnet restore "./PublicUtilities/PublicUtilities.csproj"
#COPY . . 
#WORKDIR "/src/PublicUtilities"
#RUN dotnet build "./PublicUtilities.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
## This stage is used to publish the service project to be copied to the final stage
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./PublicUtilities.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
#FROM base AS final
#WORKDIR /home/site/wwwroot
#COPY --from=publish /app/publish . 
#
## Copiar la carpeta Chrome después de la publicación del proyecto
## La carpeta 'Chrome' se copia dentro de PublicUtilities\bin\Debug\net8.0\bin
#COPY Chrome /home/site/wwwroot/PublicUtilities/bin/Debug/net8.0/bin/Chrome
#
## Establecer las variables de entorno necesarias
#ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    #AzureFunctionsJobHost__Logging__Console__IsEnabled=true