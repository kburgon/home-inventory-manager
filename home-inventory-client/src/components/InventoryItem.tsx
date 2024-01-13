import "./InventoryItem.css";

function InventoryItem({itemName, itemStockAmount}: {itemName: string, itemStockAmount: number}) {
	return (
		<div className="inventoryItem">
		<span className="productName">{itemName}</span>
		<span className="stockAmount">{itemStockAmount}</span>
		</div>
	);
}

export default InventoryItem;
