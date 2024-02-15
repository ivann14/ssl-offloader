FROM mcr.microsoft.com/dotnet/sdk:8.0

ARG PORT=443

WORKDIR /App

# Copy everything
COPY . ./
# Copy the entrypoint script
COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

ENV ASPNETCORE_HTTPS_PORTS=$PORT
EXPOSE ${PORT}

ENTRYPOINT ["/entrypoint.sh"]