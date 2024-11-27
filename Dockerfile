# Stage 1: Build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY ./*.csproj ./
RUN dotnet restore

# Copy everything else and build the app
COPY . ./
RUN dotnet publish -c Release -o /out

# Stage 2: Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Environment Variables
ENV ASPNETCORE_URLS=http://+:80 

# Expose port 80
EXPOSE 80

ENTRYPOINT ["dotnet", "SoccerJerseyStore.dll"]
