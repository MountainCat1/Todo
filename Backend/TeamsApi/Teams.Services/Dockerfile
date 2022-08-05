FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Teams.Services/Teams.Services.csproj", "Teams.Services/"]
RUN dotnet restore "Teams.Services/Teams.Services.csproj"
COPY . .
WORKDIR "/src/Teams.Services"
RUN dotnet build "Teams.Services.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Teams.Services.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Teams.Services.dll"]
