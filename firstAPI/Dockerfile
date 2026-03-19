# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy and restore
COPY firstAPI/*.csproj ./firstAPI/
RUN dotnet restore ./firstAPI/

# Copy everything and build
COPY firstAPI/. ./firstAPI/
WORKDIR /app/firstAPI
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/firstAPI/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "firstAPI.dll"]
