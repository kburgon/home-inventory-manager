package main

import (
	"database/sql"
	"fmt"
	"net/http"

	"github.com/gin-gonic/gin"
	_ "github.com/glebarez/go-sqlite"
)

var sampleProducts = []Product{
	{ Id: 1, ProductName: "Bread", Count: 4, WarningThreshold: 1 },
	{ Id: 2, ProductName: "Eggs", Count: 2, WarningThreshold: 1 },
	{ Id: 3, ProductName: "Milk", Count: 0, WarningThreshold: 1 },
}

func getProducts(c *gin.Context) {
	c.JSON(http.StatusOK, sampleProducts)
}

func adjustStock(c *gin.Context) {
	var adjustment StockAdjustment
	if err := c.BindJSON(&adjustment); err != nil {
		fmt.Println("adjustStock: Failed to parse request body")
		c.AbortWithError(500, err)
	}

	for _, product := range sampleProducts {
		if product.Id == adjustment.ProductId {
			product.Count = product.Count + adjustment.StockAdjustment
			fmt.Printf("ProductID: %d, Adjustment: %d, NewTotal: %d", product.Id, adjustment.StockAdjustment, product.Count)
			break;
		}
	}

	c.Status(204)
}

func getSqliteVersion(c *gin.Context) {
	db, err := sql.Open("sqlite", "./inventory.db")
	if err != nil {
		fmt.Println("getSqliteVersion: Error connecting to database")
		fmt.Println(err)
		c.AbortWithError(500, err)
	}

	defer db.Close()
	fmt.Println("Connected to sqlite db successfully")

	var sqliteVersion string
	err = db.QueryRow("select sqlite_version()").Scan(&sqliteVersion)
	if err != nil {
		fmt.Println("getSqliteVersion: Error capturing sqlite version")
		fmt.Println(err)
		c.AbortWithError(500, err)
	}

	c.Data(200, "application/text", []byte(sqliteVersion))
}
