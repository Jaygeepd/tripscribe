FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
ARG NETENV=Release
WORKDIR /src
COPY ./ ./
RUN dotnet restore src/tripscribe.Api/tripscribe.Api.csproj
RUN dotnet publish src/tripscribe.Api/tripscribe.Api.csproj -c $NETENV -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./

ENV ASPNETCORE_URLS=http://*:5000
EXPOSE 5000

ARG API_DB_URL
ENV DbConnectionString=$API_DB_URL

ENTRYPOINT ["dotnet", "tripscribe.Api.dll"]