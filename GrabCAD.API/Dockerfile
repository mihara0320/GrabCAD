FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY GrabCAD.API/GrabCAD.API.csproj GrabCAD.API/
RUN dotnet restore GrabCAD.API/GrabCAD.API.csproj
COPY . .
WORKDIR /src/GrabCAD.API
RUN dotnet build GrabCAD.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish GrabCAD.API.csproj -c Release -o /app

FROM build AS testrunner
WORKDIR /src/GrabCAD.UnitTests
RUN dotnet test GrabCAD.UnitTests.csproj --logger:trx

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GrabCAD.API.dll"]
