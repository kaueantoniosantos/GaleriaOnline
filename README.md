# GaleriaOnline

Este projeto é uma API para gerenciar uma galeria de imagens online. Ele permite aos usuários realizar o upload, visualização, edição e exclusão de imagens, tudo de forma eficiente através de uma API RESTful. A interface de interação principal com a API é feita através do Swagger.

O projeto foi desenvolvido com foco em oferecer uma solução robusta e escalável para o gerenciamento de imagens, com uma arquitetura que suporta a integração com um frontend moderno.

## Funcionalidades da API

A API oferece os seguintes endpoints, disponíveis via Swagger:

* **Upload de Imagem:** Permite enviar novas imagens para a galeria.
* **Listar Imagens:** Retorna uma lista de todas as imagens disponíveis.
* **Buscar Imagem por ID:** Recupera uma imagem específica utilizando seu ID.
* **Atualizar Imagem:** Modifica os dados de uma imagem existente.
* **Deletar Imagem:** Remove uma imagem da galeria.

## Tecnologias Utilizadas

* **Backend:** ASP.NET Core
* **Banco de Dados:** Microsoft SQL Server
* **ORM:** Entity Framework (Normal, Tools, SQL Server)
* **Documentação da API:** Swagger
* **IDE:** Visual Studio Community 2022

## Futuras Melhorias

* **Frontend:** Integração com um frontend desenvolvido em React para uma interface de usuário completa.
* **Script de Banco de Dados:** Geração e inclusão do script SQL para a criação do banco de dados, facilitando a configuração inicial para novos desenvolvedores.

## Como Rodar o Projeto Localmente

Siga estes passos para configurar e executar o projeto em seu ambiente local:

1.  **Pré-requisitos:** Certifique-se de ter o [Visual Studio Community 2022](https://visualstudio.microsoft.com/pt-br/vs/community/) e o [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/pt-br/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16) instalados.

2.  **Clone o Repositório:**
    ```bash
    git clone https://github.com/kaueantoniosantos/GaleriaOnline.git
    cd GaleriaOnline
    ```

3.  **Configurar a String de Conexão:**
    * Abra o projeto no Visual Studio.
    * No arquivo `appsettings.json`, atualize a string de conexão com os dados do seu banco de dados SQL Server local.

4.  **Executar o Projeto:**
    * Dentro do Visual Studio, pressione `F5` ou clique no botão `Play`.
    * O projeto será compilado e o Swagger será aberto automaticamente no seu navegador padrão.

## Uso da API com Swagger

Ao executar o projeto, a interface do Swagger será carregada, permitindo que você explore e teste cada endpoint da API de forma interativa. Você pode inserir parâmetros, enviar solicitações e ver as respostas diretamente no navegador, o que facilita o desenvolvimento e a depuração.

## Contribuição

Contribuições são bem-vindas! Se você deseja colaborar, por favor, crie um *fork* do repositório, faça suas alterações e envie um *pull request*.
