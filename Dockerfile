#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Calendar.Api/Calendar.WebApi.csproj", "Calendar.Api/"]
COPY ["Calendar.Business/Calendar.Business.csproj", "Calendar.Business/"]
COPY ["Calendar.DataAccess/Calendar.DataAccess.csproj", "Calendar.DataAccess/"]
COPY ["Calendar.Domain/Calendar.Domain.csproj", "Calendar.Domain/"]
COPY ["Calendar.Core/Calendar.Core.csproj", "Calendar.Core/"]
RUN dotnet restore "Calendar.Api/Calendar.WebApi.csproj"
COPY . .
WORKDIR "/src/Calendar.Api"
RUN dotnet build "Calendar.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Calendar.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Calendar.WebApi.dll"]
