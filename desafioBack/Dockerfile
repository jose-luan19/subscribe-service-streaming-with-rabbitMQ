# Usar a imagem base do SDK para gerar o certificado
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS cert-gen
WORKDIR /app
RUN dotnet dev-certs https -ep /app/https/cert.pfx -p senha19

# Construir o certificado baseado em uma imagem menor
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copiar o certificado gerado para o contêiner base
COPY --from=cert-gen /app/https/cert.pfx /app/https/cert.pfx

# Configurar o certificado na sua aplicação
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/https/cert.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=senha19


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["desafioBack/desafioBack.csproj", "desafioBack/"]
COPY ["RabbitMQ/RabbitMQ.csproj", "RabbitMQ/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Infra/Infra.csproj", "Infra/"]
COPY ["Models/Models.csproj", "Models/"]
RUN dotnet restore "desafioBack/desafioBack.csproj"
COPY . .
WORKDIR "/src/desafioBack"
RUN dotnet build "desafioBack.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "desafioBack.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "desafioBack.dll"]