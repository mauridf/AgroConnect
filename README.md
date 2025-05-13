# AgroConnect API

API para gestão de produtores rurais, fazendas e culturas agrícolas.

## 📌 Visão Geral

A AgroConnect API é uma solução RESTful para gerenciamento de dados agrícolas, incluindo:

- Cadastro de produtores rurais
- Gestão de fazendas e propriedades
- Controle de culturas plantadas
- Dashboard com métricas agrícolas

## 🚀 Começando

### Pré-requisitos

- .NET 9.0+
- PostgreSQL 12+

### Instalação

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/agroconnect-api.git
   ```

2. Configure o banco de dados:

   Edite a connection string no `appsettings.json`.

3. Execute as migrations:
   ```bash
   dotnet ef database update
   ```

4. Execute a aplicação:
   ```bash
   dotnet run
   ```

## 🌐 Endpoints

### Auth

- `POST /api/autenticacao/register` - Registrar um novo Usuário.
- `POST /api/autenticacao/login` - Faz login e gera um token. Ao efetuar o Login com sucesso o resultado além do Token é para trazer o Usuario e TipoUsuario.
- `POST /api/autenticacao/refresh-token` - Renova o Token.
- `POST /api/autenticacao/revoke-token` - Revoga o Token.
- `POST /api/autenticacao/request-password-reset` - Efetua o pedido de reset da senha.
- `POST /api/autenticacao/reset-password` - Reseta a senha.
- `GET /api/autenticacao/confirm-email` - Registra a confirmação por e-mail.

### Usuarios

- `GET /api/usuario/{id}` - Busca o Usuário pelo ID.
- `PUT /api/usuario/{id}` - Altera informações de Usuário.
- `DELETE /api/usuario/{id}` - Deleta Usuário.
- `GET /api/usuario/nome/{nomeUsuario}` - Buscar o Usuário pelo nome.
- `POST /api/usuario/` - Registrar um novo Usuário.

### Produtor

- `POST /api/produtor` - Cadastra um novo produtor rural
- `GET /api/produtor` - Listar todos os produtores
- `GET /api/produtor/uf/{uf}` - Listar todos os produtores de uma determinado UF
- `GET /api/produtor/{id}` - Obtém um produtor por ID
- `GET /api/produtor/cpf/{cpf}` - Busca produtor por CPF
- `PUT /api/produtor` - Atualiza um produtor
- `DELETE /api/produtor/{id}` - Remove um produtor

### Fazenda

- `POST /api/fazenda` - Cadastra uma nova fazenda
- `GET /api/fazenda` - Lista todas as fazendas
- `GET /api/fazenda/uf/{uf}` - Lista todas as fazendas de uma determinada UF.
- `GET /api/fazenda/{id}` - Obtém fazenda por ID
- `GET /api/fazenda/produtor/{produtorId}` - Obtém fazenda pelo produtor.
- `GET /api/fazenda/cnpj/{cnpj}` - Busca fazenda por CNPJ
- `PUT /api/fazenda` - Atualiza uma fazenda
- `DELETE /api/fazenda/{id}` - Remove uma fazenda

### Cultura

- `POST /api/cultura` - Cadastra uma nova cultura
- `GET /api/cultura` - Lista todas as culturas
- `GET /api/cultura/{id}` - Obtém cultura por ID
- `GET /api/cultura/categoria/{categoria}` - Filtra por categoria
- `PUT /api/cultura` - Atualiza uma cultura
- `DELETE /api/cultura/{id}` - Remove uma cultura

### FazendaCultura (Relacionamentos)

- `POST /api/fazendacultura` - Associa cultura a fazenda
- `PUT /api/fazendacultura` - Altera a associação de cultura e fazenda
- `GET /api/fazendacultura/fazenda/{fazendaId}` - Culturas de uma fazenda
- `GET /api/fazendacultura/areautilizada/{fazendaId}` - Mostra a area utilizada para produção de cultura(s) em uma fazenda
- `GET /api/fazendacultura/{id}` - Obter a associação de cultura e fazenda pelo id
- `DELETE /api/fazendacultura/{id}` - Remove associação

### Dashboard

- `GET /api/dashboard/resumo-geral` - Resumo geral
- `GET /api/dashboard/total-fazendas` - Total de fazendas
- `GET /api/dashboard/total-fazendas-por-uf` - Total de fazendas por UF
- `GET /api/dashboard/total-hectares` - Total de hectares
- `GET /api/dashboard/total-hectares-por-uf` - Total de hectares por uf
- `GET /api/dashboard/total-hectares-agricultavel` - Total de hectares agricultáveis
- `GET /api/dashboard/total-hectares-agricultavel-por-uf` - Total de hectares agricultáveis por uf
- `GET /api/dashboard/total-produdores` - Tota de produtores
- `GET /api/dashboard/total-produtores-por-uf` - Total de produtores por uf
- `GET /api/dashboard/total-culturas-plantadas` - Total Geral de Culturas plantadas
- `GET /api/dashboard/total-culturas-plantadas-por-culturas` - Total Geral de Culturas plantadas dividido por culturas
- `GET /api/dashboard/total-culturas-plantadas-por-uf` - Total Geral de culturas plantadas por uf
- `GET /api/dashboard/total-culturas-plantadas-por-categoria` - Total Geral de culturas plantadas dividido por categoria
- `GET /api/dashboard/total-culturas-plantadas-por-categoria-por-uf/{uf}` - Total Geral de culturas plantadas divido por categoria em determinada Uf.

### Error

- `GET /error` - Tratamento de erro

## 🔐 Autenticação

A API utiliza JWT para autenticação. Inclua o token no header:

```
Authorization: Bearer {seu-token}
```

## 📊 Modelos de Dados

### Produtor Rural

```json
{
  "id": "guid",
  "nome": "string",
  "cpf": "string",
  "email": "string",
  "telefone": "string",
  "endereco": {
    "logradouro": "string",
    "cidade": "string",
    "uf": "string",
    "cep": "string"
  }
}
```

### Fazenda

```json
{
  "id": "guid",
  "nome": "string",
  "cnpj": "string",
  "areaTotalHectares": 0,
  "areaAgricultavelHectares": 0,
  "endereco": {
    "logradouro": "string",
    "cidade": "string",
    "uf": "string",
    "cep": "string"
  }
}
```

## 🛠️ Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- AutoMapper
- Swagger/OpenAPI
- JWT Authentication

## 📄 Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo LICENSE para detalhes.

## 🤝 Contribuição

Contribuições são bem-vindas! Siga os passos:

1. Faça um fork do projeto
2. Crie sua branch (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## ✉️ Contato

Desenvolvedor da API - mauridf@gmail.com

## 📌 Notas Adicionais

1. Para visualização interativa da API, acesse `/swagger` quando a aplicação estiver rodando
2. Certifique-se de configurar corretamente as variáveis de ambiente para produção
3. A API segue os princípios RESTful e padrões de boas práticas

Este README fornece uma visão geral da API. Para detalhes completos de cada endpoint, consulte a documentação Swagger disponível quando a aplicação estiver em execução.