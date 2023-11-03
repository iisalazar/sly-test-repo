# Sly test repository
An example monorepo that runs .NET on the backend and NextJS on the front-end

## How to run
You need to have `docker compose` installed in your system. 

## Managing external dependencies
This project has only 1 external dependency, which is a SQL server. But it can be extended by modifiny the [docker-compose.dev.yaml](./docker-compose.dev.yaml) and adding stuff there, like a Redis instance or Seq.

## Run external dependencies
1. run `docker compose -f docker-compose.dev.yaml up -d` to run the containers in detached mode
2. Optionally, you can also run the script `run-dev-environment.sh` if you're on a Linux machine. Just open up the terminal and run `./run-dev-environment.sh`

