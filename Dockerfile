# Dockerfile for SoccerJerseyStore project

# Stage 1: Build the project
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY SoccerJerseyStore/*.csproj ./SoccerJerseyStore/
RUN dotnet restore

# Copy everything else and build the app
COPY SoccerJerseyStore/. ./SoccerJerseyStore/
WORKDIR /app/SoccerJerseyStore
RUN dotnet publish -c Release -o /out

# Stage 2: Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Environment Variables
ENV ASPNETCORE_URLS=http://+:80 

# Expose port 80
EXPOSE 80

ENTRYPOINT ["dotnet", "SoccerJerseyStore.dll"]
