# Developer Evaluation Project

---

## Descrição

API para gerenciamento de vendas, construída utilizando arquitetura DDD (Domain Driven Design), CQRS (Command Query Responsibility Segregation), validação robusta porém sem os testes automatizados.  
O projeto é modular, limpo e pronto para ser executado ou avaliado localmente.

---

## Tecnologias utilizadas

- .NET 8
- PostgreSQL
- MediatR (CQRS & Mediator)
- AutoMapper
- FluentValidation

---

## Como clonar e rodar o projeto

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/seu-projeto.git
cd seu-projeto
```

---

### 2. Configure o PostgreSQL local

Crie o banco de dados e usuário:

```sql
CREATE DATABASE developerdb;
CREATE USER devuser WITH ENCRYPTED PASSWORD 'devpass';
GRANT ALL PRIVILEGES ON DATABASE developerdb TO devuser;
```

---

### 3. Ajuste a connection string (se necessário)

Edite o arquivo `appsettings.Development.json` do projeto WebApi:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=DeveloperEvaluation;Username=devuser;Password=devpass"
  }
}
```

---

### 4. Compile, aplique as migrations e rode a aplicação

```bash
dotnet build
dotnet ef database update --project src/Ambev.DeveloperEvaluation.ORM --startup-project src/Ambev.DeveloperEvaluation.WebApi
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
```

---

### 5. Acesse a documentação da API

Acesse o Swagger em:  
[http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## Estrutura das pastas

```
src/
  Ambev.DeveloperEvaluation.Domain         # Entidades e regras de negócio
  Ambev.DeveloperEvaluation.Application    # Commands, Handlers, Validators (CQRS)
  Ambev.DeveloperEvaluation.ORM            # EF Core: contexto, repositórios, migrations
  Ambev.DeveloperEvaluation.WebApi         # Controllers, configuração, requests/responses

tests/
  Ambev.DeveloperEvaluation.Tests          # Testes automatizados (unitários e integração)
```

---

## Convenções e melhores práticas

- Arquitetura em camadas: Domain → Application → Infrastructure → WebApi
- Commands/Queries seguindo padrão CQRS
- Validações centralizadas (Application + Domain)
- Gerenciamento de dependências com injeção + MediatR
- Projeto pronto para migração e deploy (Docker, CI/CD, etc.)

---

## Como customizar a connection string

Se precisar alterar o banco de dados, edite a propriedade `DefaultConnection` em `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=developerdb;Username=devuser;Password=devpass"
  }
}
```

---


