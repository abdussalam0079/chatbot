# ─────────────────────────────────────────────────────────────────────────────
# Dockerfile — AI Chatbot (.NET 6 Windows Forms)
# NOTE: Windows Forms requires a Windows-based container.
#       Use this on Windows Docker Desktop with Windows containers enabled.
# ─────────────────────────────────────────────────────────────────────────────

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY AIChatbot.csproj ./
RUN dotnet restore

# Copy all source files and build in Release mode
COPY . .
RUN dotnet build -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish --no-restore

# Stage 3: Runtime image
FROM mcr.microsoft.com/dotnet/runtime:6.0 AS final
WORKDIR /app

# Copy published output from publish stage
COPY --from=publish /app/publish .

# Expose no ports (desktop app), but document the entry point
ENTRYPOINT ["dotnet", "AIChatbot.dll"]
