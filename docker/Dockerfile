# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Set the working directory inside the container
WORKDIR /app

# Move to the output directory
COPY ./out .

EXPOSE 8080

# Set the entry point for the application
ENTRYPOINT ["dotnet", "CarParkSG.dll"]