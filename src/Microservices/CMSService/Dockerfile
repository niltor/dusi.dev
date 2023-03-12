#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Microservices/CMSService/CMSService.csproj", "src/Microservices/CMSService/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Share/Share.csproj", "src/Share/"]
COPY ["src/Core/Core.csproj", "src/Core/"]
COPY ["src/Database/EntityFramework/EntityFramework.csproj", "src/Database/EntityFramework/"]
RUN dotnet restore "src/Microservices/CMSService/CMSService.csproj"
COPY . .
WORKDIR "/src/src/Microservices/CMSService"
RUN dotnet build "CMSService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CMSService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CMSService.dll"]