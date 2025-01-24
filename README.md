# [PRAZO DE ENTREGA - 7 DIAS CORRIDOS]

# Teste de Desenvolvimento Backend

## Inicialização

- Faça um fork do repositório do projeto.
- Clone o repositório forkado para o seu ambiente local.
- Quando finalizado, envie o link do repositório para [dev@acheipneus.com.br](mailto:dev@acheipneus.com.br "‌").
- A utilização dos templates é opcional

## Funcionalidades Principais

**CRUD de Produtos, Marcas e Atributos ( Listagens ).**


## Requisitos de Desenvolvimento

- **Sem Warnings ou Erros**: O projeto deve compilar sem erros e com o mínimo de _warnings_
- **Versionamento**: Utilizar _Git_ para versionamento do código seguindo padrões
- **Geração de logger:** gerar logs permanentes de alguma ação normal e de algum erro
- **Linguagem utilizada:** C# dotnet
- **Transmissão de dados entre camadas:** Utilizar DTO para ocultar informações que não devem ser exibidas no front.
- **Api Restful:** Utilizar o método HTTP mais apropriado para cada funcionalidade


## **Relacionamento de tabelas:**

- Produto deve ter 1 Marca e 5 Atributos.
- Seguir a configuração inicial do projeto e criar migrations conforme a 
necessidade para que quando for gerado o comando de docker compose up para 
subir os container e suba elas para um banco de teste.

## Docker
Para facilitar criação/remoção de bancos de dados pra teste, um banco normal pode ser usado também.
Os comandos devem ser executados na mesma pasta do arquivo docker-compose.yaml
 - ```docker-compose up -d``` baixa imagens (se necessário), cria containers e os coloca em execução
 - ```docker-compose down``` para e apaga os containers criados
 - ```docker-compose start``` coloca os containers já criados em execução
 - ```docker-compose stop``` para os containers

## EntityFramework
ORM da dotnet, permite uma abordagem **code-first** de lidar com o banco de dados. 
- ```dotnet-ef migrations add <NomeDaMigração>``` cria uma nova migração com as últimas alterações (se não houver nenhuma cria uma vazia)
- ```dotnet-ef database update``` aplica as migrações novas ao banco de dados do projeto usando string de conexão em appsettings.json
### Funcionamento da Aplicação

[Adicione aqui uma descrição do funcionamento da aplicação, explicando como as funcionalidades são implementadas e como o usuário pode interagir com elas.]

## Autor

[Seu nome ou informações de contato]
