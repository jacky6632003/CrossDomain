#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["JSONP.SAMPLE/JSONP.SAMPLE.csproj", "JSONP.SAMPLE/"]
RUN dotnet restore "JSONP.SAMPLE/JSONP.SAMPLE.csproj"
COPY . .
WORKDIR "/src/JSONP.SAMPLE"
RUN dotnet build "JSONP.SAMPLE.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JSONP.SAMPLE.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JSONP.SAMPLE.dll"]