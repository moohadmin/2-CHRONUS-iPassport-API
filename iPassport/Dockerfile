# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

# SDK image for building and publishing
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

# Copiar a solução inteira
COPY ["iPassport.sln", "./"]

COPY ["iPassport.Api/iPassport.Api.csproj", "iPassport.Api/"]
COPY ["iPassport.Application/iPassport.Application.csproj", "iPassport.Application/"]
COPY ["iPassport.Domain/iPassport.Domain.csproj", "iPassport.Domain/"]
COPY ["iPassport.Infra/iPassport.Infra.csproj", "iPassport.Infra/"]
COPY ["iPassport.Test/iPassport.Test.csproj", "iPassport.Test/"]

RUN dotnet restore "iPassport.sln"

# Copiar o resto dos arquivos e construir
COPY . .
WORKDIR "/src/iPassport.Api"
RUN dotnet build "iPassport.Api.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "iPassport.Api.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "iPassport.Api.dll"]
