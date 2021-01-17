FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["IMS.URM/IMS.URM.csproj", "IMS.URM/"]
RUN dotnet restore "IMS.URM/IMS.URM.csproj"
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_10.x | bash - && \
    apt-get install -y build-essential nodejs
COPY . .
WORKDIR "/src/IMS.URM"
RUN dotnet build "IMS.URM.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "IMS.URM.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IMS.URM.dll"]
