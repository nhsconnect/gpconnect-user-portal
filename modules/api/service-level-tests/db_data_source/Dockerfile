FROM ubuntu:22.04

RUN apt-get update && apt-get install -y postgresql-client

WORKDIR /data
COPY . .

ENTRYPOINT [ "bash", "do-the-data.sh" ]

