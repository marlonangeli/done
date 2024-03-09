FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Done.Api/Done.Api.csproj", "Done.Api/"]
RUN dotnet restore "Done.Api/Done.Api.csproj"
COPY . .
WORKDIR "/src/Done.Api"
RUN dotnet build "Done.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Done.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Done.Api.dll"]
