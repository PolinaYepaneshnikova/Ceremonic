#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CeremonicBackend/CeremonicBackend.csproj", "CeremonicBackend/"]
RUN dotnet restore "CeremonicBackend/CeremonicBackend.csproj"
COPY . .
WORKDIR "/src/CeremonicBackend"
RUN dotnet build "CeremonicBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CeremonicBackend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CeremonicBackend.dll"]