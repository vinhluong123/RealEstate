#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Microservices_Net5.csproj", "."]
RUN dotnet restore "./Microservices_Net5.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Microservices_Net5.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices_Net5.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices_Net5.dll"]