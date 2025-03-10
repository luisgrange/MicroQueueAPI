# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copia o projeto e restaura as dependências
COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app

# Usa a imagem do runtime para executar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copia os arquivos publicados
COPY --from=build /app .

# Expõe a porta 80
EXPOSE 80

# Define o comando de execução
ENTRYPOINT ["dotnet", "MicroQueueAPI.dll"]