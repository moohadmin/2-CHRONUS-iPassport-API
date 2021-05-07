#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim
RUN ln -s /lib/x86_64-linux-gnu/libdl.so.2 /lib/x86_64-linux-gnu/libdl.so
RUN apt update
RUN apt install -y libgdiplus
RUN apt install -y libc6-dev
RUN ln -s /usr/lib/libgdiplus.so /lib/x86_64-linux-gnu/libgdiplus.so
EXPOSE 80
COPY /artifacts app/
WORKDIR /app
ENTRYPOINT ["dotnet", "iPassport.Api.dll"]