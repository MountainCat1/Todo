FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
 
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
EXPOSE 443
ENV ASPNETCORE_URLS="https://*:443;https://*:5000;http://*:5001"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TodoApi/TodoApi.csproj", "TodoApi/"]
RUN dotnet restore "TodoApi/TodoApi.csproj"
COPY . .
WORKDIR "/src/TodoApi"
RUN dotnet build "TodoApi.csproj" -c Release -o /app/build
RUN dotnet dev-certs https

FROM build AS publish
RUN dotnet publish "TodoApi.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/

ENTRYPOINT ["dotnet", "TodoApi.dll"]
