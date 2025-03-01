package main

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
