psql -c "CREATE USER ${DB_APPUSERNAME} WITH PASSWORD '${DB_APPUSERPASSWORD}';" -U postgres
psql -c "CREATE DATABASE HomeInventory;" -U postgres
psql -c "GRANT CONNECT ON DATABASE homeinventory TO ${DB_APPUSERNAME};" -U postgres

# Create tables in homeinventory
psql -c "CREATE TABLE TestTable (TestTableID SERIAL, TestTableValue TEXT NOT NULL);" -U postgres -d homeinventory

# Grant permissions to all tables in homeinventory
psql -c "GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO ${DB_APPUSERNAME};" -U postgres -d homeinventory
psql -c "GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO ${DB_APPUSERNAME};" -U postgres -d homeinventory
psql -c "GRANT EXECUTE ON ALL FUNCTIONS IN SCHEMA public TO ${DB_APPUSERNAME}" -U postgres -d homeinventory
