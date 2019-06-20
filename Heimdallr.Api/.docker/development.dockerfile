FROM microsoft/aspnetcore:2.0
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-bin/Release/PublishOutput} /app
ENTRYPOINT ["dotnet", "Heimdallr.Api.dll"]
