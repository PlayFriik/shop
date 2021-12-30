## Useful commands

Install `dotnet ef`:
`dotnet tool install --global dotnet-ef`

Drop database:
`dotnet ef database drop --project WebApp.DAL --startup-project WebApp`

Remove migrations:
`dotnet ef migrations remove --project WebApp.DAL --startup-project WebApp`

Add migration:
`dotnet ef migrations add Initial --project WebApp.DAL --startup-project WebApp`

Create database:
`dotnet ef database update --project WebApp.DAL --startup-project WebApp`