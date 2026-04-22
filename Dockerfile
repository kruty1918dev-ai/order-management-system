FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Копіюємо тільки csproj спочатку для швидшого кешування шарів
COPY ["src/OrderManagementSystem.Api/OrderManagementSystem.Api.csproj", "src/OrderManagementSystem.Api/"]
RUN dotnet restore "src/OrderManagementSystem.Api/OrderManagementSystem.Api.csproj"

# Копіюємо решту коду і публікуємо
COPY . .
WORKDIR /src/OrderManagementSystem.Api
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["sh", "-c", "dotnet OrderManagementSystem.Api.dll --urls=http://0.0.0.0:${PORT:-80}"]
