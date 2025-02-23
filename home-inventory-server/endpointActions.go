package main

import (
	"fmt"
	"net/http"

	"github.com/gin-gonic/gin"
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


