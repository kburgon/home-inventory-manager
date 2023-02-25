# NOTE: The following environment variables need to be set to successfully build the container:
# - POSTGRES_USER
# - POSTGRES_PASSWORD
# - DB_APPUSERNAME
# - DB_APPUSERPASSWORD

FROM postgres:15.1
COPY ./init/ /docker-entrypoint-initdb.d/
RUN ls /docker-entrypoint-initdb.d/
EXPOSE 5432
