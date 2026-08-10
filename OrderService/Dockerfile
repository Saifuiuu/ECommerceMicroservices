FROM mcr.microsoft.com/dotnet/sdk:8.0 As Build
WORKDIR /src


COPY OrderService.csproj ./
Run dotnet restore


Copy . ./
Run dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 As final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT [ "dotnet","OrderService.dll" ]
