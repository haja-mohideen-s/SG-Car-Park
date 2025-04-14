#!/bin/bash

TAG=${1?: "Supply a tag for the image"}

# Remove any existing Docker image with the same tag
docker rmi -f sg-car-park:$TAG 2>/dev/null || true

# Restore, build, and publish the .NET project
dotnet restore ../
dotnet build --no-restore ../
dotnet publish ../ -c Release -o ./out

# Build the Docker image
docker build -t sg-car-park:$TAG .

#remove temp publish directory
rm -rf ./out