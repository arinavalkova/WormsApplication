﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WormsWeb/WormsWeb.csproj", "WormsWeb/"]
COPY ["EntitiesLibrary/EntitiesLibrary.csproj", "EntitiesLibrary/"]
RUN dotnet restore "WormsWeb/WormsWeb.csproj"
COPY . .
WORKDIR "/src/WormsWeb"
RUN dotnet build "WormsWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WormsWeb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WormsWeb.dll"]
