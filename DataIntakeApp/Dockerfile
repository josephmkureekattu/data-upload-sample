#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DataIntakeApp/DataIntakeApp.csproj", "DataIntakeApp/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["SharedKernal/SharedKernal.csproj", "SharedKernal/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "DataIntakeApp/DataIntakeApp.csproj"
COPY . .
WORKDIR "/src/DataIntakeApp"
RUN dotnet build "DataIntakeApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataIntakeApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataIntakeApp.dll"]