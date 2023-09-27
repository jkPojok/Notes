#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NotesIdentity/NotesIdentity.csproj", "NotesIdentity/"]
RUN dotnet restore "NotesIdentity/NotesIdentity.csproj"
COPY . .
WORKDIR "/src/NotesIdentity"
RUN dotnet build "NotesIdentity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotesIdentity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotesIdentity.dll"]