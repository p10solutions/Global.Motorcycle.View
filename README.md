# Global.Motorcycle.View

Possui uma API REST em .NET 8 que disponibiliza um endpoint para obter os dados integrados das informações de locação das motos além de possuir consumidores que se conectam nos tópicos dos Microserviços de Delivery e Motorcycle para receber os dados que são gerados nestes domínios.

Passo a Passo:

1. Execute o projeto com o seguinte comando <b>dotnet Global.Motorcycle.View.Api.dll</b> ou se preferir acesse a solução pelo <b>Visual Studio</b>.

2. Acesse o endpoint da documentação da api em seu navegador https://localhost:7086/swagger/index.html

3.Execute o seguinte docker-compose para criação do banco de dados e broker kafka: https://github.com/p10solutions/Global.MotoDelivery.Docs/blob/main/docker-compose.yml 
