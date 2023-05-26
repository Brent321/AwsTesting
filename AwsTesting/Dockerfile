#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AwsTesting/AwsTesting.csproj", "AwsTesting/"]
RUN dotnet restore "AwsTesting/AwsTesting.csproj"
COPY . .
WORKDIR "/src/AwsTesting"
RUN dotnet build "AwsTesting.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "AwsTesting.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AwsTesting.dll"]