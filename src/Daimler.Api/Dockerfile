#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Daimler.Lib/Daimler.Lib.csproj", "src/Daimler.Lib/"]
COPY ["src/Daimler.Api/Daimler.Api.csproj", "src/Daimler.Api/"]
RUN dotnet restore "src/Daimler.Api/Daimler.Api.csproj"
COPY . .
WORKDIR "src/src/Daimler.Api"
RUN dotnet build "src/Daimler.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/Daimler.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Daimler.Api.dll"]
