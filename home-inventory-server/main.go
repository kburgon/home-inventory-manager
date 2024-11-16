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

		if err := ctx.BindJSON(&results); err != nil {
			fmt.Printf("ERROR: %s\n", err)
			ctx.AbortWithError(http.StatusBadRequest, err)
		}

		ctx.IndentedJSON(http.StatusOK, &results)
	})

	router.Run(":5223")
}
