#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY BusinessObjectLayer/*.csproj ./BusinessObjectLayer/
COPY BusinessObjectLayer/*.cs ./BusinessObjectLayer/
COPY BusinessObjectLayer/Properties/*.cs ./BusinessObjectLayer/Properties/
COPY DataAccessLayer/*.csproj ./DataAccessLayer/
COPY DataAccessLayer/*.config ./DataAccessLayer/
COPY DataAccessLayer/*.cs ./DataAccessLayer/
COPY DataAccessLayer/Properties/*.cs ./DataAccessLayer/Properties/
COPY BusinessLayer/*.csproj ./BusinessLayer/
COPY BusinessLayer/*.config ./BusinessLayer/
COPY BusinessLayer/*.cs ./BusinessLayer/
COPY BusinessLayer/Properties/*.cs ./BusinessLayer/Properties/
COPY TechnicalExercise/*.csproj ./TechnicalExercise/
COPY TechnicalExercise/*.config ./TechnicalExercise/
RUN nuget restore

# copy everything else and build app
COPY TechnicalExercise/. ./TechnicalExercise/
WORKDIR /app/TechnicalExercise
RUN msbuild /p:Configuration=Release -r:False


FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8 AS runtime
WORKDIR /inetpub/wwwroot
COPY --from=build /app/TechnicalExercise/. ./
RUN icacls './db' /grant 'IIS_IUSRS:(OI)(CI)F'