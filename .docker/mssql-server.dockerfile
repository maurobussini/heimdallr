FROM microsoft/mssql-server-linux:2017-latest

RUN mkdir -p /opt/scripts
WORKDIR /opt/scripts

COPY ./Heimdallr.EntityFramework/Scripts /opt/scripts

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=P@ssw0rd

RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 10 \
    &&  /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'P@ssw0rd' -i /opt/scripts/Create.sql \
	&&  /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'P@ssw0rd' -i /opt/scripts/InitialMigration.sql \
    && pkill sqlservr