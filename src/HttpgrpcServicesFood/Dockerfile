#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/HttpgrpcServicesFood/httpgrpcservicesfood.csproj", "src/HttpgrpcServicesFood/"]
COPY ["src/HttpgrpcCommon/HttpgrpcCommon.csproj", "src/HttpgrpcCommon/"]
RUN dotnet restore "src/HttpgrpcServicesFood/httpgrpcservicesfood.csproj"
COPY . .
WORKDIR "/src/src/HttpgrpcServicesFood"
RUN dotnet build "httpgrpcservicesfood.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "httpgrpcservicesfood.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "httpgrpcservicesfood.dll"]
