## See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
## This stage is used when running from VS in fast mode (Default for Debug configuration)
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER $APP_UID
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#
## This stage is used to build the service project
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["Portfolio_API/Portfolio_API.csproj", "Portfolio_API/"]
#COPY ["portfolio_service/Portfolio_service.csproj", "portfolio_service/"]
#RUN dotnet restore "./Portfolio_API/Portfolio_API.csproj"
#COPY . .
#WORKDIR "/src/Portfolio_API"
#RUN dotnet build "./Portfolio_API.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
## This stage is used to publish the service project to be copied to the final stage
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./Portfolio_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Portfolio_API.dll"]

# בסיס להרצת האפליקציה
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# במידה ואת רוצה לעבוד עם קובץ סוד, קודם למדי שורש
USER root
RUN mkdir -p /etc/secrets && chmod 755 /etc/secrets

# החזרת המשתמש לריצה בטוחה (אם יש לך APP_UID מוגדר)
ARG APP_UID=1000
USER $APP_UID

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# שלב בנייה
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Portfolio_API/Portfolio_API.csproj", "Portfolio_API/"]
COPY ["portfolio_service/Portfolio_service.csproj", "portfolio_service/"]
RUN dotnet restore "./Portfolio_API/Portfolio_API.csproj"
COPY . .
WORKDIR "/src/Portfolio_API"
RUN dotnet build "./Portfolio_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# שלב פרסום (Publish)
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Portfolio_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# שלב סופי - הרצת האפליקציה
FROM base AS final
WORKDIR /app

# העתקת הקבצים שפורסמו
COPY --from=publish /app/publish .

# אם יש קובץ סוד מקומי שאת רוצה להעתיק - רק לדיבוג (לא בייצור!):
# COPY ./path/to/github-secret.json /etc/secrets/github-secret.json

ENTRYPOINT ["dotnet", "Portfolio_API.dll"]
