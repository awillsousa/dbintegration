###########
# BUILDER #
###########

# pull official base image
FROM ubuntu:18.04

# set work directory
WORKDIR /code
RUN mkdir -p /code

# set environment variables
ENV DEBIAN_FRONTEND noninteractive
ENV ACCEPT_EULA Y

# install system dependencies
RUN apt-get update && apt-get install -y --no-install-recommends apt-utils

# Install SDK
RUN apt-get install -y wget
RUN wget https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb
RUN rm packages-microsoft-prod.deb
RUN apt-get update
RUN apt-get install -y apt-transport-https
RUN apt-get update && apt-get install -y dotnet-sdk-5.0

# Install Runtime
RUN apt-get install -y aspnetcore-runtime-5.0

# Install Nodejs
#RUN apt-get install -y nodejs
#RUN apt-get install -y npm
RUN apt-get install -y curl dirmngr apt-transport-https lsb-release ca-certificates
RUN curl -sL https://deb.nodesource.com/setup_12.x | bash -
RUN apt-get update
RUN apt-get install -y gcc g++ make
RUN apt-get install -y nodejs

# Database access
RUN dotnet tool install --global dotnet-ef
RUN dotnet add package dotenv.net --version 3.1.0
RUN dotnet add package FSharp.Core --version=5.0.1
RUN dotnet add package Microsoft.EntityFrameworkCore --version 5.0.9
RUN dotnet add package Microsoft.EntityFrameworkCore.Design --version 5.0.9
RUN dotnet add package Microsoft.EntityFrameworkCore.Tools --version 5.0.3
RUN dotnet add package Npgsql --version 5.0.7
RUN dotnet add package SQLProvider --version 1.2.8
RUN dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 5.0.0
RUN dotnet add package EntityFrameworkCore.FSharp --version 5.0.3-beta006
RUN dotnet add package Fsharp.Data --version 4.2.2
RUN dotnet add package Npgsql.FSharp --version 4.0.0
RUN dotnet add package System.Data.SqlClient --version 4.8.2


# Copia do Código
COPY . /code/

# run entrypoint.sh
ENTRYPOINT ["dotnet","run"]

