## Deployed application

[shop.mathiaskivi.ee](https://shop.mathiaskivi.ee)

It goes to "sleep" after idling for 30 minutes. It takes around 15 seconds to wake up.

## Useful commands
Install `dotnet ef` command:<br />
`dotnet tool install --global dotnet-ef`
<br /><br />
Drop database:<br />
`dotnet ef database drop --project WebApp.DAL --startup-project WebApp`
<br /><br />
Remove migrations:<br />
`dotnet ef migrations remove --project WebApp.DAL --startup-project WebApp`
<br /><br />
Add migration:<br />
`dotnet ef migrations add Initial --project WebApp.DAL --startup-project WebApp`
<br /><br />
Create database:<br />
`dotnet ef database update --project WebApp.DAL --startup-project WebApp`
