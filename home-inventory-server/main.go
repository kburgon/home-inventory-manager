package main

import (
	"fmt"
	"net/http"

	"github.com/gin-gonic/gin"
)

type Product struct {
	productName 		string	`json:"productName"`
	count 				int		`json:"count"`
	warningThreshold 	int		`json:"warningThreshold"`
}

func main() {
	router := gin.Default()
	router.GET("/api/products/all", func(ctx *gin.Context) {
		results := []Product {
			{ productName: "Bread", count: 4, warningThreshold: 1},
			{ productName: "Eggs", count: 2, warningThreshold: 1 },
			{ productName: "Milk", count: 0, warningThreshold: 1 },
		}

		for i, result := range results {
			fmt.Printf("Product %d", i)
			fmt.Printf("\tproductName: %s\n", result.productName)
			fmt.Printf("\tcount: %d\n", result.count)
			fmt.Printf("\twarningThreshold: %d\n", result.warningThreshold)
		}

		if err := ctx.BindJSON(&results); err != nil {
			fmt.Printf("ERROR: %s\n", err)
			ctx.AbortWithError(http.StatusBadRequest, err)
		}

		ctx.IndentedJSON(http.StatusOK, &results)
	})

	router.Run(":5223")
}
