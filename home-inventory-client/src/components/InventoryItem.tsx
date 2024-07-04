import "./InventoryItem.css";

interface InventoryItemDetails {
	productName: string;
	itemStockAmount: number;
	goodStockCount: number;
}

function InventoryItem({productName, itemStockAmount, goodStockCount}: InventoryItemDetails) {
	return (
		<div className={itemStockAmount < goodStockCount ? "inventoryItem lowStockCount" : "inventoryItem"}>
		<span className="productName">{productName}</span>
		<span className="stockAmount">{itemStockAmount}</span>
		</div>
	);
}

export default InventoryItem;
