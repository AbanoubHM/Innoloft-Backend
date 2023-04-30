#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Innoloft-Backend/Innoloft-Backend.csproj", "Innoloft-Backend/"]
RUN dotnet restore "Innoloft-Backend/Innoloft-Backend.csproj"
COPY . .
WORKDIR "/src/Innoloft-Backend"
RUN dotnet build "Innoloft-Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Innoloft-Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY Innoloft-Backend/Innoloft.db .
ENTRYPOINT ["dotnet", "Innoloft-Backend.dll"]