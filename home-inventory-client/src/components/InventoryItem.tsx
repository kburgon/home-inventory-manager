import "./InventoryItem.css";

function InventoryItem() {
	return (
		<div className="inventoryItem">
			<div className="itemTitle">
				<div className="productName">Oatmeal</div>
				<div className="barcode">4wF33225</div>
			</div>
			<div className="itemStock">7</div>
		</div>
	);
}

export default InventoryItem;
