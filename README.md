# Finstar test task

### Настройка:
- В файле `appsettings.json` в строке **"DefaultConnection"** указать настройки для подключения к БД Postgre
- Выполните команду обновления БД из миграций:
```
dotnet ef database update --project "FINSTAR Test Task.csproj" --startup-project "FINSTAR Test Task.csproj" --context FINSTAR_Test_Task.Infrastructure.Context.AppDbContext --configuration Debug 20221025174006_Initial
```
