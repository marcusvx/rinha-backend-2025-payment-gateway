# Rinha de Backend 2025 - Gateway de Pagamento

Este é um projeto de exemplo de um gateway de pagamento desenvolvido em .NET para a [Rinha de Backend 2025](https://github.com/zanfranceschi/rinha-de-backend-2025).

## Como executar

### Usando Docker

Para iniciar a aplicação com o Docker, execute o seguinte comando:

```bash
docker-compose up -d
```

### Localmente

Para executar a aplicação localmente, você precisa ter o SDK do .NET 8 instalado.

```bash
dotnet run --project PaymentMediator/PaymentMediator.csproj
```

A API estará disponível em `http://localhost:5000`.
