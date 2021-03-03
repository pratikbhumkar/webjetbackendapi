# Get base SDK Image from Microsoft
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy the CSPROJ file and restore any dependencies (via NUGET)
COPY ./webjetbackendapi/*.csproj ./
RUN dotnet restore

#Copy the project files and build our Releases
COPY . ./
RUN dotnet publish webjetbackendapi.sln -c Release -o out

#Generate runtime image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
ENV TOKEN=${{ secrets.TOKEN }}
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "webjetbackendapi.dll"]