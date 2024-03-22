# writing docker file for .net core application with postgresql
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY . ./

WORKDIR /app/ExpenSpend.Web
RUN dotnet restore

RUN dotnet publish ExpenSpend.Web.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build-env /app/ExpenSpend.Web/out .
ENTRYPOINT ["dotnet", "ExpenSpend.Web.dll"]