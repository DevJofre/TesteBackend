# [PRAZO DE ENTREGA - 7 DIAS CORRIDOS]

# Teste de Desenvolvimento Backend

## Inicialização

- Faça um fork do repositório do projeto.
- Clone o repositório forkado para o seu ambiente local.
- Quando finalizado, envie o link do repositório para [dev@acheipneus.com.br](mailto:dev@acheipneus.com.br "‌").
- A utilização dos templates é opcional

## Requisitos de Desenvolvimento

- **Linguagem utilizada:** C# dotnet 8
- **Sem Warnings ou Erros**: O projeto deve compilar sem erros e com o mínimo de _warnings_
- **Versionamento**: Utilizar _Git_ para versionamento do código, tentar manter commits organizados
- **Geração de log:** gerar logs permanentes de alguma ação normal e de algum erro (de exemplo, não precisa ser de tudo)
- **Api Restful:** Utilizar o método HTTP mais apropriado para cada função
- **CRUD:** Não precisa ser completo, apenas funções que achar mais releveantes
- **Remoção:** Ao apagar uma categoria, os produtos relacionados não devem ser apagados, apenas devem 
ser atualizados pra ficar sem categoria, ao apagar um produto os atributos relacionados 
devem ser apagados (opção OnDelete) porém as categorias não devem ser apagadas.


## **Relacionamento de tabelas:**

- Produto deve ter 1 Categoria e alguns Atributos (com nome e valor).
- Seguir a configuração inicial do projeto e criar migrations conforme a 
necessidade para ter a informação do banco salva e facilitar revisão do teste

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

## Autor

[Seu nome ou informações de contato]
