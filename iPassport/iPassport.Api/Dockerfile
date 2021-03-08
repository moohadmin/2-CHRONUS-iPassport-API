#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim
EXPOSE 80
COPY /artifacts app/
WORKDIR /app
ENTRYPOINT ["dotnet", "iPassport.Api.dll"]