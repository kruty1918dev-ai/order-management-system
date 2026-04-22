FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy only the project file first to leverage Docker layer caching
COPY ["src/OrderManagementSystem.Api/OrderManagementSystem.Api.csproj", "src/OrderManagementSystem.Api/"]
RUN dotnet restore "src/OrderManagementSystem.Api/OrderManagementSystem.Api.csproj"

# Copy the rest of the sources and publish
COPY . .
# Publish explicitly referencing the project file path to avoid working-directory issues
WORKDIR /src
RUN dotnet publish "src/OrderManagementSystem.Api/OrderManagementSystem.Api.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["sh", "-c", "dotnet OrderManagementSystem.Api.dll --urls=http://0.0.0.0:${PORT:-80}"]
