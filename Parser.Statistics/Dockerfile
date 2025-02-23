FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Parser.Statistics/Parser.Statistics.csproj", "Parser.Statistics/"]
COPY ["DataBase.Statistics/DataBase.Statistics.csproj", "DataBase.Statistics/"]
COPY ["Domain.Statistics/Domain.Statistics.csproj", "Domain.Statistics/"]
RUN dotnet restore "Parser.Statistics/Parser.Statistics.csproj"
COPY . .
WORKDIR "/src/Parser.Statistics"
RUN dotnet build "Parser.Statistics.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Parser.Statistics.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Parser.Statistics.dll"]
