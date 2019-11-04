#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-nanoserver-1903 AS build
WORKDIR /src
COPY ["Middleware_CoreWeb/Middleware_CoreWeb.csproj", "Middleware_CoreWeb/"]
RUN dotnet restore "Middleware_CoreWeb/Middleware_CoreWeb.csproj"
COPY . .
WORKDIR "/src/Middleware_CoreWeb"
RUN dotnet build "Middleware_CoreWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Middleware_CoreWeb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Middleware_CoreWeb.dll"]