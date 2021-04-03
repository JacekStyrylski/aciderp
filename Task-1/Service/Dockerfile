FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build-env

WORKDIR /Aciderp-build

# Copy csproj and restore as distinct layers
COPY **/*.csproj ./

# Find all csprojs and execute dotnet restore on them
RUN for i in *.csproj; do dotnet restore "$i"; done

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out Aciderp.sln

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal
WORKDIR /Aciderp-publish
COPY --from=build-env /Aciderp-build/out .

ENTRYPOINT ["dotnet", "Aciderp.dll"]

EXPOSE 5001
