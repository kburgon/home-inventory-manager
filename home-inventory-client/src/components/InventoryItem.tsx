import "./InventoryItem.css";

interface InventoryItemDetails {
	itemName: string;
	itemStockAmount: number;
	goodStockCount: number;
}

function InventoryItem({itemName, itemStockAmount, goodStockCount}: InventoryItemDetails) {
	return (
		<div className={itemStockAmount < goodStockCount ? "inventoryItem lowStockCount" : "inventoryItem"}>
		<span className="productName">{itemName}</span>
		<span className="stockAmount">{itemStockAmount}</span>
		</div>
	);
}

export default InventoryItem;
