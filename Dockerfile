FROM mcr.microsoft.com/dotnet/sdk:6.0 as sdk
WORKDIR /Source

COPY *.sln .

# Copy over the .csproj files
# Base projects
COPY Base.BLL/*.csproj ./Base.BLL/
COPY Base.DAL/*.csproj ./Base.DAL/
COPY Base.Domain/*.csproj ./Base.Domain/
COPY Base.Extensions/*.csproj ./Base.Extensions/
COPY Base.Resources/*.csproj ./Base.Resources/

# WebApp projects
COPY WebApp/*.csproj ./WebApp/
COPY WebApp.API/*.csproj ./WebApp.API/
COPY WebApp.BLL/*.csproj ./WebApp.BLL/
COPY WebApp.DAL/*.csproj ./WebApp.DAL/
COPY WebApp.Domain/*.csproj ./WebApp.Domain/
COPY WebApp.Resources/*.csproj ./WebApp.Resources/

# Restore all the NuGet packages
RUN dotnet restore

# Copy over the source code
# Base projects
COPY Base.BLL/. ./Base.BLL/
COPY Base.DAL/. ./Base.DAL/
COPY Base.Domain/. ./Base.Domain/
COPY Base.Extensions/. ./Base.Extensions/
COPY Base.Resources/. ./Base.Resources/

# WebApp projects
COPY WebApp/. ./WebApp/
COPY WebApp.API/. ./WebApp.API/
COPY WebApp.BLL/. ./WebApp.BLL/
COPY WebApp.DAL/. ./WebApp.DAL/
COPY WebApp.Domain/. ./WebApp.Domain/
COPY WebApp.Resources/. ./WebApp.Resources/

WORKDIR /Source/WebApp

RUN dotnet publish --configuration Release --output out

# Create a new image, from aspnet runtime (no compilers)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /App

COPY --from=sdk /Source/WebApp/out ./

# Start the application

# Azure
# ENTRYPOINT ["dotnet", "WebApp.dll"]

# Heroku
CMD ASPNETCORE_URLS=http://*:$PORT dotnet WebApp.dll
