#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AspNetCore3Auth/AspNetCore3Auth.csproj", "AspNetCore3Auth/"]
RUN dotnet restore "AspNetCore3Auth/AspNetCore3Auth.csproj"
COPY . .
WORKDIR "/src/AspNetCore3Auth"
RUN dotnet build "AspNetCore3Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetCore3Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCore3Auth.dll"]