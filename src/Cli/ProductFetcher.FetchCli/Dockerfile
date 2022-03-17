FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet tool restore
WORKDIR /src/src/Cli/ProductFetcher.FetchCli
RUN dotnet paket restore
RUN dotnet restore -nowarn:msb3202,nu1503
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base as final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "ProductFetcher.FetchCli.dll"]