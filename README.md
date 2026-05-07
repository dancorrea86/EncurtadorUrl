# Encurtador de URL

Este projeto tem como objetivo criar um encurtador de URL utilizando **C#**. A proposta é servir como estudo prático de desenvolvimento web, integração entre front-end e back-end, persistência de dados e manipulação de URLs.

## Objetivo do projeto

A aplicação permitirá que o usuário informe uma URL original e receba uma versão encurtada para compartilhamento. O sistema também será responsável por armazenar o vínculo entre a URL original e a URL curta, além de redirecionar corretamente o acesso quando a URL encurtada for utilizada.

## Plano de desenvolvimento

- Criar uma API para receber as URLs a serem encurtadas.
- Criar um banco de dados para armazenar as URLs originais e as URLs encurtadas.
- Implementar a lógica responsável por gerar as URLs encurtadas.
- Criar uma interface para os usuários acessarem e gerarem URLs encurtadas.

## Tecnologias utilizadas

- C#
- SQLite
- Blazor, para a interface web
- ASP.NET Core Web API, para o backend da aplicação
- Entity Framework Core, para acesso e persistência dos dados com SQLite [1][2]

## Arquitetura pensada

A solução foi organizada com separação entre interface, API e camada de dados.

### Fluxo esperado

1. O usuário cola uma URL na interface.
2. O front-end envia a requisição para a API.
3. A API gera uma chave curta para a URL.
4. A aplicação salva a relação entre URL original e URL encurtada no banco de dados.
5. Quando a URL curta é acessada, o sistema localiza a URL original e realiza o redirecionamento.

## Diário de desenvolvimento

### Dia 1 - 23/04/2026 - Criação do projeto

Foi criada uma solução no Visual Studio com o comando abaixo:

```bash
dotnet new sln -n EncurtadorDeURL
```

Também foi criado o repositório Git do projeto e este arquivo `README.md`.

Na sequência, foi criado um projeto de API para o backend usando o template de **ASP.NET Core Web API**. Depois da criação inicial, foi feita uma limpeza dos arquivos padrão e um controller inicial em `api/encurtador` foi adicionado para receber as URLs enviadas pela interface. Por fim, a aplicação foi executada para validar que a API estava funcionando corretamente.

### Dia 2 - 24/04/2026 - Configuração do banco de dados

Seguindo a documentação do Entity Framework Core, foi configurado o uso do provedor SQLite para persistência dos dados da aplicação [1][2].

Os pacotes instalados foram:

```bash
Microsoft.EntityFrameworkCore.Sqlite
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
```

Foi criado um projeto `Data` para concentrar a configuração de acesso ao banco. Nele, foi implementado o `DbContext` com as entidades responsáveis por armazenar as URLs originais e suas versões encurtadas.

Depois disso, o projeto de dados foi referenciado pela API para permitir o uso do `DbContext` e das entidades.

A string de conexão foi configurada no `appsettings.json` da API da seguinte forma:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=encurtador.db"
}
```

Após a configuração, foram executadas as migrações para criar o banco de dados e as tabelas necessárias.

### Geração da URL encurtada

Como decisão de design, a aplicação terá uma interface gráfica em que o usuário poderá colar uma URL, como por exemplo `www.google.com.br`, e clicar em um botão para gerar a versão encurtada.

A interface fará uma chamada para a API, que ficará responsável pela lógica de geração da chave curta e pelo armazenamento do vínculo entre a chave e a URL original.

A ideia inicial foi utilizar o método `GetHashCode()` do C# para gerar uma chave hash. No entanto, para produção ou para evitar colisões e inconsistências entre execuções, essa estratégia merece revisão, já que o valor gerado por `GetHashCode()` não é uma escolha confiável como identificador público persistente. Uma abordagem baseada em identificador único, codificação Base62 ou geração de chave aleatória tende a ser mais estável para esse tipo de sistema.

### Dia 3 - 25/04/2026 - Implementação da interface gráfica

Após estudar algumas alternativas para a interface, foi decidido utilizar **Blazor** para criar uma aplicação web interativa.

Foi criado um serviço responsável por solicitar a criação da URL encurtada, utilizando `HttpClient` para comunicação com a API. Esse padrão é coerente com a forma como aplicações Blazor consomem APIs HTTP [3].

A página inicial foi planejada com uma proposta simples:

- Um campo de texto para colar a URL original.
- Um botão para acionar o encurtamento.
- A exibição da URL curta retornada pela API.

### Dia 4 - 26/04/2026 - Desenvolvimento da interface gráfica

Neste dia, o foco foi a criação do formulário em Blazor e a integração com o backend.

#### 1. Configuração do cliente HTTP

Para viabilizar as requisições HTTP, foi utilizado o pacote abaixo:

```bash
Install-Package Microsoft.Extensions.Http
```

Em seguida, o serviço foi registrado no `Program.cs` com:

```csharp
builder.Services.AddHttpClient();
```

Esse registro permite a criação e injeção de clientes HTTP configurados na aplicação, prática comum no ecossistema ASP.NET Core e Blazor [3].

#### 2. Ajustes na API e comunicação

Foi necessário configurar **CORS** na API para permitir chamadas do front-end hospedado em outra origem. Na documentação da Microsoft, CORS é descrito como o mecanismo que permite ao servidor liberar requisições entre origens diferentes, contornando a política padrão do navegador de mesma origem [4][5].

Também foi feita uma refatoração do controller da API para processar corretamente as chamadas vindas da interface e retornar a URL encurtada.

> Observação importante: ao configurar CORS no ASP.NET Core, a documentação recomenda atenção à ordem do middleware. `UseCors` deve ser executado depois de `UseRouting` e antes de `UseAuthorization` [5].

### Dia 5 - 30/04/2026 - Desenvolvimento do redirecionamento

Foi criado o controller de redirecionamento, além do serviço responsável por localizar a URL original a partir da chave encurtada.

Também foram adicionadas validações para garantir que apenas URLs válidas sejam processadas.

#### Copiar URL para o clipboard

Além do redirecionamento, foi criado um serviço para copiar a URL encurtada para a área de transferência do usuário, utilizando a API de Clipboard do navegador.

### Dia 6 - 05/05/2026 - Configuração do ambiente de produção

Nesta etapa surgiram dificuldades na configuração do Blazor com Docker Compose.

Para resolver a estrutura de execução, foi necessário criar:

- Um `Dockerfile` para o projeto Blazor.
- Um `Dockerfile` para o projeto da API.

Essa separação ajuda a organizar melhor o processo de build e execução de cada parte da aplicação no ambiente de containers.

## Próximos passos sugeridos

- Substituir `GetHashCode()` por uma estratégia de geração de chave mais estável.
- Adicionar tratamento para URLs duplicadas, evitando gerar novos códigos para o mesmo endereço sem necessidade.
- Implementar expiração opcional para links encurtados.
- Adicionar métricas simples de acesso, como quantidade de cliques por URL.
- Publicar a aplicação com configuração completa em Docker Compose.

## Referências

- Documentação do provedor SQLite para EF Core: [Microsoft Learn](https://learn.microsoft.com/pt-br/ef/core/providers/sqlite/) [1][2]
- Documentação sobre CORS no ASP.NET Core: [Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-10.0) [4][5]