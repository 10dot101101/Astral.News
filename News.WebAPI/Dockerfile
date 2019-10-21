FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["News.WebAPI/News.WebAPI.csproj", "News.WebAPI/"]
RUN dotnet restore "News.WebAPI/News.WebAPI.csproj"
COPY . .
WORKDIR "/src/News.WebAPI"
RUN dotnet build "News.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "News.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "News.WebAPI.dll"]