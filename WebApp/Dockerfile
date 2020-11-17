FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app/WebApp
ENV ASPNETCORE_URLS=http://+:8080
COPY ./out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "WebApp.dll"]