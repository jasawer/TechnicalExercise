# TechnicalExercise



## Deploy
- Locate on solution folder
- Run `docker build --pull -t <docker image name> .` in the CLI to build a docker image
- Make sure you have set Docker to run Windows containers
- Run `docker run --rm -it -p 8000:80 <docker image name>` in the CLI to run the docker container

When the container is running, follow the steps below:
  - Open up another command prompt.
  - Check the name of the conatiner by running `docker ps` and look at the CONTAINER ID or NAMES section that maps to your conatiner image
  - Run `docker exec <container name> ipconfig` ipconfig
  - Copy the container IP address and paste into your browser (for example, 172.29.245.43).
