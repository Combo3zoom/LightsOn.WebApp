FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview-jammy AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview-jammy AS build
WORKDIR /src
COPY ["LightsOn.BlazorApp/LightsOn.BlazorApp.csproj", "LightsOn.BlazorApp/"]
RUN dotnet restore "LightsOn.BlazorApp/LightsOn.BlazorApp.csproj"
COPY . .
WORKDIR "/src/LightsOn.BlazorApp"
RUN dotnet build "LightsOn.BlazorApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LightsOn.BlazorApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LightsOn.BlazorApp.dll"]
