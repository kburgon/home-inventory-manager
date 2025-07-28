package main

import (
	"database/sql"
	"fmt"

	"github.com/gin-gonic/gin"
	_ "github.com/glebarez/go-sqlite"
)

func initDb() error {
	db, err := sql.Open("sqlite", "./inventory.db")
	if (err != nil) {
		fmt.Println("Error opening db")
		return err
	}


	productTable := `CREATE TABLE IF NOT EXISTS products (
		id INTEGER PRIMARY KEY,
		productName STRING NOT NULL,
		count INTEGER NOT NULL,
		warningThreshold INTEGER NOT NULL
	);`

	fmt.Println("Creating product table")
	_, err = db.Exec(productTable)
	if (err != nil) {
		fmt.Printf("Error creating product table: %s\n", err)
		return err
	}

	defer db.Close()
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
	router.GET("/api/products", getProducts)
	router.POST("/api/products", createProduct)
	router.PUT("/api/products", updateProduct)
	router.GET("/api/db/version", getSqliteVersion)

	router.Run("localhost:5223")
}
