psql -c "CREATE USER ${DB_APPUSERNAME} WITH PASSWORD '${DB_APPUSERPASSWORD}';" -U postgres
psql -c "CREATE DATABASE HomeInventory;" -U postgres
psql -c "GRANT CONNECT ON DATABASE homeinventory TO ${DB_APPUSERNAME};" -U postgres

# Create tables in homeinventory
psql -c "CREATE TABLE Products (
	ProductId INT GENERATED ALWAYS AS IDENTITY, 
	ProductName VARCHAR(100) NOT NULL, 
	PRIMARY KEY(ProductId)
);" -U postgres -d homeinventory
psql -c "CREATE TABLE ProductItems (
	ProductItemId INT GENERATED ALWAYS AS IDENTITY, 
	ProductId INT, 
	ItemBarcodeNumber VARCHAR(100), 
	PRIMARY KEY(ProductItemId), 
	CONSTRAINT fk_products FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
	UNIQUE(ProductId, ItemBarcodeNumber)
);" -U postgres -d homeinventory
psql -c "CREATE TABLE InventoryTransactions (
	InventoryTransactionId INT GENERATED ALWAYS AS IDENTITY, 
	ProductItemId INT, 
	InventoryAdjustment INT, 
	AdjustedAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY(InventoryTransactionId),
	CONSTRAINT fk_product_item FOREIGN KEY (ProductItemId) REFERENCES ProductItems(ProductItemId)
);" -U postgres -d homeinventory

# Grant permissions to all tables in homeinventory
psql -c "GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO ${DB_APPUSERNAME};" -U postgres -d homeinventory
psql -c "GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO ${DB_APPUSERNAME};" -U postgres -d homeinventory
psql -c "GRANT EXECUTE ON ALL FUNCTIONS IN SCHEMA public TO ${DB_APPUSERNAME}" -U postgres -d homeinventory

