#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Reservation.Documents/Reservation.Documents.csproj", "Reservation.Documents/"]
RUN dotnet restore "Reservation.Documents/Reservation.Documents.csproj"
COPY . .
WORKDIR "/src/Reservation.Documents"
RUN dotnet build "Reservation.Documents.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Reservation.Documents.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Reservation.Documents.dll"]