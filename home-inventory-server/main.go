package main

import (
	"net/http"
	"github.com/gin-gonic/gin"
)

type Product struct {
	ProductName 		string	`json:"productName"`
	Count 				int		`json:"count"`
	WarningThreshold 	int		`json:"warningThreshold"`
}

var sampleProducts = []Product{
	{ ProductName: "Bread", Count: 4, WarningThreshold: 1 },
	{ ProductName: "Eggs", Count: 2, WarningThreshold: 1 },
	{ ProductName: "Milk", Count: 0, WarningThreshold: 1 },
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

func main() {
	router := gin.Default()
	router.Use(handleCORS())
	router.GET("/api/products/all", getProducts)

	router.Run("localhost:5223")
}
