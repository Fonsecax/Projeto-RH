# Sistema de Gestão de Recursos Humanos

Este é um projeto de um sistema de gestão de recursos humanos. A API permite gerenciar funcionários e áreas, com funcionalidades de CRUD (Criar, Ler, Atualizar, Deletar).

## Tecnologias Utilizadas

* **ASP.NET Core**: Framework para construir a API.
* **Entity Framework Core**: ORM para interagir com o banco de dados.
* **SQL Server**: Banco de dados para persistência dos dados.
* **Blazor**: Front-end *[em desenvolvimento]*

## Estrutura do Projeto

O projeto está dividido em três namespaces principais:

* **RH.API**: Projeto que contém API.
    * **RH.API.Models**: Contém as classes de modelo que representam as entidades do sistema (`Area`, `Funcionario` e a classe base `Entity`).
    * **RH.API.DTOs**: Contém os Data Transfer Objects, utilizados para transferir dados entre a API e o cliente, evitando expor o modelo completo.
    * **RH.API.Controllers**: Contém os controladores da API (`AreaController` e `FuncionarioController`), que definem os endpoints e a lógica de negócio.

* **RH.Frontend**: Projeto que contém o Front-end.

## Endpoints da API

A API expõe os seguintes endpoints:

---

### `AreaController`

**1. Listar Áreas**

* **Endpoint**: `GET /api/Area`
* **Descrição**: Retorna uma lista de todas as áreas ativas.
* **Resposta**: `200 OK` com a lista de áreas.

**2. Criar Área**

* **Endpoint**: `POST /api/Area`
* **Descrição**: Cria uma nova área.
* **Corpo da Requisição**:
    ```
    {
      "nome": "Nome da Área",
      "gestor": "Guid do Gestor (opcional)"
    }
    ```
* **Resposta**: `200 OK` em caso de sucesso ou `400 Bad Request` se os dados forem inválidos, ou `404 Not Found` se o gestor não for encontrado.

**3. Editar Gestor de Área**

* **Endpoint**: `PATCH /api/Area/{areaId}`
* **Descrição**: Atualiza o gestor de uma área.
* **Corpo da Requisição**:
    ```
    "Guid do Novo Gestor"
    ```
* **Resposta**: `200 OK` em caso de sucesso, `400 Bad Request` se os IDs forem inválidos, ou `404 Not Found` se a área ou o gestor não forem encontrados.

**4. Deletar Área**

* **Endpoint**: `DELETE /api/Area/{id}`
* **Descrição**: Desativa uma área. A remoção é lógica, não física. A área não pode ser desativada se tiver funcionários associados.
* **Resposta**: `200 OK` em caso de sucesso, `400 Bad Request` se o ID for inválido ou se a área tiver funcionários, ou `404 Not Found` se a área não for encontrada.

---

### `FuncionarioController`

**1. Listar Funcionários**

* **Endpoint**: `GET /api/Funcionario`
* **Descrição**: Retorna uma lista de todos os funcionários ativos.
* **Resposta**: `200 OK` com a lista de funcionários.

**2. Obter Funcionário por ID**

* **Endpoint**: `GET /api/Funcionario/{id}`
* **Descrição**: Retorna um único funcionário com base no ID.
* **Resposta**: `200 OK` com os dados do funcionário, `400 Bad Request` se o ID for inválido, ou `404 Not Found` se o funcionário não for encontrado.

**3. Criar Funcionário**

* **Endpoint**: `POST /api/Funcionario`
* **Descrição**: Cria um novo funcionário.
* **Corpo da Requisição**:
    ```
    {
      "nome": "Nome do Funcionário",
      "dataNascimento": "YYYY-MM-DD",
      "cargo": "Cargo do Funcionário",
      "salario": 0.00,
      "documento": "Documento",
      "area": "Guid da Área"
    }
    ```
* **Resposta**: `201 Created` em caso de sucesso, `400 Bad Request` se os dados forem inválidos ou se a área não for encontrada.

**4. Editar Funcionário**

* **Endpoint**: `PUT /api/Funcionario/{funcionarioId}`
* **Descrição**: Atualiza todos os dados de um funcionário.
* **Corpo da Requisição**:
    ```
    {
      "nome": "Novo Nome",
      "dataNascimento": "YYYY-MM-DD",
      "cargo": "Novo Cargo",
      "salario": 0.00,
      "documento": "Novo Documento",
      "area": "Novo Guid da Área"
    }
    ```
* **Resposta**: `200 OK` em caso de sucesso, `400 Bad Request` se os dados forem inválidos ou se a área não for encontrada.

**5. Atualizar Área do Funcionário**

* **Endpoint**: `PATCH /api/Funcionario/{id}`
* **Descrição**: Atualiza a área de um funcionário.
* **Corpo da Requisição**:
    ```
    "Guid da Nova Área"
    ```
* **Resposta**: `200 OK` em caso de sucesso, `400 Bad Request` se o ID da área for inválido, ou `404 Not Found` se a área ou o funcionário não forem encontrados.

**6. Deletar Funcionário**

* **Endpoint**: `DELETE /api/Funcionario/{id}`
* **Descrição**: Desativa um funcionário. A remoção é lógica, não física.
* **Resposta**: `200 OK` em caso de sucesso, `400 Bad Request` se o ID for inválido, ou `404 Not Found` se o funcionário não for encontrado.