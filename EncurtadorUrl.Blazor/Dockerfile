# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar arquivos de projeto
COPY ["EncurtadorUrl.Blazor/EncurtadorUrl.Blazor.csproj", "EncurtadorUrl.Blazor/"]
RUN dotnet restore "EncurtadorUrl.Blazor/EncurtadorUrl.Blazor.csproj"

# Copiar tudo e publicar
COPY . .
WORKDIR "/src/EncurtadorUrl.Blazor"
RUN rm -rf **/obj && rm -rf **/bin
RUN dotnet publish "EncurtadorUrl.Blazor.csproj" -c Release -o /app/publish

# Estágio Final
FROM nginx:alpine

# 1. Remover configurações padrão que causam conflitos
RUN rm /etc/nginx/conf.d/default.conf

# 2. Copiar os arquivos do Blazor
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

# 3. Copiar sua config para o local principal
COPY EncurtadorUrl.Blazor/nginx.conf /etc/nginx/nginx.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
