FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["OrderUp.API/OrderUp.API.csproj", "OrderUp.API/"]
RUN dotnet restore "OrderUp.API/OrderUp.API.csproj"
COPY . .
WORKDIR "/src/OrderUp.API"
RUN dotnet build "OrderUp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderUp.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderUp.API.dll"]
