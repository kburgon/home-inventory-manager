package main

import (
	"fmt"
	"net/http"

	"github.com/gin-gonic/gin"
)

type Product struct {
	Id					int		`json:"id"`
	ProductName 		string	`json:"productName"`
	Count 				int		`json:"count"`
	WarningThreshold 	int		`json:"warningThreshold"`
}

type StockAdjustment struct {
	ProductId		int	`json:"productId"`
	StockAdjustment	int	`json:"stockAdjustment"`
}

var sampleProducts = []Product{
	{ Id: 1, ProductName: "Bread", Count: 4, WarningThreshold: 1 },
	{ Id: 2, ProductName: "Eggs", Count: 2, WarningThreshold: 1 },
	{ Id: 3, ProductName: "Milk", Count: 0, WarningThreshold: 1 },
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
			break;
		}
	}

	c.Status(204)
}

func main() {
	router := gin.Default()
	router.Use(handleCORS())
	router.GET("/api/products/all", getProducts)
	router.POST("/api/adjustStock", adjustStock)

	router.Run("localhost:5223")
}
