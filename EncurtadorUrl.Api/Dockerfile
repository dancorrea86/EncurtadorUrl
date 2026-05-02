# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar arquivos de projeto e restaurar dependências
COPY ["EncurtadorUrl.Api/EncurtadorUrl.Api.csproj", "EncurtadorUrl.Api/"]
COPY ["EncurtadorUrl.Data/EncurtadorUrl.Data.csproj", "EncurtadorUrl.Data/"]
COPY ["EncurtadorUrl.Service/EncurtadorUrl.Service.csproj", "EncurtadorUrl.Service/"]
RUN dotnet restore "EncurtadorUrl.Api/EncurtadorUrl.Api.csproj"

# Copiar o restante do código
COPY . .
WORKDIR "/src/EncurtadorUrl.Api"
RUN dotnet build "EncurtadorUrl.Api.csproj" -c Release -o /app/build

# Publicar
FROM build AS publish
RUN dotnet publish "EncurtadorUrl.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio Final (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Criar pasta para o banco SQLite e garantir permissões
RUN mkdir -p /app/Data
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "EncurtadorUrl.Api.dll"]
