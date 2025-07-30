package main

import (
	"database/sql"
	"fmt"
	"net/http"
	"strconv"

	"github.com/gin-gonic/gin"
	_ "github.com/glebarez/go-sqlite"
)

var dbName string = "inventory.db"

var sampleProducts = []Product{
	{ Id: 1, ProductName: "Bread", Count: 4, WarningThreshold: 1 },
	{ Id: 2, ProductName: "Eggs", Count: 2, WarningThreshold: 1 },
	{ Id: 3, ProductName: "Milk", Count: 0, WarningThreshold: 1 },
}

func openDb() (*sql.DB, error) {
	db, err := sql.Open("sqlite", dbName)
	if (err != nil) {
		return nil, err
	}

	return db, err
}

func getProducts(c *gin.Context) {
	db, err := openDb()
	if (err != nil) {
		fmt.Printf("Error opening DB: %s\n", err)
		defer db.Close()
		c.AbortWithError(500, err)
		return
	}

	sql := `SELECT * FROM products`
	rows, err := db.Query(sql)
	defer rows.Close()
	var products []Product
	for (rows.Next()) {
		p := &Product{}
		err := rows.Scan(&p.Id, &p.ProductName, &p.Count, &p.WarningThreshold)
		if (err != nil) {
			fmt.Printf("Error querying products: %s\n", err)
			defer db.Close()
			c.AbortWithError(500, err)
	}

		products = append(products, *p)
	}

	defer db.Close()

	if products == nil {
		c.JSON(http.StatusOK, sampleProducts)
		return
	}

	c.JSON(http.StatusOK, products)
}

func getProduct(c *gin.Context) {
	productId, err := strconv.Atoi(c.Param("id"))
	if (err != nil) {
		fmt.Printf("Error parsing request parameter: %s\n", err)
		c.AbortWithError(500, err)
		return
	}

	if (productId <= 0) {
		failureMessage := BadRequestMsg {
			FieldName: "id",
			Message: "Invalid product ID.",
			StatusCode: 400,
		}

		c.AbortWithStatusJSON(400, failureMessage)
		return
	}

	db, err := openDb()
	if (err != nil) {
		fmt.Printf("Error opening DB: %s\n", err)
		c.AbortWithError(500, err)
		return
	}

	var product Product
	err = db.QueryRow("SELECT id, productName, count, warningThreshold FROM products WHERE id = ?", productId).Scan(&product.Id, &product.ProductName, &product.Count, &product.WarningThreshold)
	if (err != nil) {
		defer db.Close()
		fmt.Printf("Error querying for products: %s\n", err)
		c.AbortWithError(500, err)
		return
	}

	c.JSON(200, product)
}

func createProduct(c *gin.Context) {
	var product Product
	if err := c.BindJSON(&product); err != nil {
		fmt.Printf("Error parsing product: %s\n", err)
		c.AbortWithError(400, err)
	}

	fmt.Println("Opening DB")
	db, err := openDb()
	if (err != nil) {
		fmt.Printf("Error opening DB: %s\n", err)
		c.AbortWithError(500, err)
		return
	}

	fmt.Println("Checking for match")
	existingProductResult, err := db.Query("SELECT id FROM products WHERE productName = ?", product.ProductName)
	if (err != nil) {
		fmt.Printf("Error at product name check: %s", err)
		defer db.Close()
		c.AbortWithError(500, err)
		return
	}

	if (existingProductResult.Next()) {
		result := BadRequestMsg {
			StatusCode: 400,
			Message: "Product already exists",
			FieldName: "ProductName",
		}

		defer existingProductResult.Close()
		defer db.Close()
		c.AbortWithStatusJSON(400, result)
		return
	}

	fmt.Println("Inserting product")
	result, err := db.Exec("INSERT INTO products (productName, count, WarningThreshold) VALUES (?, ?, ?)", product.ProductName, product.Count, product.WarningThreshold)
	if (err != nil) {
		fmt.Printf("Error inserting: %s\n", err)
		defer db.Close()
		c.AbortWithError(500, err)
		return
	}

	defer db.Close()
	c.JSON(http.StatusOK, result)
}

func updateProduct(c *gin.Context) {
	var rProduct Product
	if err := c.BindJSON(&rProduct); err != nil {
		fmt.Printf("Error parsing product: %s\n", err)
		c.AbortWithError(400, err)
	}

	fmt.Println("Opening DB")
	db, err := openDb()
	if (err != nil) {
		defer db.Close()
		c.AbortWithError(500, err)
		fmt.Printf("Error opening DB: %s", err)
		return
	}

	fmt.Println("Querying for product")
	var dProduct Product
	err = db.QueryRow("SELECT id, productName, count, warningThreshold FROM products WHERE id = ?", rProduct.Id).Scan(&dProduct.Id, &dProduct.ProductName, &dProduct.Count, &dProduct.WarningThreshold)
	if (err != nil) {
		defer db.Close()
		c.AbortWithError(500, err)
		fmt.Printf("Error querying product with ID %d: %s\n", rProduct.Id, err)
		return
	}

	if (dProduct.Id < 1) {
		defer db.Close()
		fmt.Println("Creating product")
		createProduct(c)
		return
	}

	_, err = db.Exec("UPDATE products SET productName = ?, count = ?, warningThreshold = ? WHERE id = ?", rProduct.ProductName, rProduct.Count, rProduct.WarningThreshold, rProduct.Id)
	if (err != nil) {
		defer db.Close()
		c.AbortWithError(500, err)
		fmt.Printf("Error updating product: %s\n", err)
		return
	}
}

func getSqliteVersion(c *gin.Context) {
	db, err := sql.Open("sqlite", "./inventory.db")
	if err != nil {
		fmt.Println("getSqliteVersion: Error connecting to database")
		fmt.Println(err)
		defer db.Close()
		c.AbortWithError(500, err)
	}

	defer db.Close()
	fmt.Println("Connected to sqlite db successfully")

	var sqliteVersion string
	err = db.QueryRow("select sqlite_version()").Scan(&sqliteVersion)
	if err != nil {
		fmt.Println("getSqliteVersion: Error capturing sqlite version")
		fmt.Println(err)
		defer db.Close()
		c.AbortWithError(500, err)
	}

	c.Data(200, "application/text", []byte(sqliteVersion))
}
