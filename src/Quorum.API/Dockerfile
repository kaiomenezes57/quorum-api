FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/Quorum.Domain/Quorum.Domain.csproj src/Quorum.Domain/
COPY src/Quorum.Application/Quorum.Application.csproj src/Quorum.Application/
COPY src/Quorum.Infrastructure/Quorum.Infrastructure.csproj src/Quorum.Infrastructure/
COPY src/Quorum.API/Quorum.API.csproj src/Quorum.API/
RUN dotnet restore src/Quorum.API/Quorum.API.csproj

COPY src/ src/
RUN dotnet publish src/Quorum.API/Quorum.API.csproj -c Development -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Quorum.API.dll"]