# S4ETech_API

O projeto consiste em uma API que gerencia o cadastro de 'Associados' e 'Empresas', podendo ser vinculandos uns aos outros.

# Script de criação para o contêiner do Docker:
  - docker pull mcr.microsoft.com/mssql/server:2022-latest
  - docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=P@ssw0rd" ` -p 1433:1433 --name s4etech --hostname s4etech ` -d ` mcr.microsoft.com/mssql/server:2022-latest
  
# Conecte-se ao SQL Server (SSMS 19):
  - Nome do Servidor: localhost, 1433
  - Autenticação = Autenticação do SQL Server;
  - Logon: SA
  - Password: P@ssw0rd

# Observação:
  - É necessário se conectar ao banco de dados primeiro antes de startar o projeto, pois assim que rodar, ele iniciará automaticamente o Migrations que foi implementado.
