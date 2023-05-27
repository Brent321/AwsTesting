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

ENV WAITFORIT_VERSION="v2.4.1"
RUN apt-get update && apt-get install -y curl
RUN curl -o /usr/local/bin/waitforit -sSL https://github.com/maxcnunes/waitforit/releases/download/$WAITFORIT_VERSION/waitforit-linux_amd64 && \
    chmod +x /usr/local/bin/waitforit

ENTRYPOINT ["waitforit", "-address=http://localstack:4566", "-timeout=120", "--", "dotnet", "AwsTesting.dll"]