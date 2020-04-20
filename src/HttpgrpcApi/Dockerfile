#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/HttpgrpcApi/httpgrpcapigateway.csproj", "src/HttpgrpcApi/"]
COPY ["src/HttpgrpcCommon/HttpgrpcCommon.csproj", "src/HttpgrpcCommon/"]
RUN dotnet restore "src/HttpgrpcApi/httpgrpcapigateway.csproj"
COPY . .
WORKDIR "/src/src/HttpgrpcApi"
RUN dotnet build "httpgrpcapigateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "httpgrpcapigateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENV ASPNETCORE_ENVIRONMENT container
ENTRYPOINT ["dotnet", "httpgrpcapigateway.dll"]
