FROM microsoft/dotnet:2.1-runtime-alpine3.7
WORKDIR /app
COPY publish /app

ENTRYPOINT ["dotnet", "DataLoader.dll"]
