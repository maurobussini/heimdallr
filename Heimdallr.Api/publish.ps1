dotnet publish
docker build -f ./.docker/development.dockerfile -t heimdallr-api .
docker run --rm -p 1234:80 heimdallr-api