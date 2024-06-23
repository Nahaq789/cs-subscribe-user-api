FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["User.API/User.API.csproj", "User.API/"]
COPY ["User.Domain/User.Domain.csproj", "User.Domain/"]
COPY ["User.Infrastructure/User.Infrastructure.csproj", "User.Infrastructure/"]
RUN dotnet restore "User.API/User.API.csproj"
COPY . .
WORKDIR "/src/User.API"
RUN dotnet build "User.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.API.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# COPY User.API/run.sh /app/run.sh
# RUN chmod +x /app/run.sh

ENTRYPOINT ["dotnet", "User.API.dll"]