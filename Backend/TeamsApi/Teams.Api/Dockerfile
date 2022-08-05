FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TeamsApi/TeamsApi.csproj", "TeamsApi/"]
RUN dotnet restore "TeamsApi/TeamsApi.csproj"
COPY . .
WORKDIR "/src/TeamsApi"
RUN dotnet build "TeamsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TeamsApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeamsApi.dll"]
