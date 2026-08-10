
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#sdk dotnet cli waghera  ki IMAGE USE KRTA RAHA HY

WORKDIR /src
#aik repo banai container may src kay name

COPY ProductService.csproj ./
#src may ProductService.csproj ko copy kia 
RUN dotnet restore
# dotnet resotre say required dependencies install ki

copy . ./
# ab sara code copy kia src may 
RUN dotnet publish -c Release -o /app/publish
# ab code comile kia our uskay baad /app/publics may bhej dia stage 1 ka kaam hogay 


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
#final stage may sirf

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT [ "dotnet","ProductService.dll" ]
