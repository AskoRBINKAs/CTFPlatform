FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER root
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
RUN apk add --upgrade krb5-libs
RUN apk add --upgrade ca-certificates
RUN apk add --no-cache icu-libs
RUN apk add --no-cache icu-data-full

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CTFPlatform/CTFPlatform.csproj", "CTFPlatform/"]
RUN dotnet restore "CTFPlatform/CTFPlatform.csproj"
COPY . .
WORKDIR "/src/CTFPlatform"
RUN dotnet build "CTFPlatform.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CTFPlatform.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CTFPlatform.dll"]
