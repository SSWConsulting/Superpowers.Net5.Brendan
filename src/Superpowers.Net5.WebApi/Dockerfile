#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Superpowers.Net5.WebApi/Superpowers.Net5.WebApi.csproj", "Superpowers.Net5.WebApi/"]
COPY ["Superpowers.Net5.Models/Superpowers.Net5.Models.csproj", "Superpowers.Net5.Models/"]
COPY ["Superpowers.Net5.Blazor/Client/Superpowers.Net5.Blazor.Client.csproj", "Superpowers.Net5.Blazor/Client/"]
COPY ["Superpowers.Net5.Ef/Superpowers.Net5.Ef.csproj", "Superpowers.Net5.Ef/"]
COPY ["Superpowers.Net5.Domain/Superpowers.Net5.Domain.csproj", "Superpowers.Net5.Domain/"]
RUN dotnet restore "Superpowers.Net5.WebApi/Superpowers.Net5.WebApi.csproj"
COPY . .
WORKDIR "/src/Superpowers.Net5.WebApi"
RUN dotnet build "Superpowers.Net5.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Superpowers.Net5.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Superpowers.Net5.WebApi.dll"]