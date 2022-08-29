FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TeamMemberships.Api/TeamMemberships.Api.csproj", "TeamMemberships.Api/"]
RUN dotnet restore "TeamMemberships.Api/TeamMemberships.Api.csproj"
COPY . .
WORKDIR "/src/TeamMemberships.Api"
RUN dotnet build "TeamMemberships.Api.csproj" -c Release -o /app/build
RUN dotnet dev-certs https --trust

FROM build AS publish
RUN dotnet publish "TeamMemberships.Api.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/

ENTRYPOINT ["dotnet", "TeamMemberships.Api.dll"]
