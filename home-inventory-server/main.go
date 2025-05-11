package main

import (
	"database/sql"

	"github.com/gin-gonic/gin"
	_ "github.com/glebarez/go-sqlite"
)

func initDb() error {
	db, err := sql.Open("sqlite", "./inventory.db")
	if (err != nil) {
		return err
	}

	defer db.Close()

	productTable := `CREATE TABLE IF NOT EXISTS products (
		id INTEGER PRIMARY KEY,
		productName STRING NOT NULL,
		count INTEGER NOT NULL,
		warningThreshold INTEGER NOT NULL
	);`

	baseProductTableQuery := `SELECT id FROM products WHERE productName = 'Bread';`

	baseProductTableInsert := `INSERT INTO products (productName, count, warningThreshold) VALUES ('Bread', 1, 1);`

	productInventoryAudit := `CREATE TABLE IF NOT EXISTS productInventoryAudit (
		id INTEGER PRIMARY KEY,
		productId INTEGER NOT NULL,
		adjustmentAmount INTEGER NOT NULL,
		adjustedAt TEXT NOT NULL,
		FOREIGN KEY (productId) REFERENCES products (id)
	);`

	productInventoryAuditQuery := `SELECT * FROM productInventoryAudit WHERE productId = 1;`

	productInventoryAuditInsert := `INSERT INTO productInventoryAudit (productId, adjustmentAmount, datetime()) VALUES (1, 1)`

	_, err = db.Exec(productTable)
	if (err != nil) {
		return err
	}

	_, err = db.Exec(productInventoryAudit)
	if (err != nil) {
		return err
	}

	// Check to see if the default product already exists
	result, err := db.Query(baseProductTableQuery)
	if (err != nil) {
		return err
	}
	
	if (!result.Next()) {
		_, err = db.Exec(baseProductTableInsert)
		if (err != nil) {
			return err
		}
	}

	// Insert a default record into the audit table if a record doesn't already exist.
	result, err = db.Query(productInventoryAuditQuery)
	if (err != nil) {
		return err
	}

	if (!result.Next()) {
		_, err = db.Exec(productInventoryAuditInsert)
		if (err != nil) {
			return err
		}
	}
	return nil
}

func handleCORS() gin.HandlerFunc {
	return func(c *gin.Context) {
		c.Writer.Header().Set("Access-Control-Allow-Origin", "*")
		c.Writer.Header().Set("Access-Control-Allow-Credentials", "true")
		c.Writer.Header().Set("Access-Control-Allow-Headers", "Content-Type, Content-Length, Accept-Encoding, accept, origin, CacheControl, X-Requested-With, X-CSRF-Token")
		c.Writer.Header().Set("Access-Control-Allow-Methods", "POST, OPTIONS, GET, PUT")

		if c.Request.Method == "OPTIONS" {
			c.AbortWithStatus(204)
			return
		}

		c.Next()
	}
}

func main() {
	initDb()
	router := gin.Default()
	router.Use(handleCORS())
	router.GET("/api/products/all", getProducts)
	router.POST("/api/products", createProduct)
	router.POST("/api/adjustStock", adjustStock)
	router.GET("/api/db/version", getSqliteVersion)

	router.Run("localhost:5223")
}
