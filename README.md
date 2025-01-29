# 🏆 Gerenciador de Campeonatos

Aplicação que permite gerenciar um campeonato de futebol ⚽, desde o cadastro e manutenção de times, partidas e jogadores, até pesquisas paginadas para obtenção de relatórios 📊.

## 🚀 GitFlow

Para a organização do projeto no Git, utilizei o seguinte fluxo de trabalho:

![Image](https://github.com/user-attachments/assets/87851680-c427-4137-9aea-7d7c883150ef)

🔹 Apenas realizo o **build** e o **deploy** da aplicação na **Azure** dentro da branch `release`.  

## 🗄️ Estrutura de Dados

A estrutura do banco de dados foi planejada da seguinte forma:

![Image](https://github.com/user-attachments/assets/d154151a-9c0e-4f11-83fe-899922add488)

- O banco e as tabelas foram criados utilizando **Code First** do **Entity Framework**, com **migrations** para atualizações e manutenção do banco 🔄.  
- Também deixei um script SQL na pasta `scripts/` do projeto caso seja necessário criar as tabelas manualmente 📜.  

## 🔐 Autenticação e Autorização

A API utiliza **Identity do .NET 8** para autenticação e autorização 🔑.  

### Como obter o access token?  
1. **Registrar-se** na rota `/register`, informando um **e-mail** e uma **senha**.  
2. **Realizar login** na rota `/login` para obter o `accessToken`.  
3. **Utilizar o token** para autenticar as chamadas à API nas rotas protegidas.  

## 📌 Detalhes da Aplicação

✅ **Evitei a exposição direta das entidades** nas requisições e respostas, garantindo maior segurança e personalização dos retornos.  
✅ **Cada controller possui uma rota de pesquisa**, permitindo **paginação, ordenação e filtragem** de dados 🔎.  
✅ **Validações com Data Annotations** garantem que apenas requisições válidas sejam processadas ✔️.  
✅ **Middleware de tratamento de erros**, com logs gerenciados via **Serilog**, para melhor rastreamento de falhas ⚠️📝.  
✅ **Documentação da API** está disponível no **Swagger**:  
🔗 [Swagger API Docs](https://campeonato-api.azurewebsites.net/swagger/index.html)  

## 🌐 Como Acessar a Aplicação?

💻 **Hospedagem na Azure**  
- API disponível em:  
  🔗 [https://campeonato-api.azurewebsites.net/](https://campeonato-api.azurewebsites.net/)  
- Para consumir as rotas protegidas, siga os passos da seção **Autenticação e Autorização**.  

🏠 **Rodando localmente**  
1. Baixe o projeto para sua máquina.  
2. Edite o `AppSettings.json`, alterando a **Connection String** para o seu banco de dados 🛠️.  
3. No **Package Manager Console**, execute:  
   ```sh
   update-database
