# Encurtador de URL

Esse projeto tem como objetivo criar um encurtador de URL utilizando a linguagem de programação C#.
É um projeto de estudos para aprender mais sobre desenvolvimento web e manipulação de URLs.

## Plano de Desenvolvimento

1. Criar uma API para receber as URLs a serem encurtadas.
2. Criar um banco de dados para armazenar as URLs originais e as URLs encurtadas.
3. Implementar a lógica para gerar as URLs encurtadas.
4. Criar uma interface para os usuários acessarem as URLs encurtadas.

## Tecnologias Utilizadas

- C#
- SQLIte
- Blazor (para a interface)

## Diário de Desenvolvimento

### Dia 1 - 23/04/2026 - Criação do projeto

Criei uma solução no Visual Studio.

```bash
dotnet new sln -n EncurtadorDeURL
```

Crie um repositório Git para o projeto e esse arquivo README.md.

Crie um projeto de API para o backend, utilizando o template de API do .NET (ASP.NET Core Web API). Eu fiz uma limpeza do projeto, removendo os arquivos desnecessários e 
crie um controller inicial "api/encurtador" para receber as URLs a serem encurtadas. Rodei o projeto para garantir que a API estava funcionando corretamente.

### Dia 2 - 24/04/2026 - Configuração do banco de dados

Seguindo a documentação da Microsoft, eu configurei o Entity Framework Core para utilizar o SQLite como banco de dados.
https://learn.microsoft.com/pt-br/ef/core/
Eu fiz a instalação do pacote do Entity Framework Core para SQLite, utilizando o seguinte pacote pelo Nuget:

```bash
Microsoft.EntityFrameworkCore.Sqlite
```

Também o pacote Entity Framework Core Tools Design para habilitar as migrações:
```bash
Microsoft.EntityFrameworkCore.Design
```

Também instalei o pacote do Entity Framework Core Tools para habilitar as migrações:
```bash
Microsoft.EntityFrameworkCore.Tools
```

Crie um prote DATA para o guardar as conexões de banco de dados. Crie o DbContext para o Entity Framework Core, 
definindo as entidades para as URLs originais e encurtadas. 

Eu coloquei a referencia do projeto Data no projeto da API para poder utilizar o DbContext e as entidades.
Eu configurei a string de conexão para o banco no appsettings.json da API, utilizando o seguinte formato:
```json

"ConnectionStrings": {
	"DefaultConnection": "Data Source=encurtador.db"
}
```

Após essas configurações, eu rodei as migrações para criar o banco de dados e as tabelas necessárias.

#### Programação da função para gerar as URLs encurtadas

Como decisão de design do projeto, vou ter uma interface grafica, onde o cliente vai colar uma URL, por exemplo, www.google.com.br, 
A interface gráfica vai ter um botão de encurtar, quando o cliente clicar nesse botão, o a interface gráfica vai chamar uma API, que vai 
fazer um hash da string e retornar uma url encurtada.

Essa função vai chamar o método GetHashCode do C# para gerar uma chave Hash.

Vai salvar no banco de dados esse Hash com a URL em uma esquema de chave valor.

### Dia 3 - 25/04/2026 - Implementação da interface gráfica

Eu estudei algumas formas de fazer a interface gráfica, e decidi utilizar o Blazor para criar uma aplicação web interativa.

Criei o service para realizar a criação da url encurtada, utilizando o HttpClient para fazer as requisições para a API.

A página será simples, com um campo de texto para o usuário colar a URL original e um botão para encurtar a URL.

### Dia 4 — 26/04/2026 — Desenvolvimento da Interface Gráfica

Neste dia, foquei na criação do formulário em **Blazor** e na integração com o backend para o encurtamento de URLs.

#### 1. Configuração do Cliente HTTP

Para viabilizar as requisições para a API, utilizei o pacote `Microsoft.Extensions.Http`. A instalação foi realizada via Console do Gerenciador de Pacotes:

```bash
Install-Package Microsoft.Extensions.Http
```

Em seguida, registrei o serviço do `HttpClient` no arquivo `Program.cs` para permitir a injeção de dependência em toda a aplicação:

```csharp
builder.Services.AddHttpClient();
```

#### 2. Ajustes na API e Comunicação

* **CORS:** Configurei as políticas de CORS na API, permitindo que o front-end Blazor realize requisições de forma segura e sem bloqueios pelo navegador.
* **Controller:** Refatorei o controller da API para processar as chamadas originadas pela interface gráfica, garantindo o recebimento correto dos dados e o retorno da URL encurtada.