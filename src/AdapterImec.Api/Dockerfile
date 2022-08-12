#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AdapterImec.Api/AdapterImec.Api.csproj", "AdapterImec.Api/"]
COPY ["AdapterImec.Domain/AdapterImec.Domain.csproj", "AdapterImec.Domain/"]
COPY ["AdapterImec.Repository/AdapterImec.Repository.csproj", "AdapterImec.Repository/"]
COPY ["AdapterImec.Application/AdapterImec.Application.csproj", "AdapterImec.Application/"]
RUN dotnet restore "AdapterImec.Api/AdapterImec.Api.csproj"
COPY . .
WORKDIR "/src/AdapterImec.Api"
RUN dotnet build "AdapterImec.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdapterImec.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdapterImec.Api.dll"]
