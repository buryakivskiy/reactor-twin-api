# Reactor Twin API

Use the commands below to store the DB connection string in user-secrets (run in project root):

```bash
dotnet user-secrets init --project ReactorTwinAPI.csproj
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=reactortwin_db;Username=postgres;Password=postgres" --project ReactorTwinAPI.csproj
```

