# Build stage
# IMPORTANT: change the dotnet/sdk version to the one that your Blazor app targets
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0-noble AS build
WORKDIR /source

RUN apt update \
    && apt install -y python3 \
    && dotnet workload install wasm-tools

COPY . .
RUN dotnet publish Encurtador.Blazor2.csproj --output /app

# Runtime stage
FROM nginx:latest
COPY --from=build /app/wwwroot/ /usr/share/nginx/html/
COPY nginx.conf /etc/nginx/nginx.conf