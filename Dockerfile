#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR .
COPY ./ginger-garlic.sln ./
COPY ["src/gg-webapi/gg-webapi.csproj", "gg-webapi/"]
RUN dotnet restore "gg-webapi/gg-webapi.csproj"
COPY . .
WORKDIR "/src/gg-webapi"
RUN dotnet build "gg-webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "gg-webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "gg-webapi.dll"]