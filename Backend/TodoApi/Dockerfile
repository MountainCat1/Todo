FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Todos.Api/Todos.Api.csproj", "TodoApi/"]
RUN dotnet restore "TodoApi/Todos.Api.csproj"
COPY . .
WORKDIR "/src/Todos.Api"
RUN dotnet build "Todos.Api.csproj" -c Release -o /app/build
RUN dotnet dev-certs https --trust

FROM build AS publish
RUN dotnet publish "Todos.Api.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/

ENTRYPOINT ["dotnet", "Todos.Api.dll"]
